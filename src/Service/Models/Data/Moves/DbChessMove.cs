using Game.Chess;
using Game.Chess.Pieces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGame.Service.Models.Data.Moves
{
    /// <summary>
    /// Database representation of a chess move. Base class.
    /// </summary>
    [Serializable]
    [Table("ChessMoves")]
    public class DbChessMove
    {
        /// <summary>
        /// Gets or sets the id of the move. (Key)
        /// </summary>
        [Key]
        public Guid MoveId { get; set; }

        /// <summary>
        /// Gets or sets the source row of the move. (Example: 3)
        /// </summary>
        public int FromRow { get; set; }

        /// <summary>
        /// Gets or sets the source column of the move. (Example: 'A')
        /// </summary>
        public string FromColumn { get; set; }

        /// <summary>
        /// Gets the source position calculated from the 'FromRow' and 'FromColumn' properties.
        /// </summary>
        public Position From => new Position(FromColumn[0], FromRow);

        /// <summary>
        /// Gets or sets the destination row of the move.
        /// </summary>
        public int ToRow { get; set; }

        /// <summary>
        /// Gets or sets the destination column of the move.
        /// </summary>
        public string ToColumn { get; set; }

        /// <summary>
        /// Gets the destination position calculated from the 'ToRow' and 'ToColumn' properties.
        /// </summary>
        public virtual Position To => new Position(ToColumn[0], ToRow);

        /// <summary>
        /// Gets or sets a value indicating whether the move was a capture move.
        /// </summary>
        public bool IsCaptureMove { get; set; }

        /// <summary>
        /// Gets or sets the owner of the move. (Black or White.)
        /// </summary>
        public ChessPlayer Owner { get; set; }

        /// <summary>
        /// Gets or sets the moving chess piece kind.
        /// </summary>
        public PieceKind ChessPiece { get; set; }

        /// <summary>
        /// Gets or sets the date of the move.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
