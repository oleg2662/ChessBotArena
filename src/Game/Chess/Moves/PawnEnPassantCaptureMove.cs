using System;

namespace Game.Chess.Moves
{
    [Serializable]
    public class PawnEnPassantMove : ChessMove
    {
        public Position CapturePosition { get; set; }

        public override bool Equals(ChessMove other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var specificMove = (PawnEnPassantMove)other;

            if (specificMove == null)
            {
                return false;
            }

            return CapturePosition.Equals(specificMove.CapturePosition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = base.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ CapturePosition.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ nameof(PawnEnPassantMove).GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}e.p.";
        }

        public override ChessMove Clone()
        {
            return new PawnEnPassantMove()
            {
                ChessPiece = ChessPiece,
                Owner = Owner,
                From = From,
                To = To,
                IsCaptureMove = IsCaptureMove,
                CapturePosition = CapturePosition,
                //ChessMoveResult = ChessMoveResult
            };
        }
    }
}
