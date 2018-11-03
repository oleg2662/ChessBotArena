using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [Serializable]
    [DebuggerDisplay("{From}->{To}")]
    public class ChessMove : BaseMove, IEquatable<ChessMove>
    {
        public bool Equals(ChessMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return base.Equals(other) 
                   && Equals(From, other.From)
                   && Equals(To, other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((ChessMove) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (From != null ? From.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (To != null ? To.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(ChessMove left, ChessMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ChessMove left, ChessMove right)
        {
            return !Equals(left, right);
        }

        public Position From { get; }

        public Position To { get; }

        [JsonConstructor]
        public ChessMove(ChessPlayer owner, Position from, Position to)
            : base(owner)
        {
            From = from;
            To = to;
        }

        public override BaseMove Clone()
        {
            return new ChessMove(Owner, From, To);
        }
    }
}
