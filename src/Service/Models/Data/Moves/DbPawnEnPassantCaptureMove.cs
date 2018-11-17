using System;
using BoardGame.Game.Chess;

namespace BoardGame.Service.Models.Data.Moves
{
    /// <summary>
    /// Represents an en passant capture move in the database.
    /// </summary>
    [Serializable]
    public class DbPawnEnPassantMove : DbChessMove
    {
        /// <summary>
        /// Gets the capture position.
        /// </summary>
        public Position CapturePosition => new Position(CapturePositionColumn[0], CapturePositionRow);

        /// <summary>
        /// Gets or sets the capture position row.
        /// </summary>
        public int CapturePositionRow { get; set; }

        /// <summary>
        /// Gets or sets the capture position column.
        /// </summary>
        public string CapturePositionColumn { get; set; }
    }
}
