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
            var board = new ChessBoard()
            {
                CurrentPlayer = ChessPlayer.White,
                History = new List<ChessMove>(),
                Players = new[] { ChessPlayer.White, ChessPlayer.Black }
            };

            board[Positions.D4] = new Knight(ChessPlayer.White, true);
            board[Positions.B5] = new Pawn(ChessPlayer.White, false);
            board[Positions.B2] = new Pawn(ChessPlayer.White, false);
            board[Positions.E6] = new Pawn(ChessPlayer.Black, false);
            board[Positions.F5] = new Pawn(ChessPlayer.Black, false);

            ChessboardVisualizer.ChessboardVisualizer.TestShowVisualizer(board);
        }
    }
}
