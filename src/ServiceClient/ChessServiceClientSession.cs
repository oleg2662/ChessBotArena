using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.ChessGamesControllerModels;
using BoardGame.Model.Api.LadderControllerModels;
using BoardGame.Model.Api.PlayerControllerModels;

namespace BoardGame.ServiceClient
{
    public class ChessServiceClientSession : IDisposable
    {
        private string _username;
        private string _password;
        private ChessServiceClient _client;

        public ChessServiceClientSession(string baseUrl, string username, string password)
        {
            _username = username;
            _password = password;
            BaseUrl = baseUrl;
            _client = new ChessServiceClient(BaseUrl);
        }

        public ChessServiceClientSession(string baseUrl)
            : this(baseUrl, string.Empty, string.Empty)
        {
        }

        public async Task<bool> Login(string userName, string password)
        {
            _username = userName;
            _password = password;

            return await Login();
        }

        public void Logout()
        {
            LoginInformation = null;
        }

        public string BaseUrl { get; }

        public Uri BaseUri => new Uri(BaseUrl);

        private bool IsLoggedIn => LoginInformation != null && LoginInformation.ValidTo > DateTime.UtcNow;

        public async Task<bool> EnsureSessionIsActive()
        {
            if (IsLoggedIn) return true;
            if (LoginInformation == null || string.IsNullOrWhiteSpace(_username)) return false;
            return await Login();
        }

        public LoginResult LoginInformation { get; private set; }

        private string JwtToken => LoginInformation?.TokenString;

        public async Task<bool> Login()
        {
            if (!IsLoggedIn)
            {
                LoginInformation = await _client.LoginAsync(_username, _password);
            }

            return IsLoggedIn;
        }

        public async Task<ChessGame> ChallengePlayer(string username)
        {
            await Login();
            return await _client.ChallengePlayerAsync(JwtToken, username);
        }

        public async Task<ChessGameDetails> GetMatch(string id)
        {
            await Login();
            return await _client.GetMatchAsync(JwtToken, id);
        }

        public async Task<IEnumerable<ChessGame>> GetMatches()
        {
            await Login();
            return await _client.GetMatchesAsync(JwtToken);
        }

        public async Task<IEnumerable<ChessGame>> GetMatchesWithDetails()
        {
            await Login();
            return await _client.GetMatchesWithDetailsAsync(JwtToken);
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            await Login();
            return await _client.GetPlayersAsync(JwtToken);
        }

        public virtual async Task<bool> SendMove<T>(Guid matchId, T move) where T : BaseMove
        {
            return await _client.SendMoveAsync(JwtToken, matchId, move);
        }

        public async Task<string> GetVersion()
        {
            return await _client.GetVersionAsync();
        }

        /// <summary>
        /// Gets the ladder.
        /// </summary>
        /// <param name="botsLadder">
        /// If true, it will return the bots' ladder.
        /// If false, it will return the human players' ladder.
        /// If not set (default) it will return the combined ladder.
        /// </param>
        /// <returns>The ladder list. If there was an error, it returns null.</returns>
        public virtual async Task<IEnumerable<LadderItem>> GetLadder(bool? botsLadder = null)
        {
            return await _client.GetLadderAsync(botsLadder);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_client == null) return;

            _client.Dispose();
            _client = null;
        }
    }
}