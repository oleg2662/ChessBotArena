namespace Model.Api.PlayerControllerModels
{
    /// <summary>
    /// Represents a player information in the players' screen.
    /// </summary>
    public class Player : IPlayerModel
    {
        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is a bot.
        /// </summary>
        public bool IsBot { get; set; }
    }
}
