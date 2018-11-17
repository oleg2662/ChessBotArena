namespace BoardGame.Game.Chess
{
    /// <summary>
    /// Actual state of the game.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Game is ongoing.
        /// </summary>
        InProgress = 0,

        /// <summary>
        /// White won.
        /// </summary>
        WhiteWon = 1,

        /// <summary>
        /// Black won.
        /// </summary>
        BlackWon = 2,

        /// <summary>
        /// It was a draw.
        /// </summary>
        Draw = 3
    }
}
