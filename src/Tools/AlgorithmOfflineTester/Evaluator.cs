using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Algorithms.Abstractions.Interfaces;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Pieces;

namespace BoardGame.Tools.AlgorithmOfflineTester
{
    internal class Evaluator : IEvaluator<ChessRepresentation>
    {
        private readonly ChessMechanism _mechanism;

        public Evaluator(ChessMechanism mechanism)
        {
            _mechanism = mechanism;
        }

        private ChessPlayer GetOpponent(ChessRepresentation state)
        {
            return state.CurrentPlayer == ChessPlayer.Black ? ChessPlayer.White : ChessPlayer.Black;
        }

        private static readonly Dictionary<PieceKind, int> ChessPieceValues = new Dictionary<PieceKind, int>()
        {
            [PieceKind.Pawn] = 1,
            [PieceKind.Knight] = 6,
            [PieceKind.Bishop] = 6,
            [PieceKind.Rook] = 10,
            [PieceKind.Queen] = 20,
            [PieceKind.King] = 8,
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
            }

            var threatenedValue = GetValueOfThreatenedPositions(state);
            var piecesValue = GetRelativeValueOfPiecesOnTable(state);
            var checkStatusValue = GetValueOfCheckStatus(state);
            var mobilityValue = GetMobilityValue(state);
            var castlingValue = GetValueForCastling(state);

            if (gameOutcome == GameState.Draw)
            {
                if (checkStatusValue > 0 || mobilityValue < 10 || piecesValue > 40)
                {
                    return int.MinValue;
                }

                return -100;
            }

            var result = threatenedValue
                         + piecesValue
                         + checkStatusValue
                         + mobilityValue
                         + castlingValue;

            return result;
        }

        private int GetValueOfCheckStatus(ChessRepresentation state)
        {
            var inCheck = _mechanism.IsPlayerInChess(state, state.CurrentPlayer);
            return inCheck ? -1000 : 1000;
        }

        private int GetValueOfThreatenedPositions(ChessRepresentation state)
        {
            var opponent = GetOpponent(state);

            var threatenedPositions = _mechanism.GetThreatenedPositions(state, opponent)
                                          .Select(x => state[x]?.Kind)
                                          .Select(x => x == null ? 1 : ChessPieceValues[x.Value])
                                          .Sum() * 0.75;

            return (int)Math.Round(threatenedPositions);
        }

        private int GetRelativeValueOfPiecesOnTable(ChessRepresentation state)
        {
            var chessPiecesValue = Positions.PositionList.Select(x => state[x])
                .Where(x => x != null)
                .Where(x => x.Owner == state.CurrentPlayer)
                .Select(x => ChessPieceValues[x.Kind])
                .Sum();

            return chessPiecesValue;
        }

        private int GetMobilityValue(ChessRepresentation state)
        {
            return _mechanism.GenerateMoves(state).Count();
        }

        private int GetValueForCastling(ChessRepresentation state)
        {
            // Check if the state seems to be after castling...

            var expectedRow = state.CurrentPlayer == ChessPlayer.White ? 1 : 8;

            var kingPosition = Positions.PositionList
                .Select(x => new {Position = x, Piece = state[x]})
                .Where(x => x.Piece != null)
                .Where(x => x.Piece.Owner == state.CurrentPlayer)
                .Where(x => x.Position.Row == expectedRow)
                .Where(x => x.Piece.Kind == PieceKind.King)
                .Select(x => x.Position)
                .FirstOrDefault();

            var rookPosition = Positions.PositionList
                .Select(x => new {Position = x, Piece = state[x]})
                .Where(x => x.Piece != null)
                .Where(x => x.Piece.Owner == state.CurrentPlayer)
                .Where(x => x.Position.Row == expectedRow)
                .Where(x => x.Piece.Kind == PieceKind.Rook)
                .ToArray();

            if (kingPosition == null || !rookPosition.Any())
            {
                return 0;
            }

            if (kingPosition.Column == 'G' && rookPosition.Any(x => x.Position.Column == 'F'))
            {
                return 200;
            }

            if (kingPosition.Column == 'C' && rookPosition.Any(x => x.Position.Column == 'D'))
            {
                return 200;
            }

            return 0;
        }
    }
}
