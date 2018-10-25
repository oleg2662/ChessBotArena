namespace BoardGame.Service.Models.Api.ChessGamesControllerModels
{
    /// <summary>
    /// Represetsn a challenge request coming from the client.
    /// </summary>
    public class Challenge
    {
        /// <summary>
        /// Gets or sets the id of the challenger player.
        /// </summary>
        public string InitiatedBy { get; set; }

        /// <summary>
        /// Gets or sets the id of the challenged player.
        /// </summary>
        public string Opponent { get; set; }
    }
}
