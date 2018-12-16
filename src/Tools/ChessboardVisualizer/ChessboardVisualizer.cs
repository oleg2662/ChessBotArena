using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Tools.ChessboardVisualizer;
using BoardGame.Tools.Common;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(typeof(ChessboardVisualizer), typeof(Microsoft.VisualStudio.DebuggerVisualizers.VisualizerObjectSource), Target = typeof(ChessRepresentation), Description = "Chessboard visualizer")]
namespace BoardGame.Tools.ChessboardVisualizer
{
    /// <summary>
    /// Visualizer for the chessboard. 
    /// </summary>
    public class ChessboardVisualizer : DialogDebuggerVisualizer
    {
        /// <summary>
        /// Shows the chess board visualizer.
        /// </summary>
        /// <param name="windowService">Window service used to show the visualizer.</param>
        /// <param name="objectProvider">The providers used to get the object to be inspected.</param>
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            if (windowService == null)
                throw new ArgumentNullException(nameof(windowService));
            if (objectProvider == null)
                throw new ArgumentNullException(nameof(objectProvider));

            var chessBoard = (ChessRepresentation)objectProvider.GetObject();

            using (var displayForm = new Form())
            {
                var history = chessBoard.Clone().History.ToArray();
                var index = Math.Max(history.Length, 0);

                displayForm.Size = new Size(400, 400);
                displayForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                displayForm.Text = $"Current player: {chessBoard.CurrentPlayer} ({index}/{history.Length})";

                var chessPanel = new ChessBoardVisualizerPanel();
                chessPanel.ChessRepresentation = chessBoard;
                displayForm.Controls.Add(chessPanel);
                chessPanel.Dock = DockStyle.Fill;
                displayForm.ResizeEnd += (sender, args) => chessPanel.Refresh();
                displayForm.KeyUp += (sender, args) =>
                {
                    ChessRepresentation newBoard;

                    switch (args.KeyCode)
                    {
                        case Keys.Left:
                            if (index > 0)
                            {
                                index--;
                            }

                            newBoard = CalculateBoard(history, index);
                            break;

                        case Keys.Right:
                            if (index < history.Length)
                            {
                                index++;
                            }

                            newBoard = CalculateBoard(history, index);
                            break;

                        case Keys.Home:
                            index = 0;
                            newBoard = CalculateBoard(history, index);
                            break;

                        case Keys.End:
                            index = history.Length;
                            newBoard = CalculateBoard(history, index);
                            break;

                        case Keys.F1:
                            MessageBox.Show(
                                "Left arrow:\tGo back in history.\r\nRight arrow:\tGo forward in history\r\nEscape:\t\tClose Window\r\nF1:\t\tHelp dialog\r\nHome\t\tGo to start state\r\nEnd\t\tGo to end",
                                "Chessboard visualizer help");
                            return;

                        case Keys.Escape:
                            // ReSharper disable once AccessToDisposedClosure
                            displayForm.Close();
                            return;

                        default:
                            return;
                    }

                    chessPanel.ChessRepresentation = newBoard;

                    // ReSharper disable once AccessToDisposedClosure
                    displayForm.Text = $"Current player: {newBoard.CurrentPlayer} ({index}/{history.Length})";
                };

                windowService.ShowDialog(displayForm);
            }
        }

        private ChessRepresentation CalculateBoard(IReadOnlyList<BaseMove> history, int index)
        {
            var newBoard = new ChessRepresentationInitializer().Create();
            var mechanism = new ChessMechanism();

            for (var i = 0; i < index; i++)
            {
                newBoard = mechanism.ApplyMove(newBoard, history[i]);
            }

            return newBoard;
        }

        /// <summary>
        /// Tests the visualizer by hosting it outside of the debugger.
        /// </summary>
        /// <param name="objectToVisualize">The object to display in the visualizer.</param>
        public static void TestShowVisualizer(object objectToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(ChessboardVisualizer));
            visualizerHost.ShowVisualizer();
        }
    }

    public class ControlVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, Stream outgoingData)
        {
            var writer = new StreamWriter(outgoingData);
            writer.WriteLine(((Control)target).Text);
            writer.Flush();
        }
    }
}
