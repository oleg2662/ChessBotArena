using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Pieces;
using Xunit;

namespace BoardGame.Game.Tests.Unit.Chess
{
    public class ChessBoardInitializerTests
    {
        [Fact]
        public void ChessBoardInitializerTest_InitBoard_PiecesAtTheirDefaultPositions()
        {
            var initializer = new ChessRepresentationInitializer();
            var board = initializer.Create();

            Assert.Equal(ChessPieces.WhiteRook, board[Positions.A1]);
            Assert.Equal(ChessPieces.WhiteKnight, board[Positions.B1]);
            Assert.Equal(ChessPieces.WhiteBishop, board[Positions.C1]);
            Assert.Equal(ChessPieces.WhiteQueen, board[Positions.D1]);
            Assert.Equal(ChessPieces.WhiteKing, board[Positions.E1]);
            Assert.Equal(ChessPieces.WhiteBishop, board[Positions.F1]);
            Assert.Equal(ChessPieces.WhiteKnight, board[Positions.G1]);
            Assert.Equal(ChessPieces.WhiteRook, board[Positions.H1]);

            Assert.Equal(ChessPieces.WhitePawn, board[Positions.A2]);
            Assert.Equal(ChessPieces.WhitePawn, board[Positions.B2]);
            Assert.Equal(ChessPieces.WhitePawn, board[Positions.C2]);
            Assert.Equal(ChessPieces.WhitePawn, board[Positions.D2]);
            Assert.Equal(ChessPieces.WhitePawn, board[Positions.E2]);
            Assert.Equal(ChessPieces.WhitePawn, board[Positions.F2]);
            Assert.Equal(ChessPieces.WhitePawn, board[Positions.G2]);
            Assert.Equal(ChessPieces.WhitePawn, board[Positions.H2]);

            Assert.Null(board[Positions.A3]);
            Assert.Null(board[Positions.B3]);
            Assert.Null(board[Positions.C3]);
            Assert.Null(board[Positions.D3]);
            Assert.Null(board[Positions.E3]);
            Assert.Null(board[Positions.F3]);
            Assert.Null(board[Positions.G3]);
            Assert.Null(board[Positions.H3]);

            Assert.Null(board[Positions.A4]);
            Assert.Null(board[Positions.B4]);
            Assert.Null(board[Positions.C4]);
            Assert.Null(board[Positions.D4]);
            Assert.Null(board[Positions.E4]);
            Assert.Null(board[Positions.F4]);
            Assert.Null(board[Positions.G4]);
            Assert.Null(board[Positions.H4]);

            Assert.Null(board[Positions.A5]);
            Assert.Null(board[Positions.B5]);
            Assert.Null(board[Positions.C5]);
            Assert.Null(board[Positions.D5]);
            Assert.Null(board[Positions.E5]);
            Assert.Null(board[Positions.F5]);
            Assert.Null(board[Positions.G5]);
            Assert.Null(board[Positions.H5]);

            Assert.Null(board[Positions.A6]);
            Assert.Null(board[Positions.B6]);
            Assert.Null(board[Positions.C6]);
            Assert.Null(board[Positions.D6]);
            Assert.Null(board[Positions.E6]);
            Assert.Null(board[Positions.F6]);
            Assert.Null(board[Positions.G6]);
            Assert.Null(board[Positions.H6]);

            Assert.Equal(ChessPieces.BlackPawn, board[Positions.A7]);
            Assert.Equal(ChessPieces.BlackPawn, board[Positions.B7]);
            Assert.Equal(ChessPieces.BlackPawn, board[Positions.C7]);
            Assert.Equal(ChessPieces.BlackPawn, board[Positions.D7]);
            Assert.Equal(ChessPieces.BlackPawn, board[Positions.E7]);
            Assert.Equal(ChessPieces.BlackPawn, board[Positions.F7]);
            Assert.Equal(ChessPieces.BlackPawn, board[Positions.G7]);
            Assert.Equal(ChessPieces.BlackPawn, board[Positions.H7]);

            Assert.Equal(ChessPieces.BlackRook, board[Positions.A8]);
            Assert.Equal(ChessPieces.BlackKnight, board[Positions.B8]);
            Assert.Equal(ChessPieces.BlackBishop, board[Positions.C8]);
            Assert.Equal(ChessPieces.BlackQueen, board[Positions.D8]);
            Assert.Equal(ChessPieces.BlackKing, board[Positions.E8]);
            Assert.Equal(ChessPieces.BlackBishop, board[Positions.F8]);
            Assert.Equal(ChessPieces.BlackKnight, board[Positions.G8]);
            Assert.Equal(ChessPieces.BlackRook, board[Positions.H8]);
        }
    }
}
