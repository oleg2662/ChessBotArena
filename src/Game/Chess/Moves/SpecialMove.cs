using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [Serializable]
    public class SpecialMove : BaseMove, IEquatable<SpecialMove>
    {
        public bool Equals(SpecialMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Owner, other.Owner)
                   && Message == other.Message;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((SpecialMove) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Owner.GetHashCode();
                hashCode = (hashCode * 397) ^ Message.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{Message}";
        }

        public static bool operator ==(SpecialMove left, SpecialMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SpecialMove left, SpecialMove right)
        {
            return !Equals(left, right);
        }

        [JsonConstructor]
        public SpecialMove(ChessPlayer owner, MessageType message)
            : base(owner)
        {
            Message = message;
        }

        public MessageType Message { get; }

        public override BaseMove Clone()
        {
            return new SpecialMove(Owner, Message);
        }
    }
}