namespace BoardGame.Service.Models.Data
{
    /// <summary>
    /// Represents the game statuses.
    /// </summary>
    public enum DbChessGameStatus
    {
        /// <summary>
        /// The game is ongoing.
        /// </summary>
        Ongoing = 1,

        /// <summary>
        /// The game has ended in a draw.
        /// </summary>
        Draw = 0,

        /// <summary>
        /// White won the game.
        /// </summary>
        WhiteWins = -1,

        /// <summary>
        /// Black won the game.
        /// </summary>
        BlackWins = -2,
    }
}