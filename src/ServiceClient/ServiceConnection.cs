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
    public class ServiceConnection : IDisposable
    {
        //private int _pollTimeout = 10000;
        //private bool _pauseRefresh = false;
        private string _token;
        private ChessServiceClient _client;
        private readonly object _lock;
        private bool _disposed;
        //private bool _cancelRefresh = false;

        /// <summary>
        /// Creates an instance of the service connection class.
        /// </summary>
        /// <param name="baseUrl">The base URL of the service the client connects to.</param>
        public ServiceConnection(string baseUrl)
        {
            _lock = new object();
            _client = new ChessServiceClient(baseUrl);

            Players = new List<Player>();
            Matches = new List<ChessGameDetails>();
            CurrentGame = null;

            //var refreshTask = new Task(async () =>
            //{
            //    do
            //    {
            //        if (_cancelRefresh)
            //        {
            //            return;
            //        }

            //        if (!_pauseRefresh)
            //        {
            //            await PollAsync();
            //        }

            //        Thread.Sleep(_pollTimeout);
            //    } while (true);
            //});

            //refreshTask.Start();
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
        /// Gets a value indicating whether the client is in "anonymous" mode.
        /// (No valid login detected.)
        /// </summary>
        public bool IsAnonymous
        {
            get => LoggedInUser == null;
        }

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
        public ICollection<ChessGameDetails> Matches { get; set; }

        /// <summary>
        /// Gets or sets the currently selected match. Null if no game is selected.
        /// </summary>
        public ChessGameDetails CurrentGame { get; set; }

        /// <summary>
        /// Makes the given match the current one and causes a revalidation.
        /// </summary>
        /// <param name="matchId">The ID of the match.</param>
        public async Task MakeCurrentMatchAsync(Guid? matchId)
        {
            if (!matchId.HasValue)
            {
                CurrentGame = null;
            }
            else
            {
                var match = Matches.FirstOrDefault(x => x.Id == matchId);
                CurrentGame = match ?? throw new ArgumentException("Given match ID cannot be found.", nameof(matchId));
            }

            await PollAsync();
        }

        public async Task Refresh()
        {
            await PollAsync();
        }

        ///// <summary>
        ///// Pauses the automatic refresh.
        ///// </summary>
        //public void PauseRefresh()
        //{
        //    _pauseRefresh = true;
        //}

        ///// <summary>
        ///// Continues the background refresh.
        ///// </summary>
        //public void ContinueRefresh()
        //{
        //    _pauseRefresh = false;
        //}

        /// <summary>
        /// Gets a value indicating whether the object is disposed.
        /// </summary>
        public bool Disposed
        {
            get
            {
                lock (_lock)
                {
                    return _disposed;
                }
            }
        }

        /// <summary>
        /// Tries to login.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password</param>
        /// <returns>True if the login is successful. Otherwise false.</returns>
        public async Task<bool> LoginAsync(string username, string password)
        {
            DisposeGuard();
            OnLoginStarted(ServiceConnectionEventArgs.Ok());

            LoginResult result;

            try
            {
                result = await _client.LoginAsync(username, password);
            }
            catch (Exception e)
            {
                OnLoginFinished(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            SetSessionData(result);

            var success = !string.IsNullOrWhiteSpace(_token);

            OnLoginFinished(success
                ? ServiceConnectionEventArgs.Ok($"Logged in: {LoggedInUser}.")
                : ServiceConnectionEventArgs.Ok("Couldn't log in."));

            await PollAsync();

            return success;
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public async Task Logout()
        {
            _token = null;
            await PollAsync();
        }

        /// <summary>
        /// Gets the combined ladder of the game.
        /// </summary>
        /// <returns>The ladder with bot and human players.</returns>
        public async Task<ICollection<LadderItem>> GetLadder()
        {
            DisposeGuard();
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
            DisposeGuard();

            if (CurrentGame == null)
            {
                return false;
            }

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
            DisposeGuard();
            var chessGame = await _client.ChallengePlayerAsync(_token, username);
            if (chessGame != null)
            {
                await PollAsync();
            }

            return chessGame;
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

        /// <summary>
        /// Disposes the object in a safe way.
        /// Used internally! Do not use directly!
        /// </summary>
        /// <param name="disposing">Is disposing?</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (_lock)
            {
                if (_disposed) return;
                if (!disposing) return;
                if (_client == null) return;

                //_pauseRefresh = true;
                //_cancelRefresh = true;

                _client.Dispose();
                _client = null;
                _disposed = true;
            }
        }

        private void SetSessionData(LoginResult loginResult)
        {
            _token = loginResult?.TokenString;
            TokenValidTo = loginResult?.ValidTo.ToLocalTime();
            LoggedInUser = loginResult?.Username;
            IsBotLoggedIn = loginResult?.IsBot;
        }

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

        private void OnTokenExpired(EventArgs e)
        {
            TokenExpired?.Invoke(this, e);
        }

        private async Task PollAsync()
        {
            DisposeGuard();
            OnPollStarted(ServiceConnectionEventArgs.Ok());

            if (_token == null)
            {
                await RefreshPlayersListAsync();
                await RefreshMatchesListAsync();
                await RefreshCurrentMatchAsync();
                return;
            }

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
                DisposeGuard();
                await _client.GetVersionAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> RefreshTokenAsync(bool individualCall = false)
        {
            DisposeGuard();

            if (_token == null)
            {
                return false;
            }

            if (TokenValidTo < DateTime.Now)
            {
                OnTokenExpired(EventArgs.Empty);
                return false;
            }

            var loginResult = await _client.ProlongToken(_token);
            var success = loginResult != null;
            var previousTokenValid = TokenValidTo > DateTime.Now;

            // If it's successful, then set session data and return true.
            if (success)
            {
                SetSessionData(loginResult);
                return true;
            }

            // If we couldn't prolong the token then we check if the previous one is still valid.
            // if no, then we logout (reset the logged in information) and return false...
            // Otherwise we return true
            // (This will happen until the previous token is valid...)
            if (previousTokenValid) return true;

            SetSessionData(null);
            if (individualCall)
            {
                OnTokenExpired(EventArgs.Empty);
            }

            return false;
        }

        private async Task RefreshPlayersListAsync()
        {
            DisposeGuard();

            ICollection<Player> players = IsAnonymous
                ? new List<Player>()
                : (await _client.GetPlayersAsync(_token)).ToList();

            var oldPlayers = Players?.Select(x => x.Name);
            var newPlayers = players.Select(x => x.Name);

            if (oldPlayers.SequenceEqual(newPlayers))
            {
                return;
            }

            Players = players.ToList().AsReadOnly();
            OnPlayersListChanged(ServiceConnectionEventArgs.Ok());
        }

        private async Task RefreshMatchesListAsync()
        {
            DisposeGuard();

            ICollection<ChessGame> matches = IsAnonymous
                ? new List<ChessGame>()
                : (await _client.GetMatchesAsync(_token)).ToList();

            var oldMatches = Matches.Select(x => x.Id).ToList();
            var newMatches = matches.Select(x => x.Id).ToList();

            if (oldMatches.SequenceEqual(newMatches))
            {
                return;
            }

            Matches.Clear();

            foreach (var matchId in newMatches)
            {
                var matchDetails = await _client.GetMatchAsync(_token, matchId);
                Matches.Add(matchDetails);
            }

            OnMatchesListChanged(ServiceConnectionEventArgs.Ok());
        }

        private async Task RefreshCurrentMatchAsync()
        {
            DisposeGuard();

            if (IsAnonymous)
            {
                CurrentGame = null;
            }

            if (CurrentGame == null)
            {
                OnCurrentGameChanged(ServiceConnectionEventArgs.Ok());
                return;
            }

            var match = await _client.GetMatchAsync(_token, CurrentGame.Id);

            if (match.Equals(CurrentGame))
            {
                return;
            }

            CurrentGame = match;

            OnCurrentGameChanged(ServiceConnectionEventArgs.Ok());
        }

        private void DisposeGuard()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException("Object is already disposed");
            }
        }

        /// <summary>
        /// Destructor of the class.
        /// </summary>
        ~ServiceConnection()
        {
            Dispose(false);
        }
    }
}
