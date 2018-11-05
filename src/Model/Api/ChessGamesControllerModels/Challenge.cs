namespace Model.Api.ChessGamesControllerModels
{
    /// <summary>
    /// Represents a challenge request coming from the client.
    /// </summary>
    public class Challenge
    {
        /// <summary>
        /// Gets or sets the id of the challenged player.
        /// </summary>
        public string Opponent { get; set; }
    }
}
