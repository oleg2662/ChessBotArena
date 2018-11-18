using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Game.Chess;
using BoardGame.Model.Api.LadderControllerModels;
using BoardGame.Service.Data;
using BoardGame.Service.Models.Data;
using BoardGame.Service.Models.Data.Moves;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.Service.Repositories
{
    /// <summary>
    /// The ladder repository returning the actual standing of human and bot players.
    /// </summary>
    internal class LadderRepository : ILadderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ladder repository.
        /// </summary>
        /// <param name="dbContext">The database context to be used to query the data.</param>
        public LadderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="ILadderRepository.GetLadder" />
        public IEnumerable<LadderItem> GetLadder()
        {
            return GetLadderInner();
        }

        /// <inheritdoc cref="ILadderRepository.GetBotLadder" />
        public IEnumerable<LadderItem> GetBotLadder()
        {
            return GetLadderInner(true);
        }

        /// <inheritdoc cref="ILadderRepository.GetHumanLadder" />
        public IEnumerable<LadderItem> GetHumanLadder()
        {
            return GetLadderInner(false);
        }

        private IEnumerable<LadderItem> GetLadderInner(bool? isBot = null)
        {
            var query = _dbContext.ChessGames
                .Include(x => x.History)
                .Include(x => x.BlackPlayer)
                .Include(x => x.WhitePlayer)
                .Where(x => x.Status != GameState.InProgress);

            var pointsList = query.ToList().SelectMany(GetPlayersPoints);

            if (isBot.HasValue)
            {
                pointsList = pointsList.Where(x => x.IsBot == isBot);
            }

            var pointSumGroups = pointsList.GroupBy(x => new {x.Username, x.IsBot})
                .Select(x => new
                {
                    x.Key.Username,
                    x.Key.IsBot,
                    Points = x.Sum(y => y.AveragePlyPoints)
                })
                .GroupBy(x => x.Points)
                .OrderByDescending(x => x.Key)
                .Select((x, i) => new
                {
                    x.Key,
                    Position = i+1,
                    Players = x.Select(y => new { y.Username, y.IsBot })
                })
                .ToList();

            var result = pointSumGroups.SelectMany(x => x.Players.Select(y => new LadderItem
            {
                Name = y.Username,
                IsBot = y.IsBot,
                Place = x.Position,
                Points = x.Key
            })).ToList();

            return result;
        }

        private IEnumerable<PlayerPoints> GetPlayersPoints(DbChessGame game)
        {
            if (game.BlackPlayer.Id == game.WhitePlayer.Id)
            {
                return Enumerable.Empty<PlayerPoints>();
            }

            var whitePlayer = new PlayerPoints()
            {
                Username = game.WhitePlayer.UserName,
                IsBot = game.WhitePlayer.Bot,
                AveragePlyPoints = GetPoints(ChessPlayer.White, game.Status) / GetNumberOfMoves(ChessPlayer.White, game.History)
            };

            var blackPlayer = new PlayerPoints()
            {
                Username = game.BlackPlayer.UserName,
                IsBot = game.BlackPlayer.Bot,
                AveragePlyPoints = GetPoints(ChessPlayer.Black, game.Status) / GetNumberOfMoves(ChessPlayer.Black, game.History)
            };

            return new[]
            {
                whitePlayer,
                blackPlayer
            };
        }

        private int GetNumberOfMoves(ChessPlayer player, IEnumerable<DbBaseMove> history)
        {
            var half = history.Count() / 2.0m;

            switch (player)
            {
                case ChessPlayer.White: return (int)Math.Floor(half);
                case ChessPlayer.Black: return (int) Math.Ceiling(half);
                default: throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }

        private decimal GetPoints(ChessPlayer player, GameState state)
        {
            switch (state)
            {
                case GameState.WhiteWon:
                    return player == ChessPlayer.White ? 1000 : -1000;

                case GameState.BlackWon:
                    return player == ChessPlayer.Black ? 1000 : -1000;

                case GameState.Draw:
                    return 0;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private class PlayerPoints
        {
            public string Username { get; set; }
            public bool IsBot { get; set; }
            public decimal AveragePlyPoints { get; set; }
        }
    }
}
