using System;
using BoardGame.Game.Chess.Moves;

namespace BoardGame.Service.Models.Data.Moves
{
    /// <summary>
    /// Represents the special (messaging) type moves in the chess game in the database.
    /// </summary>
    [Serializable]
    public class DbSpecialMove : DbBaseMove
    {
        /// <summary>
        /// Gets or sets the content of the special messaging move.
        /// </summary>
        public MessageType Message { get; set; }
    }
}