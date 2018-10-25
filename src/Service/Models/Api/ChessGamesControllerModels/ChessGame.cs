using System;

namespace BoardGame.Service.Models.Api.ChessGamesControllerModels
{
    /// <inheritdoc />
    public class ChessGame : IChessGame
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public ApplicationUser InitiatedBy { get; set; }

        /// <inheritdoc />
        public ApplicationUser Opponent { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public DateTime ChallengeDate { get; set; }

        /// <inheritdoc />
        public DateTime LastMoveDate { get; set; }
    }
}
