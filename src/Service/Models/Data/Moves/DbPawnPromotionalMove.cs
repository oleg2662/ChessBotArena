using System;
using BoardGame.Game.Chess.Pieces;

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
