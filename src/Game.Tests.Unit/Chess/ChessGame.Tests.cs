using System.Collections.Generic;
using System.Linq;
using Game.Chess;
using Game.Chess.Moves;
using Game.Chess.Pieces;
using Xunit;

namespace Game.Tests.Unit.Chess
{
    public class ChessGameTests
    {
        [Fact]
        public void GenerateMovesTest_Initial_AppropriateMovesReturned()
        {
            var expected = 20;

            var representation = new ChessRepresentationInitializer().Create();
            
            var game = new ChessMechanism();

            var moves = game.GenerateMoves(representation);
            var result = moves.Count();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateMovesTest_CastlingsReturned_WhiteSide()
        {
            var expected = 2;

            var board = new ChessRepresentation()
            {
                CurrentPlayer = ChessPlayer.White,
                History = new List<BaseMove>(),
                Players = new[] { ChessPlayer.White, ChessPlayer.Black }
            };

            board[Positions.E1] = new King(ChessPlayer.White, false);
            board[Positions.A1] = new Rook(ChessPlayer.White, false);
            board[Positions.H1] = new Rook(ChessPlayer.White, false);
            board[Positions.E8] = new King(ChessPlayer.Black, false);

            var game = new ChessMechanism();

            var result = game.GenerateMoves(board).OfType<KingCastlingMove>().Count();
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateMovesTest_CastlingsReturned_BlackSide()
        {
            var expected = 2;

            var board = new ChessRepresentation()
            {
                CurrentPlayer = ChessPlayer.Black,
                History = new List<BaseMove>(),
                Players = new[] { ChessPlayer.White, ChessPlayer.Black }
            };

            board[Positions.E8] = new King(ChessPlayer.Black, false);
            board[Positions.A8] = new Rook(ChessPlayer.Black, false);
            board[Positions.H8] = new Rook(ChessPlayer.Black, false);
            board[Positions.E1] = new King(ChessPlayer.White, false);

            var game = new ChessMechanism();
            var moves = game.GenerateMoves(board).OfType<KingCastlingMove>();
            var result = moves.Count();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateMovesTest_BishopMovesReturnedWithCapture_WhiteSide()
        {
            var expectedBishopMoves = new[]
            {
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.E5),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.C5),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.E3),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.C3)
            }.ToHashSet();

            var board = new ChessRepresentation()
            {
                CurrentPlayer = ChessPlayer.White,
                History = new List<BaseMove>(),
                Players = new[] { ChessPlayer.White, ChessPlayer.Black }
            };

            board[Positions.D4] = new Bishop(ChessPlayer.White, true);
            board[Positions.F6] = new Pawn(ChessPlayer.White, false);
            board[Positions.B6] = new Queen(ChessPlayer.White, false);
            board[Positions.B2] = new Pawn(ChessPlayer.White, false);
            board[Positions.E3] = new Pawn(ChessPlayer.Black, false);
            board[Positions.E8] = new King(ChessPlayer.Black, false);

            var game = new ChessMechanism();
            var moves = game.GenerateMoves(board).ToList();
            var bishopMoves = moves.OfType<ChessMove>()
                                   .Where(x => x.From == Positions.D4)
                                   .Where(x => x.Owner == ChessPlayer.White)
                                   .ToHashSet();

            Assert.Subset(expectedBishopMoves, bishopMoves);
            Assert.Subset(bishopMoves, expectedBishopMoves);
        }

        [Fact]
        public void GenerateMovesTest_KnightMovesReturnedWithCapture_WhiteSide()
        {
            var expectedKnightMoves = new[]
            {
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.C2),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.E2),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.B3),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.F3),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.F5),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.C6),
                new ChessMove(ChessPlayer.White, Positions.D4, Positions.E6)
            }.ToHashSet();

            var board = new ChessRepresentation()
            {
                CurrentPlayer = ChessPlayer.White,
                History = new List<BaseMove>(),
                Players = new[] { ChessPlayer.White, ChessPlayer.Black }
            };

            board[Positions.D4] = new Knight(ChessPlayer.White, true);
            board[Positions.B5] = new Pawn(ChessPlayer.White, false);
            board[Positions.B2] = new Pawn(ChessPlayer.White, false);
            board[Positions.E6] = new Pawn(ChessPlayer.Black, false);
            board[Positions.F5] = new Pawn(ChessPlayer.Black, false);

            var game = new ChessMechanism();
            var moves = game.GenerateMoves(board).ToList();
            var knightMoves = moves.OfType<ChessMove>()
                                   .Where(x => x.From == Positions.D4)
                                   .Where(x => x.Owner == ChessPlayer.White)
                                   .ToHashSet();

            Assert.Subset(expectedKnightMoves, knightMoves);
            Assert.Subset(knightMoves, expectedKnightMoves);
        }

        [Fact]
        public void GenerateMovesTest_KingMovesInChess_WhiteSide()
        {
            var expectedKnightMoves = new[]
            {
                new ChessMove(ChessPlayer.White, Positions.C4, Positions.E2),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.G3),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.G1),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.H3),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.H1),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.F3),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.F1)
            }.ToHashSet<BaseMove>();

            var board = new ChessRepresentation
            {
                CurrentPlayer = ChessPlayer.White,
                History = new List<BaseMove>(),
                Players = new[] {ChessPlayer.White, ChessPlayer.Black},
                [Positions.G2] = new King(ChessPlayer.White, true),
                [Positions.E2] = new Rook(ChessPlayer.Black, false),
                [Positions.C4] = new Queen(ChessPlayer.White, false),
                [Positions.G3] = new Pawn(ChessPlayer.Black, false)
            };


            var game = new ChessMechanism();
            var moves = game.GenerateMoves(board).ToHashSet();

            Assert.Subset(expectedKnightMoves, moves);
            Assert.Subset(moves, expectedKnightMoves);
        }
    }
}
