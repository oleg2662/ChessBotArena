using Game.Chess.Pieces;
using System;

namespace BoardGame.Service.Models.Data.Moves
{
    /// <summary>
    /// Represents a prawn promotional move in the database.
    /// </summary>
    [Serializable]
    public class DbPawnPromotionalMove : DbChessMove
    {
        /// <summary>
        /// Gets or sets what kind of chess piece the pawn got promoted to.
        /// </summary>
        public PieceKind PromoteTo { get; set; }
    }
}
