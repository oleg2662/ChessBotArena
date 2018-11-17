namespace BoardGame.Model.Api.PlayerControllerModels
{
    /// <summary>
    /// Interface of the player's model.
    /// </summary>
    public interface IPlayerModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the player is a bot or not.
        /// </summary>
        bool IsBot { get; set; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        string Name { get; set; }
    }
}