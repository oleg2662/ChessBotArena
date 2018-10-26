namespace Game.Tests.Unit.Chess
{
    using System.Linq;

    using Game.Chess;
    using Game.Chess.Pieces;
    using Xunit;

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
    }
}
