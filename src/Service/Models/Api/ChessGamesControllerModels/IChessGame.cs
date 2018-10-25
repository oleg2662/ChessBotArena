using System;

namespace BoardGame.Service.Models.Api.ChessGamesControllerModels
{
    /// <summary>
    /// Interface to a chess game item in the chess games controller.
    /// </summary>
    public interface IChessGame
    {
        /// <summary>
        /// Gets or sets the ID of the game.
        /// </summary>
        int Id { get; set; }

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
        /// Gets or sets the date the challenge was sent.
        /// </summary>
        DateTime ChallengeDate { get; set; }

        /// <summary>
        /// Gets or sets the date of the last move. Initially set to the challenge date.
        /// </summary>
        DateTime LastMoveDate { get; set; }
    }
}
