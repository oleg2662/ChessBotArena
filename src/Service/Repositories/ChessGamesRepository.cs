using BoardGame.Service.Data;
using BoardGame.Service.Models.Api.ChessGamesControllerModels;
using BoardGame.Service.Models.Converters;
using BoardGame.Service.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="chessGameConveter">Converter which transforms the inner database-near model to API model.</param>
        public ChessGameRepository(ApplicationDbContext dbContext, IChessGameRepositoryConverter chessGameConveter)
        {
            _dbContext = dbContext;
            _chessGameConverter = chessGameConveter;
        }

        /// <inheritdoc />
        public IReadOnlyList<ChessGame> Get(string participantPlayerName, Func<DbChessGame, bool> predicate = null)
        {
            var result = _dbContext.ChessGames
                                   .Include(x => x.InitiatedBy)
                                   .Include(x => x.Opponent)
                                   .Where(x => x.InitiatedBy != null && x.InitiatedBy.UserName == participantPlayerName
                                               || x.Opponent != null && x.Opponent.UserName == participantPlayerName)
                                   .Where(predicate ?? (x => true))
                                   .Select(x => _chessGameConverter.ConvertToChessGame(x))
                                   .ToList()
                                   .AsReadOnly();

            return result;
        }

        /// <inheritdoc />
        public ChessGameRepositoryAddResult Add(Challenge challengeRequest)
        {
            var initiatedBy = _dbContext.Users.SingleOrDefault(x => x.Id == challengeRequest.InitiatedBy);

            if(initiatedBy == null)
            {
                return new ChessGameRepositoryAddResult
                {
                    RequestResult = ChallengeRequestResults.InitiatedByUserNull,
                    NewlyCreatedGame = null
                };
            }

            var opponent = _dbContext.Users.SingleOrDefault(x => x.Id == challengeRequest.Opponent);

            if (opponent == null)
            {
                return new ChessGameRepositoryAddResult
                {
                    RequestResult = ChallengeRequestResults.OpponentNull,
                    NewlyCreatedGame = null
                };
            }

            var now = DateTime.Now.ToUniversalTime();

            var newGame = new DbChessGame()
            {
                ChallengeDate = now,
                InitiatedBy = initiatedBy,
                Opponent = opponent,
                Name = $"{initiatedBy.UserName} vs {opponent.UserName}",
                LastMoveDate = now
            };

            var newEntity =_dbContext.Add(newGame).Entity;
            _dbContext.SaveChanges();

            return new ChessGameRepositoryAddResult
            {
                RequestResult = ChallengeRequestResults.OK,
                NewlyCreatedGame = _chessGameConverter.ConvertToChessGameDetails(newEntity)
            };
        }

        /// <inheritdoc />
        public IReadOnlyList<ChessGameDetails> GetDetails(string participantPlayerName, Func<DbChessGame, bool> predicate = null)
        {
            var result = _dbContext.ChessGames
                                   .Include(x => x.InitiatedBy)
                                   .Include(x => x.Opponent)
                                   .Where(x => x.InitiatedBy != null && x.InitiatedBy.UserName == participantPlayerName
                                               || x.Opponent != null && x.Opponent.UserName == participantPlayerName)
                                   .Where(predicate ?? (x => true))
                                   .Select(x => _chessGameConverter.ConvertToChessGameDetails(x))
                                   .ToList()
                                   .AsReadOnly();

            return result;
        }
    }
}
