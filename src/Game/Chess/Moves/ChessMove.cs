using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace BoardGame.Game.Chess.Moves
{
    [Serializable]
    [DebuggerDisplay("{From}->{To}")]
    public sealed class ChessMove : BaseChessMove, IEquatable<ChessMove>
    {
        public bool Equals(ChessMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(From, other.From) && Equals(To, other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((ChessMove)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Owner.GetHashCode();
                hashCode = (hashCode * 397) ^ (From != null ? From.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (To != null ? To.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{From}->{To}";
        }

        public static bool operator ==(ChessMove left, ChessMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ChessMove left, ChessMove right)
        {
            return !Equals(left, right);
        }

        [JsonConstructor]
        public ChessMove(ChessPlayer owner, Position from, Position to)
            : base(owner, from, to)
        {
        }

        public override BaseMove Clone()
        {
            return new ChessMove(Owner, From, To);
        }
    }
}