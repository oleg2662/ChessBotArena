[assembly: System.Diagnostics.DebuggerVisualizer(
    typeof(ChessboardVisualizer.ChessboardVisualizer),
    typeof(Microsoft.VisualStudio.DebuggerVisualizers.VisualizerObjectSource),
    Target = typeof(Game.Chess.ChessBoard),
    Description = "Chessboard visualizer")]
namespace ChessboardVisualizer
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.DebuggerVisualizers;
    using Game.Chess;
    using System.Drawing;
    using System.IO;

    /// <summary>
    /// Visualizer for the chessboard. 
    /// </summary>
    public class ChessboardVisualizer : DialogDebuggerVisualizer
    {
        /// <summary>
        /// Shows the chess board visualizator.
        /// </summary>
        /// <param name="windowService">Window service used to show the visualizator.</param>
        /// <param name="objectProvider">The providers used to get the object to be inspected.</param>
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            if (windowService == null)
                throw new ArgumentNullException(nameof(windowService));
            if (objectProvider == null)
                throw new ArgumentNullException(nameof(objectProvider));

            var chessBoard = (ChessBoard)objectProvider.GetObject();

            using (var displayForm = new Form())
            {
                displayForm.Size = new Size(400, 400);
                displayForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                displayForm.Text = $"Next: {chessBoard.CurrentPlayer}";

                var pictureBox = new PictureBox();
                var bitmap = new Bitmap(1000, 1000);

                pictureBox.Image = bitmap;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                var g = Graphics.FromImage(bitmap);

                var blackColour = Color.SandyBrown;
                var whiteColour = Color.BlanchedAlmond;

                var blackBrush = new SolidBrush(blackColour);
                var whiteBrush = new SolidBrush(whiteColour);

                g.FillRectangle(whiteBrush, g.VisibleClipBounds);

                for (var i = 0; i < 64; i++)
                {
                    Position p = (Position)i;

                    var brush = ((p.Row-1) + (p.Column-'A')) % 2 == 0 ? blackBrush : whiteBrush;
                    var rectangle = this.GetFieldRectangle(g.VisibleClipBounds, (Position)i);

                    g.FillRectangle(brush, rectangle);

                    var figureText = chessBoard[p]?.ToString() ?? string.Empty;

                    var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    g.DrawString(
                        figureText,
                        new Font(FontFamily.GenericSansSerif, rectangle.Width / 2, FontStyle.Regular),
                        Brushes.Black,
                        rectangle,
                        sf);
                }

                displayForm.Controls.Add(pictureBox);
                pictureBox.Dock = DockStyle.Fill;

                windowService.ShowDialog(displayForm);
            }
        }

        private RectangleF GetFieldRectangle(RectangleF clipRectangle, Position position)
        {
            var col = position.Column - 'A';
            var row = 8 - position.Row;

            var width = clipRectangle.Width / 8.0f;
            var height = clipRectangle.Height / 8.0f;

            var calculatedPosition = new PointF(clipRectangle.Left + col * width, clipRectangle.Top + row * height);
            var calculatedSize = new SizeF(width, height);

            return new RectangleF(calculatedPosition, calculatedSize);
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
            var board = (ChessBoard)target;
            writer.WriteLine(((Control)target).Text);
            writer.Flush();
        }
    }
}
