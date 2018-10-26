namespace BoardGame.Service.Models.Api.ChessGamesControllerModels
{
    /// <summary>
    /// Detailed information about a chess game.
    /// </summary>
    public class ChessGameDetails : ChessGame
    {
        public ChessGame MyProperty { get; set; }
    }
}
