namespace BoardGame.Model.Api.LadderControllerModels
{
    /// <summary>
    /// Represents a row in the ladder.
    /// </summary>
    public class LadderItem
    {
        /// <summary>
        /// Gets or sets the place of the player in the ladder.
        /// </summary>
        public int Place { get; set; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is a bot or not.
        /// </summary>
        public bool IsBot { get; set; }

        /// <summary>
        /// Gets or sets the number of points the player has.
        /// It contains the averages points gathered in a ply according to all the played matches.
        /// </summary>
        public decimal Points { get; set; }
    }
}
