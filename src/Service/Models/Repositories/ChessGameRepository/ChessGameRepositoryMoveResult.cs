using Model.Api.ChessGamesControllerModels;

namespace BoardGame.Service.Models.Repositories.ChessGameRepository
{
    /// <summary>
    /// The result with additional result information.
    /// </summary>
    public class ChessGameRepositoryMoveResult
    {
        /// <summary>
        /// The result of the addition.
        /// </summary>
        public MoveRequestResults RequestResult { get; set; }

        /// <summary>
        /// The newly created entity if the result was ok.
        /// </summary>
        public ChessGameDetails NewState { get; set; }
    }
}
