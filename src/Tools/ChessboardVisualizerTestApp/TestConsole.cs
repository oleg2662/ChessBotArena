using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessboardVisualizer;
using Game.Chess;
using Game.Chess.Moves;
using Game.Chess.Pieces;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ChessboardVisualizerTestApp
{
    class TestConsole
    {
        static void Main(string[] args)
        {
            var board = new ChessRepresentationInitializer().Create();

            board.CurrentPlayer = ChessPlayer.White;
            board.History = new List<ChessMove>();
            board.Players = new[] { ChessPlayer.White, ChessPlayer.Black };

            var manager = new ChessMechanism();

            var step1 = new ChessMove
            {
                //ChessMoveResult = ChessMoveResult.Nothing,
                ChessPiece = PieceKind.Pawn,
                From = (Position)"B2",
                To = (Position)"B4",
                IsCaptureMove = false,
                Owner = ChessPlayer.White
            };

            var step2 = new ChessMove
            {
                //ChessMoveResult = ChessMoveResult.Nothing,
                ChessPiece = PieceKind.Pawn,
                From = (Position)"E7",
                To = (Position)"E5",
                IsCaptureMove = false,
                Owner = ChessPlayer.Black
            };

            var step3 = new ChessMove
            {
                //ChessMoveResult = ChessMoveResult.Nothing,
                ChessPiece = PieceKind.Pawn,
                From = (Position)"B4",
                To = (Position)"B5",
                IsCaptureMove = false,
                Owner = ChessPlayer.White
            };

            var step4 = new ChessMove
            {
                //ChessMoveResult = ChessMoveResult.Nothing,
                ChessPiece = PieceKind.Pawn,
                From = (Position)"B8",
                To = (Position)"C6",
                IsCaptureMove = false,
                Owner = ChessPlayer.Black
            };


            var step5 = new ChessMove
            {
                //ChessMoveResult = ChessMoveResult.Nothing,
                ChessPiece = PieceKind.Pawn,
                From = (Position)"B5",
                To = (Position)"C6",
                IsCaptureMove = true,
                Owner = ChessPlayer.White
            };

            board = manager.ApplyMove(board, step1);
            board = manager.ApplyMove(board, step2);
            board = manager.ApplyMove(board, step3);
            board = manager.ApplyMove(board, step4);
            board = manager.ApplyMove(board, step5);

            ChessboardVisualizer.ChessboardVisualizer.TestShowVisualizer(board);
        }
    }
}
