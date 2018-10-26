using Game.Chess.Moves;
using Game.Chess.Pieces;
using System.Collections.Generic;

namespace Game.Chess
{
    public class ChessRepresentationInitializer : IChessRepresentationInitializer
    {
        public ChessRepresentation Create()
        {
            var board = new ChessRepresentation()
            {
                Players = new List<ChessPlayer> { ChessPlayer.White, ChessPlayer.Black },
                CurrentPlayer = ChessPlayer.White,
                History = new List<ChessMove>()
            };

            board[Positions.A1] = ChessPieces.WhiteRook;
            board[Positions.B1] = ChessPieces.WhiteKnight;
            board[Positions.C1] = ChessPieces.WhiteBishop;
            board[Positions.D1] = ChessPieces.WhiteQueen;
            board[Positions.E1] = ChessPieces.WhiteKing;
            board[Positions.F1] = ChessPieces.WhiteBishop;
            board[Positions.G1] = ChessPieces.WhiteKnight;
            board[Positions.H1] = ChessPieces.WhiteRook;

            board[Positions.A2] = ChessPieces.WhitePawn;
            board[Positions.B2] = ChessPieces.WhitePawn;
            board[Positions.C2] = ChessPieces.WhitePawn;
            board[Positions.D2] = ChessPieces.WhitePawn;
            board[Positions.E2] = ChessPieces.WhitePawn;
            board[Positions.F2] = ChessPieces.WhitePawn;
            board[Positions.G2] = ChessPieces.WhitePawn;
            board[Positions.H2] = ChessPieces.WhitePawn;

            board[Positions.A7] = ChessPieces.BlackPawn;
            board[Positions.B7] = ChessPieces.BlackPawn;
            board[Positions.C7] = ChessPieces.BlackPawn;
            board[Positions.D7] = ChessPieces.BlackPawn;
            board[Positions.E7] = ChessPieces.BlackPawn;
            board[Positions.F7] = ChessPieces.BlackPawn;
            board[Positions.G7] = ChessPieces.BlackPawn;
            board[Positions.H7] = ChessPieces.BlackPawn;

            board[Positions.A8] = ChessPieces.BlackRook;
            board[Positions.B8] = ChessPieces.BlackKnight;
            board[Positions.C8] = ChessPieces.BlackBishop;
            board[Positions.D8] = ChessPieces.BlackQueen;
            board[Positions.E8] = ChessPieces.BlackKing;
            board[Positions.F8] = ChessPieces.BlackBishop;
            board[Positions.G8] = ChessPieces.BlackKnight;
            board[Positions.H8] = ChessPieces.BlackRook;

            return board;
        }
    }
}
