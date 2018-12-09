using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Algorithms.Abstractions.Interfaces;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;

namespace BoardGame.AlgorithmOfflineTester
{
    internal class MoveGenerator : IGenerator<ChessRepresentation, BaseMove>
    {
        private readonly ChessMechanism _mechanism;
        private readonly Random _random = new Random();
        private readonly ICollection<MessageType> _forbiddenMessageTypes;

        public MoveGenerator(ChessMechanism mechanism)
        {
            _mechanism = mechanism;
            _forbiddenMessageTypes = new List<MessageType>()
            {
                MessageType.Resign,
                MessageType.DrawAccept,
                MessageType.DrawOffer
            };
        }

        public IEnumerable<BaseMove> Generate(ChessRepresentation state)
        {
            return _mechanism
                .GenerateMoves(state)
                .Where(x => (x.GetType() != typeof(SpecialMove))
                            || (x.GetType() == typeof(SpecialMove) && !_forbiddenMessageTypes.Contains(((SpecialMove)x).Message)))
                .OrderBy(x => _random.Next(0, 100));
        }
    }
}
