using Game.Chess;

namespace Model.Api.ChessGamesControllerModels
{
    /// <summary>
    /// Detailed information about a chess game.
    /// </summary>
    public class ChessGameDetails : ChessGame
    {
        /// <summary>
        /// Gets or sets the current representation of a match and it's current state.
        /// </summary>
        public ChessRepresentation Representation { get; set; }
    }
}
