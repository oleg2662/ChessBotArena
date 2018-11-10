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
            var previousPlayer = state.CurrentPlayer == ChessPlayer.Black ? ChessPlayer.White : ChessPlayer.Black;
            var lastMove = state.History.Last();
            var moveBonus = 0;

            if (!(lastMove is SpecialMove))
            {
                moveBonus += 10;
            }

            if (lastMove is BaseChessMove lastBaseChessMove)
            {
                var movedPiece = state[lastBaseChessMove.To];
                if (movedPiece != null)
                {
                    moveBonus += ChessPieceValues[movedPiece.Kind];
                }
            }

            switch (gameOutcome)
            {
                case GameState.WhiteWon:
                    return previousPlayer == ChessPlayer.White ? int.MaxValue : int.MinValue;
                case GameState.BlackWon:
                    return previousPlayer == ChessPlayer.Black ? int.MaxValue : int.MinValue;
                case GameState.Draw:
                    return 10;
            }

            var chessPieces = Positions.PositionList.Select(x => state[x]).Where(x => x != null).ToList();

            var whitePieceAndValues =
                chessPieces.Where(x => x.Owner == ChessPlayer.White).Select(x => ChessPieceValues[x.Kind]).ToArray();

            var blackPieceAndValues =
                chessPieces.Where(x => x.Owner == ChessPlayer.Black).Select(x => ChessPieceValues[x.Kind]).ToArray();

            var whiteValue = (int)Math.Round(whitePieceAndValues.Sum() + whitePieceAndValues.Average());
            var blackValue = (int)Math.Round(blackPieceAndValues.Sum() + whitePieceAndValues.Average());

            var piecesValue = previousPlayer == ChessPlayer.White
                                ? whiteValue - blackValue
                                : blackValue - whiteValue;

            var result = piecesValue + moveBonus;

            return result;
        }
    }
}
