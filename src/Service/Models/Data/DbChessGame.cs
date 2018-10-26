using System;

namespace BoardGame.Service.Models.Data
{
    /// <inheritdoc />
    public class DbChessGame : IDbChessGame
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public ApplicationUser InitiatedBy { get; set; }

        /// <inheritdoc />
        public ApplicationUser Opponent { get; set; }

        /// <inheritdoc />
        public ApplicationUser WhitePlayer { get; set; }

        /// <inheritdoc />
        public ApplicationUser BlackPlayer { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public DateTime ChallengeDate { get; set; }

        /// <inheritdoc />
        public DateTime LastMoveDate { get; set; }

    }
}
