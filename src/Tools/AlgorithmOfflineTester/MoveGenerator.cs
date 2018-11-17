using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Abstractions.Interfaces;

namespace BoardGame.AlgorithmOfflineTester
{
    internal class MoveGenerator : IGenerator<ChessRepresentation, BaseMove>
    {
        private readonly ChessMechanism _mechanism;
        private readonly Random _random = new Random();

        public MoveGenerator(ChessMechanism mechanism)
        {
            _mechanism = mechanism;
        }

        public IEnumerable<BaseMove> Generate(ChessRepresentation state)
        {
            return _mechanism.GenerateMoves(state).OrderBy(x => _random.Next(0, 100));
        }
    }
}
