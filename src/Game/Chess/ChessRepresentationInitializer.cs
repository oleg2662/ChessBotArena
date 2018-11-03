using Game.Chess.Moves;
using Game.Chess.Pieces;
using System.Collections.Generic;

namespace Game.Chess
{
    public class ChessRepresentationInitializer
    {
        public ChessRepresentation Create()
        {
            var board = new ChessRepresentation
            {
                CurrentPlayer = ChessPlayer.White,

                [Positions.A1] = ChessPieces.WhiteRook,
                [Positions.B1] = ChessPieces.WhiteKnight,
                [Positions.C1] = ChessPieces.WhiteBishop,
                [Positions.D1] = ChessPieces.WhiteQueen,
                [Positions.E1] = ChessPieces.WhiteKing,
                [Positions.F1] = ChessPieces.WhiteBishop,
                [Positions.G1] = ChessPieces.WhiteKnight,
                [Positions.H1] = ChessPieces.WhiteRook,
                [Positions.A2] = ChessPieces.WhitePawn,
                [Positions.B2] = ChessPieces.WhitePawn,
                [Positions.C2] = ChessPieces.WhitePawn,
                [Positions.D2] = ChessPieces.WhitePawn,
                [Positions.E2] = ChessPieces.WhitePawn,
                [Positions.F2] = ChessPieces.WhitePawn,
                [Positions.G2] = ChessPieces.WhitePawn,
                [Positions.H2] = ChessPieces.WhitePawn,
                [Positions.A7] = ChessPieces.BlackPawn,
                [Positions.B7] = ChessPieces.BlackPawn,
                [Positions.C7] = ChessPieces.BlackPawn,
                [Positions.D7] = ChessPieces.BlackPawn,
                [Positions.E7] = ChessPieces.BlackPawn,
                [Positions.F7] = ChessPieces.BlackPawn,
                [Positions.G7] = ChessPieces.BlackPawn,
                [Positions.H7] = ChessPieces.BlackPawn,
                [Positions.A8] = ChessPieces.BlackRook,
                [Positions.B8] = ChessPieces.BlackKnight,
                [Positions.C8] = ChessPieces.BlackBishop,
                [Positions.D8] = ChessPieces.BlackQueen,
                [Positions.E8] = ChessPieces.BlackKing,
                [Positions.F8] = ChessPieces.BlackBishop,
                [Positions.G8] = ChessPieces.BlackKnight,
                [Positions.H8] = ChessPieces.BlackRook
            };

            return board;
        }
    }
}
