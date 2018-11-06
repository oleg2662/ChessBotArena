using Algorithms.Abstractions.Interfaces;
using Game.Chess;
using Game.Chess.Moves;

namespace MinimaxBot
{

    internal class MoveApplier : IApplier<ChessRepresentation, BaseMove>
    {
        private readonly ChessMechanism _mechanism;

        public MoveApplier(ChessMechanism mechanism)
        {
            _mechanism = mechanism;
        }

        public ChessRepresentation Apply(ChessRepresentation state, BaseMove move)
        {
            return _mechanism.ApplyMove(state, move);
        }
    }
}
