using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Abstractions.Interfaces;
using Game.Chess;
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

        private ChessPlayer GetOpponent(ChessRepresentation representation)
        {
            return representation.CurrentPlayer == ChessPlayer.Black ? ChessPlayer.White : ChessPlayer.Black;
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

            switch (gameOutcome)
            {
                case GameState.WhiteWon:
                    return state.CurrentPlayer == ChessPlayer.White ? int.MaxValue : int.MinValue;

                case GameState.BlackWon:
                    return state.CurrentPlayer == ChessPlayer.Black ? int.MaxValue : int.MinValue;

                case GameState.Draw:
                    return 0;
            }

            var result = GetValueOfThreatenedPositions(state)
                         + GetRelativeValueOfPiecesOnTable(state)
                         + GetValueOfCheckStatus(state)
                         + GetMobilityValue(state);

            return result;
        }

        private int GetValueOfCheckStatus(ChessRepresentation representation)
        {
            var inCheck = _mechanism.IsPlayerInChess(representation, representation.CurrentPlayer);
            return inCheck ? -1000 : 1000;
        }

        private int GetValueOfThreatenedPositions(ChessRepresentation representation)
        {
            var opponent = GetOpponent(representation);

            var threatenedPositions = _mechanism.GetThreatenedPositions(representation, representation.CurrentPlayer)
                                          .Select(x => representation[x]?.Kind)
                                          .Select(x => x == null ? 1 : ChessPieceValues[x.Value])
                                          .Sum() * 0.5;

            var opponentThreatenedPositions = _mechanism.GetThreatenedPositions(representation, opponent)
                                                  .Select(x => representation[x]?.Kind)
                                                  .Select(x => x == null ? 1 : ChessPieceValues[x.Value])
                                                  .Sum() * 0.5;

            return (int)Math.Round(threatenedPositions - opponentThreatenedPositions);
        }

        private int GetRelativeValueOfPiecesOnTable(ChessRepresentation state)
        {
            var chessPiecesValue = Positions.PositionList.Select(x => state[x])
                .Where(x => x != null)
                .Where(x => x.Owner == state.CurrentPlayer)
                .Select(x => ChessPieceValues[x.Kind])
                .Sum();

            var opponentChessPiecesValue = Positions.PositionList.Select(x => state[x])
                .Where(x => x != null)
                .Where(x => x.Owner != state.CurrentPlayer)
                .Select(x => ChessPieceValues[x.Kind])
                .Sum();

            return chessPiecesValue - opponentChessPiecesValue;
        }

        private int GetMobilityValue(ChessRepresentation state)
        {
            return + _mechanism.GenerateMoves(state).Count();
        }
    }
}
