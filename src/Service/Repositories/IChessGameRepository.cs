using System;
using System.Collections.Generic;
using BoardGame.Service.Models.Data;
using Game.Chess.Moves;
using Model.Api.ChessGamesControllerModels;

namespace BoardGame.Service.Repositories
{
    /// <summary>
    /// Interface of the chess game repository.
    /// </summary>
    public interface IChessGameRepository
    {
        /// <summary>
        /// Gets the list of chess games according to the given predicate.
        /// </summary>
        /// <param name="participantPlayerName">Username of the participant (either sides) to filter for.</param>
        /// <param name="predicate">Predicate for additional filtering if needed.</param>
        /// <returns>List of chess games.</returns>
        IReadOnlyList<ChessGame> Get(string participantPlayerName, Func<DbChessGame, bool> predicate = null);

        /// <summary>
        /// Gets the list of chess game details according to the given predicate.
        /// </summary>
        /// <param name="participantPlayerName">Username of the participant (either sides) to filter for.</param>
        /// <param name="predicate">Predicate for additional filtering if needed.</param>
        /// <returns>List of detailed chess games.</returns>
        IReadOnlyList<ChessGameDetails> GetDetails(string participantPlayerName, Func<DbChessGame, bool> predicate = null);

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
