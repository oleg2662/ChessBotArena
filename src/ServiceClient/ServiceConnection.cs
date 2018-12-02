using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.ChessGamesControllerModels;
using BoardGame.Model.Api.LadderControllerModels;
using BoardGame.Model.Api.PlayerControllerModels;

namespace BoardGame.ServiceClient
{
    /// <summary>
    /// Used to manage and maintain a connection to the chess service.
    /// </summary>
    public class ServiceConnection
    {
        private int _pollTimeout = 10000;
        private bool _pauseRefresh = false;
        private string _token;
        private ChessServiceClient _client;
        private readonly Task _refreshTask;

        /// <summary>
        /// Creates an instance of the service connection class.
        /// </summary>
        /// <param name="baseUrl">The base URL of the service the client connects to.</param>
        public ServiceConnection(string baseUrl)
        {
            _client = new ChessServiceClient(baseUrl);
            _refreshTask = new Task(async () =>
            {
                if (!_pauseRefresh)
                {
                    await PollAsync();
                }

                Thread.Sleep(_pollTimeout);
            });
        }

        /// <summary>
        /// Gets the date and time of the last update.
        /// </summary>
        public DateTime LastUpdate { get; private set; }

        /// <summary>
        /// Gets the token's validation date and time converted to client local time.
        /// </summary>
        public DateTime? TokenValidTo { get; private set; }

        /// <summary>
        /// Gets the logged in user. If no user is logged in yet it returns null.
        /// </summary>
        public string LoggedInUser { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the logged in user is a bot. If no user is logged in returns null.
        /// </summary>
        public bool? IsBotLoggedIn { get; private set; }

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        public ICollection<Player> Players { get; set; }

        /// <summary>
        /// Gets the list of matches
        /// </summary>
        public ICollection<ChessGame> Matches { get; set; }

        /// <summary>
        /// Gets or sets the currently selected match. Null if no game is selected.
        /// </summary>
        public ChessGame CurrentGame { get; set; }

        /// <summary>
        /// Tries to login.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password</param>
        /// <returns>True if the login is successful. Otherwise false.</returns>
        public async Task<bool> LoginAsync(string username, string password)
        {
            OnLoginStarted(ServiceConnectionEventArgs.Ok());

            LoginResult result;

            try
            {
                result = await _client.LoginAsync(username, password);
            }
            catch (Exception e)
            {
                OnPollFinished(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            _token = result?.TokenString;
            TokenValidTo = result?.ValidTo.ToLocalTime();
            LoggedInUser = result?.Username;
            IsBotLoggedIn = result?.IsBot;

            var success = string.IsNullOrWhiteSpace(_token);

            if (success)
            {
                OnPollFinished(ServiceConnectionEventArgs.Ok($"Logged in: {LoggedInUser}."));
            }
            else
            {
                OnPollFinished(ServiceConnectionEventArgs.Ok("Couldn't log in."));
            }

            return success;
        }

        /// <summary>
        /// Gets the combined ladder of the game.
        /// </summary>
        /// <returns>The ladder with bot and human players.</returns>
        public async Task<ICollection<LadderItem>> GetLadder()
        {
            var ladder = await _client.GetLadderAsync(null);
            return ladder.ToList().AsReadOnly();
        }

        /// <summary>
        /// Sends a move in to the currently selected game.
        /// </summary>
        /// <typeparam name="T">Type of the move.</typeparam>
        /// <param name="move">The move.</param>
        /// <returns>True if successful. Otherwise false.</returns>
        public async Task<bool> SendMoveAsync<T>(T move) where T : BaseMove
        {
            var result = await _client.SendMoveAsync(_token, CurrentGame.Id, move);
            if (result)
            {
                await PollAsync();
            }

            return result;
        }

        /// <summary>
        /// Sends a challenge request to the given player.
        /// </summary>
        /// <param name="username">The username of the player to be challenged.</param>
        /// <returns>The newly created details of the match. Null if unsuccessful.</returns>
        public async Task<ChessGame> ChallengePlayerAsync(string username)
        {
            var chessGame = await _client.ChallengePlayerAsync(_token, username);
            if (chessGame != null)
            {
                await PollAsync();
            }

            return chessGame;
        }

        /// <summary>
        /// Event raised when the background poll started.
        /// </summary>
        public event EventHandler<ServiceConnectionEventArgs> PollStarted;

        /// <summary>
        /// Event raised when the background poll has finished.
        /// </summary>
        public event EventHandler<ServiceConnectionEventArgs> PollFinished;

        /// <summary>
        /// Event raised when the list of players have changed.
        /// </summary>
        public event EventHandler<ServiceConnectionEventArgs> PlayersListChanged;

        /// <summary>
        /// Event raised when the list of matches have changed.
        /// </summary>
        public event EventHandler<ServiceConnectionEventArgs> MatchesListChanged;

        /// <summary>
        /// Event raised when the state of the current game has changed. (Opponent moved.)
        /// </summary>
        public event EventHandler<ServiceConnectionEventArgs> CurrentGameChanged;

        /// <summary>
        /// Event raised when the service tells the token cannot be extended since it has expired.
        /// </summary>
        public event EventHandler<EventArgs> TokenExpired;

        /// <summary>
        /// Event raised when the login is attempted.
        /// </summary>
        public event EventHandler<ServiceConnectionEventArgs> LoginStarted;

        /// <summary>
        /// Event raised when the login call has ended.
        /// Returns error only if there was a problem in the service call.
        /// </summary>
        public event EventHandler<ServiceConnectionEventArgs> LoginFinished;

        private void OnPlayersListChanged(ServiceConnectionEventArgs e)
        {
            PlayersListChanged?.Invoke(this, e);
        }

        private void OnMatchesListChanged(ServiceConnectionEventArgs e)
        {
            MatchesListChanged?.Invoke(this, e);
        }

        private void OnCurrentGameChanged(ServiceConnectionEventArgs e)
        {
            CurrentGameChanged?.Invoke(this, e);
        }

        private void OnPollStarted(ServiceConnectionEventArgs e)
        {
            PollStarted?.Invoke(this, e);
        }

        private void OnPollFinished(ServiceConnectionEventArgs e)
        {
            PollFinished?.Invoke(this, e);
            LastUpdate = DateTime.Now;
        }

        private void OnLoginStarted(ServiceConnectionEventArgs e)
        {
            LoginStarted?.Invoke(this, e);
        }

        private void OnLoginFinished(ServiceConnectionEventArgs e)
        {
            LoginFinished?.Invoke(this, e);
        }

        private async Task PollAsync()
        {
            OnPollStarted(ServiceConnectionEventArgs.Ok());

            var serverAlive = await CheckIfServerAliveAsync();
            if (!serverAlive)
            {
                OnPollFinished(ServiceConnectionEventArgs.Error("Server is down."));
                return;
            }

            var tokenRefreshSuccess = await RefreshTokenAsync();

            if (tokenRefreshSuccess)
            {
                try
                {
                    await RefreshPlayersListAsync();
                    await RefreshMatchesListAsync();
                    await RefreshCurrentMatchAsync();
                }
                catch (Exception e)
                {
                    OnPollFinished(ServiceConnectionEventArgs.Error(e));
                    return;
                }
            }

            OnPollFinished(ServiceConnectionEventArgs.Ok());
        }

        private async Task<bool> CheckIfServerAliveAsync()
        {
            try
            {
                await _client.GetVersionAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> RefreshTokenAsync()
        {
            _token = await _client.ProlongToken(_token);
            return !string.IsNullOrWhiteSpace(_token);
        }

        private async Task RefreshPlayersListAsync()
        {
            var players = await _client.GetPlayersAsync(_token);
            var oldPlayers = Players.Select(x => x.Name);
            var newPlayers = players.Select(x => x.Name);

            if (!oldPlayers.SequenceEqual(newPlayers))
            {
                OnPlayersListChanged(ServiceConnectionEventArgs.Ok());
                Players = players.ToList().AsReadOnly();
            }
        }

        private async Task RefreshMatchesListAsync()
        {
            var matches = await _client.GetMatchesAsync(_token);

            var oldMatches = Matches.Select(x => x.Id);
            var newMatches = matches.Select(x => x.Id);

            if (!oldMatches.SequenceEqual(newMatches))
            {
                OnMatchesListChanged(ServiceConnectionEventArgs.Ok());
                Matches = matches.ToList().AsReadOnly();
            }
        }

        private async Task RefreshCurrentMatchAsync()
        {
            if (CurrentGame == null)
            {
                return;
            }

            var match = await _client.GetMatchAsync(_token, CurrentGame.Id);

            if (!match.Equals(CurrentGame))
            {
                OnCurrentGameChanged(ServiceConnectionEventArgs.Ok());
                CurrentGame = match;
            }
        }
    }
}
