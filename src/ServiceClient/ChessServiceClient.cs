using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.AccountControllerModels;
using BoardGame.Model.Api.ChessGamesControllerModels;
using BoardGame.Model.Api.LadderControllerModels;
using BoardGame.Model.Api.PlayerControllerModels;
using Easy.Common;
using Easy.Common.Extensions;
using Easy.Common.Interfaces;
using Newtonsoft.Json;

namespace BoardGame.ServiceClient
{
    public class ChessServiceClient : IDisposable
    {
        private readonly string _baseUrl;
        private IRestClient _client;

        private string GamesControllerUrl => $"{_baseUrl}/api/games";

        private Uri HealthControllerUri => new Uri($"{_baseUrl}/api/health");
        private Uri AccountControllerUri => new Uri($"{_baseUrl}/api/account");
        private Uri PlayersControllerUri => new Uri($"{_baseUrl}/api/players");
        private Uri GamesControllerUri => new Uri(GamesControllerUrl);
        private Uri LadderControllerUri => new Uri($"{_baseUrl}/api/ladder");
        private Uri LadderHumansControllerUri => new Uri($"{_baseUrl}/api/ladder/humans");
        private Uri LadderBotsControllerUri => new Uri($"{_baseUrl}/api/ladder/bots");
        private Uri TokenControllerUri => new Uri($"{_baseUrl}/api/token");

        public ChessServiceClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            JsonConvert.DefaultSettings = () =>
            {
                var x = new JsonSerializerSettings();
                x.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                x.NullValueHandling = NullValueHandling.Ignore;
                x.TypeNameHandling = TypeNameHandling.All;
                return x;
            };

            var defaultHeaders = new Dictionary<string, string>()
            {
                {"Accept", "application/json"},
            };

            _client = new RestClient(defaultHeaders, timeout: 15.Seconds());

            ServicePointManager.DnsRefreshTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
            ServicePointManager.FindServicePoint(new Uri(_baseUrl))
                               .ConnectionLeaseTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
        }

        public virtual async Task<string> GetVersionAsync()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, HealthControllerUri);

            var resultMessage = await _client.SendAsync(message);

            resultMessage.EnsureSuccessStatusCode();

            var version = await resultMessage.Content.ReadAsStringAsync();

            return version;
        }

        public virtual async Task<LoginResult> LoginAsync(string username, string password)
        {
            var loginModel = new LoginModel
            {
                UserName = username,
                Password = password
            };

            var jsonMessage = JsonConvert.SerializeObject(loginModel);

            var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            var message = new HttpRequestMessage(HttpMethod.Post, AccountControllerUri)
            {
                Content = content
            };

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return null;
            }

            var resultString = await resultMessage.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtTokenApiModel>(resultString);
            var jwt = new JwtSecurityToken(token.Token);

            var result = new LoginResult()
            {
                TokenString = jwt.RawData,
                ValidTo = jwt.ValidTo,
                Username = (string) jwt.Payload[ClaimTypes.Name],
                EmailAddress = (string) jwt.Payload[ClaimTypes.Email],
                IsBot = (string)jwt.Payload["IsBot"] == "True"
            };

            return result;
        }

        public virtual async Task<IEnumerable<Player>> GetPlayersAsync(string token)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, PlayersControllerUri);

            message.Headers.Add("Authorization", $"Bearer {token}");

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return null;
            }

            var resultString = await resultMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Player>>(resultString);

            return result;
        }

        public virtual async Task<IEnumerable<ChessGame>> GetMatchesAsync(string token)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, GamesControllerUri);

            message.Headers.Add("Authorization", $"Bearer {token}");

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return null;
            }

            var resultString = await resultMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<ChessGame>>(resultString);

            return result;
        }

        public virtual async Task<ChessGameDetails> GetMatchAsync(string token, Guid id)
        {
            return await GetMatchAsync(token, id.ToString());
        }

        public virtual async Task<ChessGameDetails> GetMatchAsync(string token, string id)
        {
            var url = $"{GamesControllerUri.AbsoluteUri}/{id}";
            var uri = new Uri(url);
            var message = new HttpRequestMessage(HttpMethod.Get, uri);

            message.Headers.Add("Authorization", $"Bearer {token}");

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return null;
            }

            var resultString = await resultMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ChessGameDetails>(resultString);

            var history = result.Representation.History;

            result.Representation = new ChessRepresentationInitializer().Create();
            var mechanism = new ChessMechanism();
            foreach (var baseMove in history)
            {
                result.Representation = mechanism.ApplyMove(result.Representation, baseMove);
            }

            return result;
        }

        public virtual async Task<ChessGame> ChallengePlayerAsync(string token, string username)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, GamesControllerUri);

            message.Headers.Add("Authorization", $"Bearer {token}");

            var challengeDto = new ChallengeRequest()
            {
                Opponent = username
            };

            message.Content = new StringContent(JsonConvert.SerializeObject(challengeDto), Encoding.UTF8, "application/json");

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return null;
            }

            var resultString = await resultMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ChessGame>(resultString);

            return result;
        }

        public virtual async Task<bool> SendMoveAsync<T>(string token, Guid matchId, T move) where T : BaseMove
        {
            return await SendMoveAsync(token, matchId.ToString(), move);
        }

        public virtual async Task<bool> SendMoveAsync<T>(string token, string matchId, T move) where T : BaseMove
        {
            var url = $"{GamesControllerUri.AbsoluteUri}/{matchId}";
            var uri = new Uri(url);
            var message = new HttpRequestMessage(HttpMethod.Put, uri);

            message.Headers.Add("Authorization", $"Bearer {token}");
            message.Headers.Add("Accept", $"application/json");

            var moveString = JsonConvert.SerializeObject(move);

            message.Content = new StringContent(moveString, Encoding.UTF8, "application/json");

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<LoginResult> ProlongToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var message = new HttpRequestMessage(HttpMethod.Post, TokenControllerUri);

            message.Headers.Add("Authorization", $"Bearer {token}");

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return null;
            }

            var resultString = await resultMessage.Content.ReadAsStringAsync();
            var newToken = JsonConvert.DeserializeObject<JwtTokenApiModel>(resultString);
            var jwt = new JwtSecurityToken(newToken.Token);

            var result = new LoginResult()
            {
                TokenString = jwt.RawData,
                ValidTo = jwt.ValidTo,
                Username = (string)jwt.Payload[ClaimTypes.Name],
                EmailAddress = (string)jwt.Payload[ClaimTypes.Email],
                IsBot = (string)jwt.Payload["IsBot"] == "True"
            };

            return result;
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
        public virtual async Task<IEnumerable<LadderItem>> GetLadderAsync(bool? botsLadder = null)
        {
            HttpRequestMessage message;

            if (!botsLadder.HasValue)
            {
                message = new HttpRequestMessage(HttpMethod.Get, LadderControllerUri);
            }
            else
            {
                message = botsLadder.Value
                    ? new HttpRequestMessage(HttpMethod.Get, LadderBotsControllerUri)
                    : new HttpRequestMessage(HttpMethod.Get, LadderHumansControllerUri);
            }

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return null;
            }

            var resultJson = await resultMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<LadderItem>>(resultJson);

            return result;
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
