using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Game.Chess;
using Game.Chess.Moves;
using Model.Api.AccountControllerModels;
using Model.Api.ChessGamesControllerModels;
using Model.Api.PlayerControllerModels;
using Newtonsoft.Json;

namespace ServiceClient
{
    public class ChessServiceClient : IDisposable
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client = new HttpClient();

        private string GamesControllerUrl => $"{_baseUrl}/api/games";

        private Uri HealthControllerUri => new Uri($"{_baseUrl}/api/health");
        private Uri AccountControllerUri => new Uri($"{_baseUrl}/api/account");
        private Uri PlayersControllerUri => new Uri($"{_baseUrl}/api/players");
        private Uri GamesControllerUri => new Uri(GamesControllerUrl);

        public ChessServiceClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            JsonConvert.DefaultSettings = () =>
            {
                var x = new JsonSerializerSettings();
                x.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                x.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                x.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
                return x;
            };
        }

        //private JsonSerializerSettings GetJsonSettings()
        //{
        //    var x = new JsonSerializerSettings();
        //    x.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        //    x.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        //    x.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
        //    return x;
        //}

        public async Task<string> GetVersion()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, HealthControllerUri);

            var resultMessage = await _client.SendAsync(message);

            resultMessage.EnsureSuccessStatusCode();

            var version = await resultMessage.Content.ReadAsStringAsync();

            return version;
        }

        public async Task<string> Login(string username, string password)
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

            return token.Token;
        }

        public async Task<IEnumerable<Player>> GetPlayers(string token)
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

        public async Task<IEnumerable<ChessGame>> GetMatches(string token)
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

        public async Task<ChessGameDetails> GetMatch(string token, string id)
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

        public async Task<ChessGame> ChallengePlayer(string token, string username)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, GamesControllerUri);

            message.Headers.Add("Authorization", $"Bearer {token}");

            var challengeDto = new Challenge()
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

        public async Task<bool> SendMove<T>(string token, Guid matchId, T move) where T : BaseMove
        {
            var url = $"{GamesControllerUri.AbsoluteUri}/{matchId}";
            var uri = new Uri(url);
            var message = new HttpRequestMessage(HttpMethod.Put, uri);

            message.Headers.Add("Authorization", $"Bearer {token}");
            message.Headers.Add("Accept", $"application/json");

            var moveString = JsonConvert.SerializeObject(move/*, GetJsonSettings()*/);

            message.Content = new StringContent(moveString, Encoding.UTF8, "application/json");

            var resultMessage = await _client.SendAsync(message);

            if (!resultMessage.IsSuccessStatusCode)
            {
                return false;
            }

            //var resultString = await resultMessage.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<ChessGameDetails>(resultString);

            //var history = result.Representation.History;

            //result.Representation = new ChessRepresentationInitializer().Create();
            //var mechanism = new ChessMechanism();
            //foreach (var baseMove in history)
            //{
            //    result.Representation = mechanism.ApplyMove(result.Representation, baseMove);
            //}

            //return result;
            return true;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
