using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algorithms.Minimax;
using Game.Chess;
using Game.Chess.Moves;
using Model.Api.ChessGamesControllerModels;
using Model.Api.PlayerControllerModels;
using ServiceClient;

namespace MinimaxBot
{
    public partial class MainForm : Form
    {
        private static ChessServiceClient _client = new ChessServiceClient("http://localhost/BoardGame.Service");
        private readonly MinimaxAlgorithm<ChessRepresentation, BaseMove> _algorithm;
        private string _jwtToken;
        private bool _isActive = false;
        private string _username;
        private int _round = 1;
        private bool _isRobotThinking = false;
        private int _maxNumberOfMatches = 5;

        public MainForm()
        {
            InitializeComponent();

            var mechanism = new ChessMechanism();

            _algorithm = new MinimaxAlgorithm<ChessRepresentation, BaseMove>(
                                new Evaluator(mechanism),
                                new MoveGenerator(mechanism),
                                new MoveApplier(mechanism))
            {
                MaxDepth = 3
            };

            var task = new Task(() =>
            {
                do
                {
                    if (!_isActive)
                    {
                        // Not doing anything, checking in every second...
                        Thread.Sleep(1000);
                        continue;
                    }

                    DoRobotWork();

                    Thread.Sleep(1000);

                } while (true);
            });

            task.Start();
        }

        private void DoRobotWork()
        {
            if (_isRobotThinking)
            {
                return;
            }

            _isRobotThinking = true;
            UpdateLog($" == ROUND {_round} =================================================================== ");

            UpdateLog("Reading list of matches which are in progress...", 1);

            UpdateLog("Getting list of matches...", 2);
            var matches = _client.GetMatches(_jwtToken).Result;
            if (matches == null)
            {
                UpdateLog("ERROR: Couldn't get list of matches. (Maybe unauthorized?)", 2);
                return;
            }

            var playableMatches = new List<ChessGameDetails>();

            foreach (var chessGame in matches.Where(x => x.Outcome == GameState.InProgress))
            {
                UpdateLog($"Getting details of {chessGame.Name} ({chessGame.Id})...", 3);
                var details = _client.GetMatch(_jwtToken, chessGame.Id.ToString()).Result;
                if (details == null)
                {
                    UpdateLog($"ERROR: Couldn't query data of match with name {chessGame.Name} ({chessGame.Id})!", 3);
                    continue;
                }

                var myColour = details.BlackPlayer.UserName == _username
                    ? ChessPlayer.Black
                    : ChessPlayer.White;

                if (details.Representation.CurrentPlayer == myColour)
                {
                    playableMatches.Add(details);
                }
            }
            
            
            // Replying to matches where possible
            UpdateLog($"Replying to matches where possible...({playableMatches.Count})", 1);

            var stopWatch = new Stopwatch();
            foreach (var chessGameDetails in playableMatches)
            {
                stopWatch.Reset();
                stopWatch.Start();
                UpdateLog($"Running algorithm for game {chessGameDetails.Name} ({chessGameDetails.Id}...", 2);
                var representation = chessGameDetails.Representation;
                var move = _algorithm.Calculate(representation);
                stopWatch.Stop();
                UpdateLog($"Algorithm finished in {stopWatch.Elapsed.TotalSeconds:F} seconds and generated move: {move}", 2);
                UpdateLog($"Generated move: {move}", 2);
                UpdateLog($"Sending it in...", 2);
                var isMoveSuccess = _client.SendMove(_jwtToken, chessGameDetails.Id, move).Result;
                if (isMoveSuccess)
                {
                    UpdateLog($"Successfully updated match {chessGameDetails.Name} ({chessGameDetails.Id}).", 2);
                }
                else
                {
                    UpdateLog($"ERROR: Couldn't update match with calculated move.", 2);
                }
            }

            // Checking whether there is place for a new match and challenges a random player with no active match
            UpdateLog("Challenging users if possible...", 1);
            var players = _client.GetPlayers(_jwtToken).Result;

            var challengablePlayers = players.Select(x => new
            {
                Player = x,
                OpenForMatch = !playableMatches.Any(y => y.InitiatedBy.UserName == x.Name || y.Opponent.UserName == x.Name)
            }).Where(x => x.OpenForMatch)
                .Select(x => x.Player.Name)
                .ToList();

            UpdateLog($"Found {challengablePlayers.Count} players who may be open for a challenge.", 2);

            matches = _client.GetMatches(_jwtToken).Result;

            var numberOfOngoingMatches = matches.Count(x => x.Outcome == GameState.InProgress);
            var numberOfOpenGameSlots = (int) Math.Max(_maxNumberOfMatches - numberOfOngoingMatches, 0);

            if (numberOfOpenGameSlots > 0 && challengablePlayers.Any())
            {
                var p = challengablePlayers.OrderBy(x => Guid.NewGuid()).First();
                UpdateLog($"Challenging {p}...", 3);
                var newGame = _client.ChallengePlayer(_jwtToken, p).Result;
                if (newGame == null)
                {
                    UpdateLog($"ERROR: Failed...", 4);
                }
            }

            _isRobotThinking = false;
            _round++;
        }

        private void UpdateLog(string message, int level = 0)
        {
            var spacing = new string(' ', level);
            InvokeIfRequired(textboxBotMessages, () => textboxBotMessages.AppendText(spacing + message + Environment.NewLine));
        }

        public static void InvokeIfRequired(Control control, MethodInvoker action)
        {
            // See Update 2 for edits Mike de Klerk suggests to insert here.

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reconnect();
        }

        private async void Reconnect()
        {
            labelStatus.Text = "Connecting to server...";
            string result;

            try
            {
                result = await _client.GetVersion().ConfigureAwait(true);
            }
            catch (Exception)
            {
                MessageBox.Show("Service seems down");
                return;
            }

            Text = $"Minimax Bot (Server version: v{result})";

            labelStatus.Text = string.Empty;
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            labelStatus.Text = "Logging in...";
            string result;
            var username = textboxUsername.Text;
            var password = textboxPassword.Text;

            try
            {
                result = await _client.Login(username, password).ConfigureAwait(true);
            }
            catch (Exception)
            {
                MessageBox.Show("Service seems down.");
                labelStatus.Text = string.Empty;
                return;
            }

            if (result == null)
            {
                labelLoginStatus.Text = "Unauthorized";
                _jwtToken = null;
                labelStatus.Text = string.Empty;
                return;
            }

            _jwtToken = result;
            _username = username;

            labelLoginStatus.Text = "OK";
            labelStatus.Text = string.Empty;

            StartPlaying();
        }

        private void StartPlaying()
        {
            _isActive = true;
        }

        private void StopPlaying()
        {
            _isActive = false;
        }

        private void buttonReconnect_Click(object sender, EventArgs e)
        {
            Reconnect();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            StopPlaying();
        }
    }

    internal class DecoratedPlayer : Player
    {
        public DecoratedPlayer(string name, bool isBot)
        {
            Name = name;
            IsBot = isBot;
        }

        public override string ToString()
        {
            return IsBot ? $"{Name}(bot)" : $"{Name}(human)";
        }
    }

    internal class DecoratedMatch : ChessGame
    {
        public override string ToString()
        {
            return $"{Name}({Outcome})";
        }
    }
}
