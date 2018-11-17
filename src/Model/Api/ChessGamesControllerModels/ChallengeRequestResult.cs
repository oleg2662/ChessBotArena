namespace Model.Api.ChessGamesControllerModels
{
    /// <summary>
    /// The result with additional result information.
    /// </summary>
    public class ChallengeRequestResult
    {
        /// <summary>
        /// The result of the addition.
        /// </summary>
        public ChallengeRequestResultStatuses RequestResult { get; set; }

        /// <summary>
        /// The newly created entity if the result was ok.
        /// </summary>
        public ChessGame NewlyCreatedGame { get; set; }
    }
}
