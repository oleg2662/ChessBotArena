using Game.Chess.Pieces;
using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [Serializable]
    [DebuggerDisplay("{From}->{To}(prom:{PromoteTo})")]
    public sealed class PawnPromotionalMove : BaseChessMove, IEquatable<PawnPromotionalMove>
    {
        public bool Equals(PawnPromotionalMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(From, other.From)
                   && Equals(To, other.To)
                   && Equals(PromoteTo, other.PromoteTo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((PawnPromotionalMove) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (int) PromoteTo;
            }
        }

        public static bool operator ==(PawnPromotionalMove left, PawnPromotionalMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PawnPromotionalMove left, PawnPromotionalMove right)
        {
            return !Equals(left, right);
        }

        public PieceKind PromoteTo { get; }

        [JsonConstructor]
        public PawnPromotionalMove(ChessPlayer owner, Position from, Position to, PieceKind promoteTo)
            : base(owner, from, to)
        {
            PromoteTo = promoteTo;
        }

        public override BaseMove Clone()
        {
            return new PawnPromotionalMove(Owner, From, To, PromoteTo);
        }
    }
}
