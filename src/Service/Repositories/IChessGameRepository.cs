using System;
using System.Collections.Generic;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.ChessGamesControllerModels;

namespace BoardGame.Service.Repositories
{
    /// <summary>
    /// Interface of the chess game repository.
    /// </summary>
    public interface IChessGameRepository
    {
        /// <summary>
        /// Gets the list of chess games for the specified player.
        /// </summary>
        /// <param name="participantPlayerName">Username of the participant (either sides) to filter for.</param>
        /// <returns>List of chess games.</returns>
        IReadOnlyList<ChessGame> GetList(string participantPlayerName);

        /// <summary>
        /// Gets the list of chess games (with details) for the specified player.
        /// </summary>
        /// <param name="participantPlayerName">Username of the participant (either sides) to filter for.</param>
        /// <returns>List of chess games.</returns>
        IReadOnlyList<ChessGameDetails> GetListWithDetails(string participantPlayerName);

        /// <summary>
        /// Gets the selected chess game details.
        /// </summary>
        /// <param name="participantPlayerName">Username of the participant (either sides) to filter for.</param>
        /// <param name="chessGameId">ID of the game.</param>
        /// <returns>List of detailed chess games.</returns>
        ChessGameDetails GetDetails(string participantPlayerName, Guid chessGameId);

        /// <summary>
        /// Validates and saves a new game party according to the supplied challenge request.
        /// </summary>
        /// <param name="participantPlayerName">Username of the participant sending the move request.</param>
        /// <param name="challengeRequest">The request coming from the API.</param>
        /// <returns>Returns the result of the validation and the operation.</returns>
        ChallengeRequestResult Add(string participantPlayerName, ChallengeRequest challengeRequest);

        /// <summary>
        /// Validates and saves a new game party according to the supplied challenge request.
        /// </summary>
        /// <param name="participantPlayerName">Username of the participant sending the move request.</param>
        /// <param name="chessGameId">ID of the game.</param>
        /// <param name="move">The chess move.</param>
        /// <returns>Returns the result of the validation and the operation.</returns>
        ChessGameMoveResult Move<T>(string participantPlayerName, Guid chessGameId, T move) where T : BaseMove;
    }
}
