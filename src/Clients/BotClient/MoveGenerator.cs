using System.Collections.Generic;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Abstractions.Interfaces;

namespace BoardGame.BotClient
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
