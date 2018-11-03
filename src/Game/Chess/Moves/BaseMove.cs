using System;
using Game.Abstraction;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [Serializable]
    public abstract class BaseMove : ICloneable<BaseMove>
    {
        public override int GetHashCode()
        {
            return (int) Owner;
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