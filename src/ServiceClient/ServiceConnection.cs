using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.ChessGamesControllerModels;
using BoardGame.Model.Api.LadderControllerModels;
using BoardGame.Model.Api.PlayerControllerModels;
using Timer = System.Timers.Timer;

namespace BoardGame.ServiceClient
{
    /// <summary>
    /// Used to manage and maintain a connection to the chess service.
    /// </summary>
    public class ServiceConnection : IDisposable
    {
        private ChessServiceClient _client;
        private readonly object _lock;
        private bool _disposed;
        private LoginResult _loginResult;
        private readonly ChessMechanism _mechanism;

        private Timer _tokenRefreshTimer;
        private Timer _stateRefreshTimer;

        private readonly List<Player> _players;
        private readonly List<ChessGameDetails> _matches;

        private readonly ConcurrentHashSet<string> _runningMethods;

        private static readonly SemaphoreSlim _pollSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Creates an instance of the service connection class.
        /// </summary>
        /// <param name="baseUrl">The base URL of the service the client connects to.</param>
        public ServiceConnection(string baseUrl)
        {
            _lock = new object();
            _client = new ChessServiceClient(baseUrl);
            _runningMethods = new ConcurrentHashSet<string>();
            _mechanism = new ChessMechanism();

            _players = new List<Player>();
            _matches = new List<ChessGameDetails>();
            CurrentGame = null;

            InitTokenRefreshTimer();
            InitStateRefreshTimer();
        }

        /// <summary>
        /// Gets the date and time of the last update.
        /// </summary>
        public DateTime LastUpdate { get; private set; }

        /// <summary>
        /// Gets the token's validation date and time converted to client local time.
        /// </summary>
        public DateTime? TokenValidTo => _loginResult?.ValidTo.ToLocalTime();

        /// <summary>
        /// Gets the logged in user. If no user is logged in yet it returns null.
        /// </summary>
        public string LoggedInUser => _loginResult?.Username;

        /// <summary>
        /// Gets a value indicating whether the client is in "anonymous" mode.
        /// (No valid login detected.)
        /// </summary>
        public bool IsAnonymous => LoggedInUser == null || TokenValidTo < DateTime.Now;

        /// <summary>
        /// Gets a value indicating whether the logged in user is a bot. If no user is logged in returns null.
        /// </summary>
        public bool? IsBotLoggedIn => _loginResult?.IsBot;

        /// <summary>
        /// Gets a value indicating whether it's the logged in user's turn in the current game.
        /// If no one is logged in or there is no current game selected the property returns null.
        /// </summary>
        public bool? IsItMyTurn
        {
            get
            {
                if (CurrentGame == null || IsAnonymous)
                {
                    return null;
                }

                var representation = CurrentGame.Representation;

                switch (representation.CurrentPlayer)
                {
                    case ChessPlayer.White:
                        if (CurrentGame.WhitePlayer.UserName == LoggedInUser)
                        {
                            return true;
                        }
                        break;
                    case ChessPlayer.Black:
                        if (CurrentGame.BlackPlayer.UserName == LoggedInUser)
                        {
                            return true;
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(representation.CurrentPlayer));
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the current state of the current game. Null if there is no selected game.
        /// </summary>
        public GameState? CurrentGameState => CurrentGame?.Representation == null ? null : (GameState?)_mechanism.GetGameState(CurrentGame.Representation);

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

        /// <summary>
        /// Gets the list of matches
        /// </summary>
        public IReadOnlyCollection<ChessGameDetails> Matches => _matches.AsReadOnly();

        /// <summary>
        /// Gets or sets the currently selected match. Null if no game is selected.
        /// </summary>
        public ChessGameDetails CurrentGame { get; private set; }

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
        /// Makes the given match the current one and causes a revalidation.
        /// </summary>
        /// <param name="matchId">The ID of the match.</param>
        public void MakeCurrentMatchAsync(Guid? matchId)
        {
            DisposeGuard();

            if (!matchId.HasValue)
            {
                CurrentGame = null;
            }
            else
            {
                var match = Matches.FirstOrDefault(x => x.Id == matchId);
                CurrentGame = match ?? throw new ArgumentException("Given match ID cannot be found.", nameof(matchId));
            }
        }

        /// <summary>
        /// Stops the auto-refresh mechanism.
        /// </summary>
        public void StopAutoRefresh()
        {
            _stateRefreshTimer.Stop();
        }

        /// <summary>
        /// Starts the auto-refresh mechanism.
        /// </summary>
        public void StartAutoRefresh()
        {
            _stateRefreshTimer.Start();
        }

        /// <summary>
        /// Refreshes the state of the current game session.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Refresh()
        {
            return await PollAsync();
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

            if (!TryBeginInvoke())
            {
                return false;
            }

            LoginResult result;

            try
            {
                result = await _client.LoginAsync(username, password);
            }
            catch (Exception e)
            {
                await PollAsync();
                TryEndInvoke();
                OnBackgroundError(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            SetSessionData(result);
            await PollAsync();
            TryEndInvoke();

            return result != null;
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public async Task Logout()
        {
            _loginResult = null;
            await PollAsync();
        }

        /// <summary>
        /// Gets the combined ladder of the game.
        /// </summary>
        /// <returns>The ladder with bot and human players.</returns>
        public async Task<ICollection<LadderItem>> GetLadder()
        {
            DisposeGuard();
            if (!TryBeginInvoke())
            {
                return null;
            }

            ICollection<LadderItem> result;

            try
            {
                result = (await _client.GetLadderAsync()).ToList().AsReadOnly();
            }
            catch (TaskCanceledException)
            {
                TryEndInvoke();
                return null;
            }

            TryEndInvoke();
            return result;
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

            if (!TryBeginInvoke())
            {
                return false;
            }

            bool result;

            try
            {
                result = await _client.SendMoveAsync(_loginResult?.TokenString, CurrentGame.Id, move);
            }
            catch (Exception e)
            {
                TryEndInvoke();
                OnPollFinished(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            if (result)
            {
                await PollAsync();
            }

            TryEndInvoke();

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
            if (!TryBeginInvoke())
            {
                return null;
            }

            ChessGame chessGame;

            try
            {
                chessGame = await _client.ChallengePlayerAsync(_loginResult?.TokenString, username);
            }
            catch (TaskCanceledException)
            {
                TryEndInvoke();
                return null;
            }

            if (chessGame != null)
            {
                await PollAsync();
            }

            TryEndInvoke();
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
        /// Event raised when something goes wrong in the background state refresh.
        /// </summary>
        public event EventHandler<ServiceConnectionEventArgs> BackgroundError;

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

                _client.Dispose();
                _client = null;
                _disposed = true;
            }
        }

        private void SetSessionData(LoginResult loginResult)
        {
            _loginResult = loginResult;
        }

        private void InitTokenRefreshTimer()
        {
            _tokenRefreshTimer = new Timer(600000)
            {
                AutoReset = false,
                Enabled = true
            };
            _tokenRefreshTimer.Elapsed += TokenRefreshTimerOnElapsed;
        }

        private void InitStateRefreshTimer()
        {
            _stateRefreshTimer = new Timer(5000)
            {
                AutoReset = false,
                Enabled = false
            };
            _stateRefreshTimer.Elapsed += StateRefreshTimerOnElapsed;
        }

        private bool TryBeginInvoke([CallerMemberName] string callerMemberName = "")
        {
            var result = _runningMethods.Add(callerMemberName);
            return result;
        }

        private void TryEndInvoke([CallerMemberName] string callerMemberName = "")
        {
            _runningMethods.Remove(callerMemberName);
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

        private void OnBackgroundError(ServiceConnectionEventArgs e)
        {
            BackgroundError?.Invoke(this, e);
        }

        private async Task<bool> PollAsync([CallerMemberName] string callerMemberName = nameof(PollAsync))
        {
            DisposeGuard();
            await _pollSemaphore.WaitAsync();
            var result = false;

            try
            {
                OnPollStarted(ServiceConnectionEventArgs.Ok(callerMemberName));

                var serverAlive = await CheckIfServerAliveAsync();
                if (!serverAlive)
                {
                    LastUpdate = DateTime.Now;
                    OnPollFinished(ServiceConnectionEventArgs.Error("Server is down."));
                    return false;
                }

                var tokenRefreshSuccess = await RefreshTokenAsync();

                if (tokenRefreshSuccess)
                {
                    try
                    {
                        result |= await RefreshPlayersListAsync();
                        result |= await RefreshMatchesListAsync();
                        result |= await RefreshCurrentMatchAsync();
                    }
                    catch (Exception e)
                    {
                        LastUpdate = DateTime.Now;
                        OnPollFinished(ServiceConnectionEventArgs.Error(e, callerMemberName));
                        return result;
                    }
                }

                LastUpdate = DateTime.Now;
                TryEndInvoke();
                OnPollFinished(ServiceConnectionEventArgs.Ok(callerMemberName: callerMemberName));
            }
            catch (Exception e)
            {
                OnBackgroundError(ServiceConnectionEventArgs.Error(e, callerMemberName));
            }
            finally
            {
                _pollSemaphore.Release();
            }

            return result;
        }

        private async Task<bool> CheckIfServerAliveAsync()
        {
            DisposeGuard();

            try
            {
                await _client.GetVersionAsync();
            }
            catch (Exception e)
            {
                OnBackgroundError(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            return true;
        }

        private async Task<bool> RefreshTokenAsync()
        {
            DisposeGuard();
            if (!TryBeginInvoke())
            {
                return false;
            }

            LoginResult loginResult;
            try
            {
                loginResult = await _client.ProlongToken(_loginResult?.TokenString);
            }
            catch (Exception e)
            {
                TryEndInvoke();
                OnBackgroundError(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            SetSessionData(loginResult);
            TryEndInvoke();

            return true;
        }

        private async Task<bool> RefreshPlayersListAsync()
        {
            DisposeGuard();
            ICollection<Player> players;

            try
            {
                players = IsAnonymous
                    ? new List<Player>()
                    : (await _client.GetPlayersAsync(_loginResult?.TokenString)).ToList();
            }
            catch (Exception e)
            {
                OnBackgroundError(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            var oldPlayers = Players?.Select(x => x.Name) ?? Enumerable.Empty<string>();
            var newPlayers = players.Select(x => x.Name);

            if (newPlayers.SequenceEqual(oldPlayers))
            {
                return false;
            }

            _players.Clear();
            _players.AddRange(players);

            return true;
        }

        private async Task<bool> RefreshMatchesListAsync()
        {
            DisposeGuard();

            ICollection<ChessGameDetails> matches;

            try
            {
                matches = IsAnonymous
                        ? new List<ChessGameDetails>()
                        : (await _client.GetMatchesWithDetailsAsync(_loginResult?.TokenString)).ToList();
            }
            catch (Exception e)
            {
                OnBackgroundError(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            var oldMatches = Matches.Select(x => x.Id).ToList();
            var newMatches = matches.Select(x => x.Id).ToList();

            _matches.Clear();
            _matches.AddRange(matches);

            return !oldMatches.SequenceEqual(newMatches);
        }

        private async Task<bool> RefreshCurrentMatchAsync()
        {
            DisposeGuard();

            if (IsAnonymous)
            {
                if (CurrentGame == null) return false;

                CurrentGame = null;
            }

            if (CurrentGame == null)
            {
                return false;
            }

            ChessGameDetails match;

            try
            {
                match = await _client.GetMatchAsync(_loginResult?.TokenString, CurrentGame.Id);
            }
            catch (Exception e)
            {
                OnBackgroundError(ServiceConnectionEventArgs.Error(e));
                return false;
            }

            if (match.Equals(CurrentGame))
            {
                return false;
            }

            CurrentGame = match;
            return true;
        }

        private async void StateRefreshTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            DisposeGuard();

            try
            {
                await PollAsync();
            }
            catch (Exception exception)
            {
                OnBackgroundError(ServiceConnectionEventArgs.Error(exception));
            }

            _stateRefreshTimer.Start();
        }

        private async void TokenRefreshTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            DisposeGuard();

            try
            {
                await RefreshTokenAsync();
            }
            catch (Exception exception)
            {
                OnBackgroundError(ServiceConnectionEventArgs.Error(exception));
            }

            _tokenRefreshTimer.Start();
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
