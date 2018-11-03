using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [DebuggerDisplay("{From}->{To}")]
    [Serializable]
    public abstract class BaseChessMove : BaseMove
    {
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

        public Position From { get; }

        public Position To { get; }

        [JsonConstructor]
        protected BaseChessMove(ChessPlayer owner, Position from, Position to) : base(owner)
        {
            From = from;
            To = to;
        }
    }
}
