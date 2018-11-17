using BoardGame.Service.Models.Data.Moves;
using System;
using System.Collections.Generic;
using BoardGame.Game.Chess;

namespace BoardGame.Service.Models.Data
{
    /// <summary>
    /// Interface to a chess game item in the chess games controller.
    /// </summary>
    public interface IDbChessGame
    {
        /// <summary>
        /// Gets or sets the id of the 
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the user who have initiated the game. (Challenged someone.)
        /// </summary>
        ApplicationUser InitiatedBy { get; set; }

        /// <summary>
        /// Gets or sets the user who was challenged by the game initiator.
        /// </summary>
        ApplicationUser Opponent { get; set; }

        /// <summary>
        /// Gets or sets the white player.
        /// </summary>
        ApplicationUser WhitePlayer { get; set; }

        /// <summary>
        /// Gets or sets the black player.
        /// </summary>
        ApplicationUser BlackPlayer { get; set; }

        /// <summary>
        /// Gets or sets the date the challenge was sent.
        /// </summary>
        DateTime ChallengeDate { get; set; }

        /// <summary>
        /// Gets or sets the date of the last move. Initially set to the challenge date.
        /// </summary>
        DateTime LastMoveDate { get; set; }

        /// <summary>
        /// Gets or sets the history of the match.
        /// </summary>
        ICollection<DbBaseMove> History { get; set; }

        /// <summary>
        /// Gets or sets the status of the game.
        /// </summary>
        GameState Status { get; set; }
    }
}
