namespace Model.Api.ChessGamesControllerModels
{
    /// <summary>
    /// Validation results for the chess move request.
    /// </summary>
    public enum ChessGameMoveRequestResultStatuses
    {
        /// <summary>
        /// Move is valid and applied.
        /// </summary>
        Ok = 1,

        /// <summary>
        /// It's not your turn!
        /// </summary>
        WrongTurn = -1,

        /// <summary>
        /// Invalid move!
        /// </summary>
        InvalidMove = -2,

        /// <summary>
        /// Couldn't find match!
        /// </summary>
        NoMatchFound = -3,

        /// <summary>
        /// Multiple matches found. (DB error)
        /// </summary>
        MultipleMatchesFound = -4,

        /// <summary>
        /// Game has already ended.
        /// </summary>
        GameHasAlreadyEnded = -5
    }
}
