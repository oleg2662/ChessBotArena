using System;
using Newtonsoft.Json;

namespace BoardGame.Game.Chess.Moves
{
    
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

        public override string ToString()
        {
            return $"{From}->{To}";
        }
    }
}
