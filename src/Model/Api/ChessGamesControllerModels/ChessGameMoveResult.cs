namespace Model.Api.ChessGamesControllerModels
{
    /// <summary>
    /// The result with additional result information.
    /// </summary>
    public class ChessGameMoveResult
    {
        /// <summary>
        /// The result of the addition.
        /// </summary>
        public ChessGameMoveRequestResultStatuses MoveRequestResultStatus { get; set; }

        /// <summary>
        /// The newly created entity if the result was ok.
        /// </summary>
        public ChessGameDetails NewState { get; set; }
    }
}
