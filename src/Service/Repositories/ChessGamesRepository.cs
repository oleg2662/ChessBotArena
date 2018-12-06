using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.ChessGamesControllerModels;
using Microsoft.EntityFrameworkCore;
using BoardGame.Service.Data;
using BoardGame.Service.Models.Converters;
using BoardGame.Service.Models.Data;
using BoardGame.Service.Extensions;

namespace BoardGame.Service.Repositories
{
    /// <summary>
    /// Implementation of the chess game repository.
    /// </summary>
    public class ChessGameRepository : IChessGameRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IChessGameRepositoryConverter _chessGameConverter;

        /// <summary>
        /// Initializes a new instance of the chess game repository.
        /// </summary>
        /// <param name="dbContext">The database context to be used to query the data.</param>
        /// <param name="chessGameConverter">Converter which transforms the inner database-near model to API model.</param>
        public ChessGameRepository(ApplicationDbContext dbContext, IChessGameRepositoryConverter chessGameConverter)
        {
            _dbContext = dbContext;
            _chessGameConverter = chessGameConverter;
        }

        /// <inheritdoc />
        public IReadOnlyList<ChessGame> GetList(string participantPlayerName)
        {
            var result = GetMainQuery(participantPlayerName)
                .Select(x => _chessGameConverter.ConvertToChessGame(x))
                .ToList()
                .AsReadOnly();

            return result;
        }


        /// <inheritdoc />
        public IReadOnlyList<ChessGameDetails> GetListWithDetails(string participantPlayerName)
        {
            var result = GetMainQuery(participantPlayerName)
                .Include(x => x.History)
                .Select(x => _chessGameConverter.ConvertToChessGameDetails(x))
                .ToList()
                .AsReadOnly();

            return result;
        }

        /// <inheritdoc />
        public ChallengeRequestResult Add(string participantPlayerName, ChallengeRequest challengeRequest)
        {
            var initiatedBy = _dbContext.Users.SingleOrDefault(x => x.UserName == participantPlayerName);

            if (initiatedBy == null)
            {
                return new ChallengeRequestResult
                {
                    RequestResult = ChallengeRequestResultStatuses.InitiatedByUserNull,
                    NewlyCreatedGame = null
                };
            }

            var opponent = _dbContext.Users.SingleOrDefault(x => x.UserName == challengeRequest.Opponent);

            if (opponent == null)
            {
                return new ChallengeRequestResult
                {
                    RequestResult = ChallengeRequestResultStatuses.OpponentNull,
                    NewlyCreatedGame = null
                };
            }

            var now = DateTime.UtcNow;

            // Randomize sides
            var players = new[] { initiatedBy, opponent }.OrderBy(x => Guid.NewGuid()).ToArray();
            var white = players[0];
            var black = players[1];

            var newGame = new DbChessGame()
            {
                ChallengeDate = now,
                InitiatedBy = initiatedBy,
                Opponent = opponent,
                WhitePlayer = white,
                BlackPlayer = black,
                Name = $"{initiatedBy.UserName} vs {opponent.UserName}",
                LastMoveDate = now,
                Status = GameState.InProgress
            };

            var newEntity = _dbContext.Add(newGame).Entity;
            _dbContext.SaveChanges();

            return new ChallengeRequestResult
            {
                RequestResult = ChallengeRequestResultStatuses.Ok,
                NewlyCreatedGame = _chessGameConverter.ConvertToChessGameDetails(newEntity)
            };
        }

        /// <inheritdoc />
        public ChessGameDetails GetDetails(string participantPlayerName, Guid chessGameId)
        {
            var result = GetMainQuery(participantPlayerName)
                .Where(x => x.Id == chessGameId)
                .Include(x => x.History)
                .Select(x => _chessGameConverter.ConvertToChessGameDetails(x))
                .FirstOrDefault();

            return result;
        }

        /// <inheritdoc />
        public ChessGameMoveResult Move<T>(string participantPlayerName, Guid chessGameId, T move) where T : BaseMove
        {
            var match = GetMainQuery(participantPlayerName)
                            .Include(x => x.History)
                            .FirstOrDefault(x => x.Id == chessGameId);

            if (match is null)
            {
                return new ChessGameMoveResult
                {
                    NewState = null,
                    MoveRequestResultStatus = ChessGameMoveRequestResultStatuses.NoMatchFound
                };
            }

            var players = match.GetPlayerNames();

            var oldChessGameDetails = _chessGameConverter.ConvertToChessGameDetails(match);

            var game = oldChessGameDetails.Representation;
            var gameMechanism = new ChessMechanism();

            // Game has already ended, not accepting any more moves!
            if (gameMechanism.GetGameState(game) != GameState.InProgress)
            {
                return new ChessGameMoveResult
                {
                    MoveRequestResultStatus = ChessGameMoveRequestResultStatuses.GameHasAlreadyEnded,
                    NewState = null
                };
            }

            // Checking if the current player tries to move.
            var currentPlayerName = players[game.CurrentPlayer];
            if (currentPlayerName != participantPlayerName)
            {
                return new ChessGameMoveResult
                {
                    MoveRequestResultStatus = ChessGameMoveRequestResultStatuses.WrongTurn,
                    NewState = null
                };
            }

            // Validating move...
            if (!gameMechanism.ValidateMove(game, move))
            {
                var previousState = new ChessGameDetails
                {
                    BlackPlayer = oldChessGameDetails.BlackPlayer,
                    ChallengeDate = oldChessGameDetails.ChallengeDate,
                    Id = oldChessGameDetails.Id,
                    InitiatedBy = oldChessGameDetails.InitiatedBy,
                    LastMoveDate = oldChessGameDetails.LastMoveDate,
                    Name = oldChessGameDetails.Name,
                    Opponent = oldChessGameDetails.Opponent,
                    WhitePlayer = oldChessGameDetails.WhitePlayer,
                    Representation = game
                };

                return new ChessGameMoveResult
                {
                    MoveRequestResultStatus = ChessGameMoveRequestResultStatuses.InvalidMove,
                    NewState = previousState
                };
            }

            // Everything seems fine, applying and returning...
            var newState = gameMechanism.ApplyMove(game, move);
            var newStateOutcome = gameMechanism.GetGameState(newState);

            var result = new ChessGameDetails()
            {
                BlackPlayer = oldChessGameDetails.BlackPlayer,
                ChallengeDate = oldChessGameDetails.ChallengeDate,
                Id = oldChessGameDetails.Id,
                InitiatedBy = oldChessGameDetails.InitiatedBy,
                LastMoveDate = DateTime.UtcNow,
                Name = oldChessGameDetails.Name,
                Opponent = oldChessGameDetails.Opponent,
                WhitePlayer = oldChessGameDetails.WhitePlayer,
                Representation = newState
            };

            var newDbMove = _chessGameConverter.CovertToDbChessMove(move);
            var dbGame = _dbContext.ChessGames.Find(match.Id);
            dbGame.Status = newStateOutcome;
            dbGame.History.Add(newDbMove);
            _dbContext.SaveChanges();

            return new ChessGameMoveResult
            {
                MoveRequestResultStatus = ChessGameMoveRequestResultStatuses.Ok,
                NewState = result
            };
        }

        private IQueryable<DbChessGame> GetMainQuery(string participantPlayerName)
        {
            var query = _dbContext.ChessGames
                .Include(x => x.InitiatedBy)
                .Include(x => x.Opponent)
                .Where(x => x.InitiatedBy != null && x.InitiatedBy.UserName == participantPlayerName
                            || x.Opponent != null && x.Opponent.UserName == participantPlayerName);

            return query;
        }
    }
}
