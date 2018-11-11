using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Abstractions.Interfaces;
using Game.Chess;
using Game.Chess.Moves;
using Game.Chess.Pieces;

namespace BotClient
{
    internal class Evaluator : IEvaluator<ChessRepresentation>
    {
        private readonly ChessMechanism _mechanism;

        public Evaluator(ChessMechanism mechanism)
        {
            _mechanism = mechanism;
        }

        private static readonly Dictionary<PieceKind, int> ChessPieceValues = new Dictionary<PieceKind, int>()
        {
            [PieceKind.Pawn] = 1,
            [PieceKind.Knight] = 3,
            [PieceKind.Bishop] = 3,
            [PieceKind.Rook] = 5,
            [PieceKind.Queen] = 10,
            [PieceKind.King] = 4,
        };

        public int Evaluate(ChessRepresentation state)
        {
            var gameOutcome = _mechanism.GetGameState(state);
            var lastMove = state.History.Last();
            var moveBonus = 0;

            if (!(lastMove is SpecialMove))
            {
                moveBonus += 10;
            }

            var chessPiecesValue = Positions.PositionList.Select(x => state[x])
                .Where(x => x != null)
                .Where(x => x.Owner == state.CurrentPlayer)
                .Select(x => ChessPieceValues[x.Kind])
                .Sum();

            switch (gameOutcome)
            {
                case GameState.WhiteWon:
                    return state.CurrentPlayer == ChessPlayer.White ? int.MaxValue : int.MinValue;

                case GameState.BlackWon:
                    return state.CurrentPlayer == ChessPlayer.Black ? int.MaxValue : int.MinValue;

                case GameState.Draw:
                    var opponentChessPiecesValue = Positions.PositionList.Select(x => state[x])
                                                    .Where(x => x != null)
                                                    .Where(x => x.Owner != state.CurrentPlayer)
                                                    .Select(x => ChessPieceValues[x.Kind])
                                                    .Sum();

                    return chessPiecesValue - opponentChessPiecesValue;
            }

            var result = chessPiecesValue + moveBonus;

            return result;
        }
    }
}
