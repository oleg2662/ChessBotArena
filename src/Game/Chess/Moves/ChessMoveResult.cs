namespace Game.Chess.Moves
{
    /// <summary>
    /// Represents the result the move has caused.
    /// Example: chess.
    /// </summary>
    public enum ChessMoveResult
    {
        /// <summary>
        /// No result.
        /// </summary>
        Nothing = 0,

        /// <summary>
        /// Move causes check
        /// </summary>
        Check = 1,

        /// <summary>
        /// Move causes check-mate
        /// </summary>
        CheckMate = 2
    }
}
