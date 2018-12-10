using System;
using System.Linq;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Game.Chess.Pieces;
using Xunit;

namespace BoardGame.Game.Tests.Unit.Chess
{
    public class ChessBoardTests
    {
        [Fact]
        public void ChessBoardTest_NewlyCreated_BoardIsEmpty()
        {
            // Arrange
            var board = new ChessRepresentation();

            // Act

            // Assert
            Assert.All(Helpers.PositionList, x => { Assert.Null(board[x]); });
        }

        [Fact]
        public void ChessRepresentation_GetHashCodeTest_AreEqual()
        {
            var representation1 = new ChessRepresentationInitializer().Create();
            var representation2 = new ChessRepresentationInitializer().Create();

            var hash1 = representation1.GetHashCode();
            var hash2 = representation2.GetHashCode();

            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void ChessBoardTest_AddingItem_BoardIsEmptyExceptInGivenPosition()
        {
            // Arrange
            var positionOfChessPiece = Positions.H3;
            var board = new ChessRepresentation();
            var expectedChessPiece = new Bishop(ChessPlayer.Black);
            var emptyPositions = Helpers.PositionList.Where(x => x != positionOfChessPiece);

            // Act
            board[positionOfChessPiece] = expectedChessPiece;

            // Assert
            Assert.All(emptyPositions, x => { Assert.Null(board[x]); });
            Assert.Equal(expectedChessPiece, board[positionOfChessPiece]);
        }

        [Fact]
        public void ChessBoardTest_EqualityCheck_BoardsAreEqual()
        {
            var generatorMechanism = new ChessMechanism();
            var applierMechanism1 = new ChessMechanism();
            var applierMechanism2 = new ChessMechanism();

            var numberOfTries = 10;

            var result = true;

            for (var i = 0; i < numberOfTries; i++)
            {
                var game1 = new ChessRepresentationInitializer().Create();
                var game2 = new ChessRepresentationInitializer().Create();
                var referenceGame = new ChessRepresentationInitializer().Create();

                var count = 0;
                while (true)
                {
                    count++;
                    var move = generatorMechanism.GenerateMoves(referenceGame)
                                                 .OfType<BaseChessMove>()
                                                 .OrderBy(x => Guid.NewGuid())
                                                 .FirstOrDefault();

                    if (move == null || count > 20)
                    {
                        break;
                    }

                    game1 = applierMechanism1.ApplyMove(game1, move);
                    game2 = applierMechanism2.ApplyMove(game2, move);
                    referenceGame = generatorMechanism.ApplyMove(referenceGame, move);
                }

                result = result && game1.Equals(game1);
                result = result && game2.Equals(game2);
                result = result && referenceGame.Equals(referenceGame);

                result = result && game1.Equals(game2);
                result = result && game2.Equals(referenceGame);
                result = result && referenceGame.Equals(game1);

                result = result && game1.Equals(referenceGame);
                result = result && referenceGame.Equals(game2);
                result = result && game2.Equals(game1);
            }

            Assert.True(result);
        }

        [Fact]
        public void ChessBoardTest_SameOutcomeDifferentHistory_BoardsAreNonEqual()
        {
            var mechanism = new ChessMechanism();
            var game1 = new ChessRepresentationInitializer().Create();
            var game2 = new ChessRepresentationInitializer().Create();

            var move1 = new ChessMove(ChessPlayer.White, Positions.B1, Positions.C3);
            var move2 = new ChessMove(ChessPlayer.Black, Positions.B8, Positions.C6);

            game1 = mechanism.ApplyMove(game1, move1);
            game1 = mechanism.ApplyMove(game1, move2);
            game2 = mechanism.ApplyMove(game2, move1);
            game2 = mechanism.ApplyMove(game2, move2);

            var move3 = new ChessMove(ChessPlayer.White, Positions.C3, Positions.E4);
            var move4 = new ChessMove(ChessPlayer.Black, Positions.C6, Positions.E5);
            var move5 = new ChessMove(ChessPlayer.White, Positions.E4, Positions.C3);
            var move6 = new ChessMove(ChessPlayer.Black, Positions.E5, Positions.C6);

            game1 = mechanism.ApplyMove(game1, move3);
            game1 = mechanism.ApplyMove(game1, move4);
            game1 = mechanism.ApplyMove(game1, move5);
            game1 = mechanism.ApplyMove(game1, move6);

            Assert.NotEqual(game1, game2);
        }
    }
}
