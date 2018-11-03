using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [DebuggerDisplay("{From}->{To}")]
    [Serializable]
    public abstract class BaseChessMove : BaseMove
    {
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
