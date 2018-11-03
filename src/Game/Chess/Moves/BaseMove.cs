using System;
using Game.Abstraction;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [Serializable]
    public abstract class BaseMove : ICloneable<BaseMove>, IEquatable<BaseMove>
    {
        public bool Equals(BaseMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Owner == other.Owner;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseMove) obj);
        }

        public override int GetHashCode()
        {
            return (int) Owner;
        }

        public static bool operator ==(BaseMove left, BaseMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseMove left, BaseMove right)
        {
            return !Equals(left, right);
        }

        [JsonConstructor]
        protected BaseMove(ChessPlayer owner)
        {
            Owner = owner;
        }

        public ChessPlayer Owner { get; }

        public abstract BaseMove Clone();
    }
}