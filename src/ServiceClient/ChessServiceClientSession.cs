using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Api.ChessGamesControllerModels;
using Model.Api.PlayerControllerModels;

namespace ServiceClient
{
    public class ChessServiceClientSession : IDisposable
    {
        private readonly string _username;
        private readonly string _password;
        private ChessServiceClient _client;

        public ChessServiceClientSession(string baseUrl, string username, string password)
        {
            _username = username;
            _password = password;
            BaseUrl = baseUrl;
            _client = new ChessServiceClient(BaseUrl);
        }

        public string BaseUrl { get; }

        public Uri BaseUri => new Uri(BaseUrl);

        public bool IsLoggedIn => LoginInformation != null && LoginInformation.ValidTo > DateTime.UtcNow;

        public LoginResult LoginInformation { get; private set; }

        private string JwtToken => LoginInformation?.TokenString;

        public async Task<bool> Initialize()
        {
            if (!IsLoggedIn)
            {
                LoginInformation = await _client.Login(_username, _password);
            }

            return IsLoggedIn;
        }

        public async Task<ChessGame> ChallengePlayer(string username)
        {
            await Initialize();
            return await _client.ChallengePlayer(JwtToken, username);
        }

        public async Task<ChessGameDetails> GetMatch(string id)
        {
            await Initialize();
            return await _client.GetMatch(JwtToken, id);
        }

        public async Task<IEnumerable<ChessGame>> GetMatches()
        {
            await Initialize();
            return await _client.GetMatches(JwtToken);
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            await Initialize();
            return await _client.GetPlayers(JwtToken);
        }

        public async Task<string> GetVersion()
        {
            return await _client.GetVersion();
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