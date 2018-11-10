using System.Collections.Generic;
using Algorithms.Abstractions.Interfaces;
using Game.Chess;
using Game.Chess.Moves;

namespace BotClient
{
    internal class MoveGenerator : IGenerator<ChessRepresentation, BaseMove>
    {
        private readonly ChessMechanism _mechanism;

        public MoveGenerator(ChessMechanism mechanism)
        {
            _mechanism = mechanism;
        }

        public IEnumerable<BaseMove> Generate(ChessRepresentation state)
        {
            return _mechanism.GenerateMoves(state);
        }
    }
}
