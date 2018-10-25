namespace BoardGame.Service.Repositories
{
    /// <summary>
    /// Validation and DB operation results for the <see cref="IChessGameRepository" />.
    /// </summary>
    public enum ChallengeRequestResults
    {
        /// <summary>
        /// Challenge request accepted.
        /// </summary>
        OK = 1,

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
