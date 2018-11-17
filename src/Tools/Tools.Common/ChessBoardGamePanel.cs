using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;

namespace BoardGame.Tools.Common
{
    public sealed class ChessBoardGamePanel : BaseChessBoardVisualizerPanel
    {
        private readonly ChessMechanism _mechanism;

        public event ChessBoardMoveSelectedEventHandler OnValidMoveSelected;

        public ChessBoardGamePanel()
        {
            DoubleBuffered = true;
            _mechanism = new ChessMechanism();
        }

        public Position TargetPosition { get; private set; }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (SelectedPosition == null)
            {
                base.OnMouseClick(e);
                return;
            }

            if (HoverPosition == SelectedPosition)
            {
                SelectedPosition = null;
                ThreatenedPositions = Enumerable.Empty<Position>();
                return;
            }

            if (HoverPosition == TargetPosition)
            {
                TargetPosition = null;
                return;
            }

            if (ThreatenedPositions.Contains(HoverPosition))
            {
                TargetPosition = HoverPosition;
                var move = GetSelectedChessMove();
                if (move == null)
                {
                    return;
                }

                ChessRepresentation = _mechanism.ApplyMove(ChessRepresentation, move);
                OnValidMoveSelected?.Invoke(this, new ChessboardMoveSelectedEventArg(move, ChessRepresentation.CurrentPlayer));
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            HighlightField(pe.Graphics, TargetPosition, Color.DarkMagenta);
        }

        private BaseChessMove GetSelectedChessMove()
        {
            var moves = _mechanism
                .GenerateMoves(ChessRepresentation)
                .OfType<BaseChessMove>()
                .Where(x => x.From == SelectedPosition && x.To == TargetPosition)
                .ToArray();

            if (moves.Length <= 1) return moves.FirstOrDefault();
            
            // Promotional moves...
            var promotionalForm = new PromotionForm();
            promotionalForm.ShowDialog();

            return moves.OfType<PawnPromotionalMove>().FirstOrDefault(x => x.PromoteTo == promotionalForm.SelectedPieceKind);
        }
    }

    public class ChessboardMoveSelectedEventArg
    {
        public ChessboardMoveSelectedEventArg(BaseChessMove move, ChessPlayer player)
        {
            Move = move;
            Player = player;
        }
        public BaseChessMove Move { get; }
        public ChessPlayer Player { get; }
    }

    public delegate void ChessBoardMoveSelectedEventHandler(object source, ChessboardMoveSelectedEventArg eventArg);
}
