using Game.Chess.Pieces;
using System.Collections.Generic;

namespace Game.Chess
{
    public interface IChessBoard
    {
        /// <summary>
        /// Algebraic notation accessor.
        /// </summary>
        /// <param name="col">Character of the column. Can be A..H.</param>
        /// <param name="row">The row number. Can be between 1..8.</param>
        /// <returns>The chess piece on the given field. Returns a null chess piece object if empty. Throws exception if column or row id isn't valid.</returns>
        ChessPiece this[Position position] { get; }

        ChessPlayer CurrentPlayer { get; }

        IEnumerable<ChessPlayer> Players { get; }
    }
}
