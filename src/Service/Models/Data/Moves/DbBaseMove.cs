using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Game.Chess;

namespace BoardGame.Service.Models.Data.Moves
{
    /// <summary>
    /// Base class for the move representation in the database.
    /// </summary>
    [Serializable]
    [Table("ChessMoves")]
    public class DbBaseMove
    {
        /// <summary>
        /// Gets or sets the id of the move. (Key)
        /// </summary>
        [Key]
        public Guid MoveId { get; set; }

        /// <summary>
        /// Gets or sets the owner of the move. (Black or White.)
        /// </summary>
        public ChessPlayer Owner { get; set; }

        /// <summary>
        /// Gets or sets the date of the move.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}