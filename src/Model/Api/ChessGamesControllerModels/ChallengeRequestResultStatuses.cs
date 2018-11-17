namespace BoardGame.Model.Api.ChessGamesControllerModels
{
    /// <summary>
    /// Validation and DB operation results for the <see cref="IChessGameRepository" />.
    /// </summary>
    public enum ChallengeRequestResultStatuses
    {
        /// <summary>
        /// Challenge request accepted.
        /// </summary>
        Ok = 1,

        /// <summary>
        /// The supplied id for the game initiator couldn't be found.
        /// </summary>
        InitiatedByUserNull = -1,

        /// <summary>
        /// The supplied id for the opponent couldn't be found.
        /// </summary>
        OpponentNull = -2
    }
}
