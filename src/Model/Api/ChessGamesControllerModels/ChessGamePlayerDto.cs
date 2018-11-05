namespace Model.Api.ChessGamesControllerModels
{
    /// <summary>
    /// Represents the minimum required information about the player for the chess games list controller.
    /// </summary>
    public class ChessGamePlayerDto
    {
        /// <summary>
        /// Gets or sets the id of the player user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the player user.
        /// </summary>
        public string UserName { get; set; }
    }
}
