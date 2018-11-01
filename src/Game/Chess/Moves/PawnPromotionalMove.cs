using Game.Chess.Pieces;
using System;
using Game.Abstraction;

namespace Game.Chess.Moves
{
    [Serializable]
    public class PawnPromotionalMove : ChessMove
    {
        public PieceKind PromoteTo { get; set; }

        public override bool Equals(ChessMove other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var specificMove = (PawnPromotionalMove)other;

            if (specificMove == null)
            {
                return false;
            }

            return PromoteTo.Equals(specificMove.PromoteTo);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = base.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ PromoteTo.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ nameof(PawnPromotionalMove).GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}={PromoteTo.ToString()}";
        }

        public override ChessMove Clone()
        {
            return new PawnPromotionalMove()
            {
                ChessPiece = ChessPiece,
                Owner = Owner,
                From = From,
                To = To,
                IsCaptureMove = IsCaptureMove,
                PromoteTo = PromoteTo,
            };
        }
    }
}
