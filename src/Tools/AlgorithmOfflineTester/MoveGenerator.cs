using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Abstractions.Interfaces;
using Game.Chess;
using Game.Chess.Moves;

namespace AlgorithmOfflineTester
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
