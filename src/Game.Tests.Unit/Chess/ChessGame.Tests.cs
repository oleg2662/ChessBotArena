using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Exceptions;
using BoardGame.Game.Chess.Moves;
using BoardGame.Game.Chess.Pieces;
using Xunit;

namespace BoardGame.Game.Tests.Unit.Chess
{
    public class ChessGameTests
    {
        [Fact]
        public void GenerateMovesTest_Initial_AppropriateMovesReturned()
        {
            var expected = 21;

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
            var expectedKnightMoves = new BaseMove[]
            {
                new SpecialMove(ChessPlayer.White, MessageType.Resign),
                new ChessMove(ChessPlayer.White, Positions.C4, Positions.E2),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.G3),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.G1),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.H3),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.H1),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.F3),
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.F1)
            }.ToHashSet();

            var board = new ChessRepresentation
            {
                CurrentPlayer = ChessPlayer.White,
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

        [Fact]
        public void GameStateCheck_CheckMateTest_BlackWins()
        {
            var expected = GameState.BlackWon;

            // Fool's mate
            var game = new ChessRepresentationInitializer().Create();
            var mechanism = new ChessMechanism();
            var moves = new List<BaseMove>(4)
            {
                new ChessMove(ChessPlayer.White, Positions.G2, Positions.G4),
                new ChessMove(ChessPlayer.Black, Positions.E7, Positions.E5),
                new ChessMove(ChessPlayer.White, Positions.F2, Positions.F3),
                new ChessMove(ChessPlayer.Black, Positions.D8, Positions.H4)
            };

            foreach (var move in moves)
            {
                game = mechanism.ApplyMove(game, move);
            }

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_CheckMateTest_WhiteWins()
        {
            var expected = GameState.WhiteWon;

            //Scholar's Mate
            var game = new ChessRepresentationInitializer().Create();
            var mechanism = new ChessMechanism();
            var moves = new List<BaseMove>(7)
            {
                new ChessMove(ChessPlayer.White, Positions.E2, Positions.E4),
                new ChessMove(ChessPlayer.Black, Positions.E7, Positions.E5),
                new ChessMove(ChessPlayer.White, Positions.F1, Positions.C4),
                new ChessMove(ChessPlayer.Black, Positions.B8, Positions.C6),
                new ChessMove(ChessPlayer.White, Positions.D1, Positions.H5),
                new ChessMove(ChessPlayer.Black, Positions.G8, Positions.F6),
                new ChessMove(ChessPlayer.White, Positions.H5, Positions.F7)
            };

            foreach (var move in moves)
            {
                game = mechanism.ApplyMove(game, move);
            }

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_StaleMateTest_Draw()
        {
            var expected = GameState.Draw;

            //Stalemate
            var game = new ChessRepresentationInitializer().Create();
            var mechanism = new ChessMechanism();
            var moves = new List<BaseMove>(19)
            {
                new ChessMove(ChessPlayer.White, Positions.E2, Positions.E3),
                new ChessMove(ChessPlayer.Black, Positions.A7, Positions.A5),
                new ChessMove(ChessPlayer.White, Positions.D1, Positions.H5),
                new ChessMove(ChessPlayer.Black, Positions.A8, Positions.A6),
                new ChessMove(ChessPlayer.White, Positions.H5, Positions.A5),
                new ChessMove(ChessPlayer.Black, Positions.H7, Positions.H5),
                new ChessMove(ChessPlayer.White, Positions.H2, Positions.H4),
                new ChessMove(ChessPlayer.Black, Positions.A6, Positions.H6),
                new ChessMove(ChessPlayer.White, Positions.A5, Positions.C7),
                new ChessMove(ChessPlayer.Black, Positions.F7, Positions.F6),
                new ChessMove(ChessPlayer.White, Positions.C7, Positions.D7),
                new ChessMove(ChessPlayer.Black, Positions.E8, Positions.F7),
                new ChessMove(ChessPlayer.White, Positions.D7, Positions.B7),
                new ChessMove(ChessPlayer.Black, Positions.D8, Positions.D3),
                new ChessMove(ChessPlayer.White, Positions.B7, Positions.B8),
                new ChessMove(ChessPlayer.Black, Positions.D3, Positions.H7),
                new ChessMove(ChessPlayer.White, Positions.B8, Positions.C8),
                new ChessMove(ChessPlayer.Black, Positions.F7, Positions.G6),
                new ChessMove(ChessPlayer.White, Positions.C8, Positions.E6)
            };

            foreach (var move in moves)
            {
                game = mechanism.ApplyMove(game, move);
            }

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_KingVsKing_Draw()
        {
            var expected = GameState.Draw;

            // Draw
            var game = new ChessRepresentation()
            {
                History = new List<BaseMove>(),
                ["C5"] = new King(ChessPlayer.White, true),
                ["F4"] = new King(ChessPlayer.Black, true)
            };
            var mechanism = new ChessMechanism();

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_KingBishopVsKingBishopSameColour_Draw()
        {
            var expected = GameState.Draw;

            // Draw
            var game = new ChessRepresentation()
            {
                History = new List<BaseMove>(),
                ["C5"] = new King(ChessPlayer.White, true),
                ["F4"] = new King(ChessPlayer.Black, true),
                ["C8"] = new Bishop(ChessPlayer.White, true),
                ["H1"] = new Bishop(ChessPlayer.Black, true),
            };
            var mechanism = new ChessMechanism();

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_KingBishopVsKingBishopDifferentColour_InProgress()
        {
            var expected = GameState.InProgress;

            // Draw
            var game = new ChessRepresentation()
            {
                History = new List<BaseMove>(),
                ["C5"] = new King(ChessPlayer.White, true),
                ["F4"] = new King(ChessPlayer.Black, true),
                ["C8"] = new Bishop(ChessPlayer.White, true),
                ["G1"] = new Bishop(ChessPlayer.Black, true),
            };
            var mechanism = new ChessMechanism();

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_BlackKingVsWhiteKingBishop_Draw()
        {
            var expected = GameState.Draw;

            // Draw
            var game = new ChessRepresentation()
            {
                History = new List<BaseMove>(),
                ["C5"] = new King(ChessPlayer.White, true),
                ["F4"] = new King(ChessPlayer.Black, true),
                ["C8"] = new Bishop(ChessPlayer.White, true)
            };
            var mechanism = new ChessMechanism();

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_WhiteKingVsBlackKingBishop_Draw()
        {
            var expected = GameState.Draw;

            // Draw
            var game = new ChessRepresentation()
            {
                History = new List<BaseMove>(),
                ["C5"] = new King(ChessPlayer.Black, true),
                ["F4"] = new King(ChessPlayer.White, true),
                ["C8"] = new Bishop(ChessPlayer.Black, true)
            };
            var mechanism = new ChessMechanism();

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_BlackKingVsWhiteKingKnight_Draw()
        {
            var expected = GameState.Draw;

            // Draw
            var game = new ChessRepresentation()
            {
                History = new List<BaseMove>(),
                ["C5"] = new King(ChessPlayer.White, true),
                ["F4"] = new King(ChessPlayer.Black, true),
                ["C8"] = new Knight(ChessPlayer.White, true)
            };
            var mechanism = new ChessMechanism();

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameStateCheck_WhiteKingVsBlackKingKnight_Draw()
        {
            var expected = GameState.Draw;

            // Draw
            var game = new ChessRepresentation()
            {
                History = new List<BaseMove>(),
                ["C5"] = new King(ChessPlayer.Black, true),
                ["F4"] = new King(ChessPlayer.White, true),
                ["C8"] = new Knight(ChessPlayer.Black, true)
            };
            var mechanism = new ChessMechanism();

            var actual = mechanism.GetGameState(game);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(InvalidMovesTestData))]
        public void ValidationTest_InvalidMove_IllegalExceptionThrown(BaseMove move)
        {
            var game = new ChessRepresentationInitializer().Create();
            var mechanism = new ChessMechanism();

            Assert.Throws<ChessIllegalMoveException>(() => { mechanism.ApplyMove(game, move); });
        }

        public static IEnumerable<object[]> InvalidMovesTestData =>
            new List<object[]>
            {
                new object[] { new ChessMove(ChessPlayer.White, Positions.A1, Positions.B7) },
                new object[] { new ChessMove(ChessPlayer.Black, Positions.A1, Positions.B7) },
                new object[] { new ChessMove(ChessPlayer.Black, Positions.A7, Positions.A6) }
            };
    }
}
