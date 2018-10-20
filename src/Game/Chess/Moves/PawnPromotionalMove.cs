using Game.Chess.Pieces;
using System;

namespace Game.Chess.Moves
{
    [Serializable]
    public class PawnPromotionalMove : ChessMove
    {
        public ChessPiece PromoteTo { get; set; }

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
                hash = (hash ^ Constants.HashXor) ^ this.PromoteTo.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ nameof(PawnPromotionalMove).GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}={PromoteTo.ToString()}.";
        }

        public override ChessMove Clone()
        {
            return new PawnPromotionalMove()
            {
                ChessPiece = this.ChessPiece.Clone(),
                From = this.From,
                To = this.To,
                IsCaptureMove = this.IsCaptureMove,
                PromoteTo = this.PromoteTo.Clone()
            };
        }
    }
}
