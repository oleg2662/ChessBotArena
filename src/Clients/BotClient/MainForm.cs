using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Abstractions.Interfaces;
using BoardGame.Model.AlphaBeta;
using BoardGame.Model.Api.ChessGamesControllerModels;
using BoardGame.Model.Dumb;
using BoardGame.Model.Greedy;
using BoardGame.Model.Minimax;
using BoardGame.Model.MinimaxAverage;
using BoardGame.ServiceClient;

namespace BoardGame.BotClient
{
    public partial class MainForm : Form
    {
        private ChessServiceClient Client;

        private readonly IEvaluator<ChessRepresentation> _evaluator;
        private readonly IGenerator<ChessRepresentation, BaseMove> _generator;
        private readonly IApplier<ChessRepresentation, BaseMove> _applier;

        private IAlgorithm<ChessRepresentation, BaseMove> _algorithm;
        private static readonly Random Randomizer = new Random();
        private string _jwtToken;
        private bool _isActive = false;
        private string _username;
        private int _round = 1;
        private bool _isRobotThinking = false;
        private int _maxNumberOfMatches = 5;
#if DEBUG
        private readonly string _baseUrl = "http://localhost/BoardGame.Service";
#else
        private readonly string _baseUrl = "http://poseen-001-site1.gtempurl.com";
#endif

        private void ChangeAlgorithm()
        {
            Algorithms selectedAlgorithm = Algorithms.Minimax;
            int maxDepth = 2;

            InvokeIfRequired(listboxAlgorithms, () => selectedAlgorithm = (Algorithms)listboxAlgorithms.SelectedItem);
            InvokeIfRequired(numericMaxDepth, () => maxDepth = (int)numericMaxDepth.Value);

            switch (selectedAlgorithm)
            {
                case Algorithms.Minimax:
                    _algorithm = new MinimaxAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier)
                    {
                        MaxDepth = maxDepth
                    };
                    break;

                case Algorithms.MinimaxAverage:
                    _algorithm = new MinimaxAverageAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier)
                    {
                        MaxDepth = maxDepth
                    };
                    break;

                case Algorithms.AlphaBeta:
                    _algorithm = new AlphaBetaAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier)
                    {
                        MaxDepth = maxDepth
                    };
                    break;

                case Algorithms.Random:
                    _algorithm = new DumbAlgorithm<ChessRepresentation, BaseMove>(_generator);
                    break;

                case Algorithms.Greedy:
                    _algorithm = new GreedyAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier);
                    break;

                default:
                    _algorithm = new MinimaxAlgorithm<ChessRepresentation, BaseMove>(_evaluator, _generator, _applier);
                    break;
            }
        }

        internal enum Algorithms
        {
            Minimax,
            MinimaxAverage,
            AlphaBeta,
            Random,
            Greedy
        }

        public MainForm()
        {
            Client = new ChessServiceClient(_baseUrl);
            var mechanism = new ChessMechanism();
            _evaluator = new Evaluator(mechanism);
            _generator = new MoveGenerator(mechanism);
            _applier = new MoveApplier(mechanism);

            var task = new Task(() =>
            {
                do
                {
                    if (!_isActive)
                    {
                        InvokeIfRequired(progressbarBotActive, () =>
                        {
                            progressbarBotActive.MarqueeAnimationSpeed = 0;
                        });

                        // Not doing anything, checking in every second...
                        Thread.Sleep(1000);
                        continue;
                    }

                    InvokeIfRequired(progressbarBotActive, () =>
                    {
                        if (progressbarBotActive.MarqueeAnimationSpeed != 10)
                        {
                            progressbarBotActive.MarqueeAnimationSpeed = 10;
                        }
                    });

                    DoRobotWork();

                    Thread.Sleep(1000);

                } while (true);
            });

            InitializeComponent();

            listboxAlgorithms.Items.Add(Algorithms.Minimax);
            listboxAlgorithms.Items.Add(Algorithms.MinimaxAverage);
            listboxAlgorithms.Items.Add(Algorithms.AlphaBeta);
            listboxAlgorithms.Items.Add(Algorithms.Greedy);
            listboxAlgorithms.Items.Add(Algorithms.Random);
            listboxAlgorithms.SelectedIndex = Randomizer.Next(0, listboxAlgorithms.Items.Count - 1);

            task.Start();
        }

        private void DoRobotWork()
        {
            if (_isRobotThinking)
            {
                return;
            }

            ChangeAlgorithm();

            _isRobotThinking = true;
            UpdateLog($" == ROUND {_round} =================================================================== ");

            UpdateLog("Reading list of matches which are in progress...", 1);

            UpdateLog("Getting list of matches...", 2);
            var matches = Client.GetMatches(_jwtToken).Result;
            if (matches == null)
            {
                UpdateLog("ERROR: Couldn't get list of matches. (Maybe unauthorized?)", 2);
                _isRobotThinking = false;
                _round++;
                return;
            }

            var playableMatches = new List<ChessGameDetails>();
            var inProgressMatches = matches.Where(x => x.Outcome == GameState.InProgress).ToList();
            foreach (var chessGame in inProgressMatches)
            {
                UpdateLog($"Getting details of {chessGame.Name} ({chessGame.Id})...", 3);
                var details = Client.GetMatch(_jwtToken, chessGame.Id.ToString()).Result;
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
                InvokeIfRequired(progressbarAlgorithm, () => progressbarAlgorithm.MarqueeAnimationSpeed = 10);
                var representation = chessGameDetails.Representation;
                var move = _algorithm.Calculate(representation);
                InvokeIfRequired(progressbarAlgorithm, () => progressbarAlgorithm.MarqueeAnimationSpeed = 0);
                stopWatch.Stop();
                UpdateLog($"Algorithm finished in {stopWatch.Elapsed.TotalSeconds:F} seconds and generated move: {move}", 2);
                UpdateLog($"Sending it in...", 2);
                var isMoveSuccess = Client.SendMove(_jwtToken, chessGameDetails.Id, move).Result;

                UpdateLog(
                    isMoveSuccess
                        ? $"Successfully updated match {chessGameDetails.Name} ({chessGameDetails.Id})."
                        : $"ERROR: Couldn't update match with calculated move.", 2);
            }

            // Checking whether there is place for a new match and challenges a random player with no active match
            if (playableMatches.Count > _maxNumberOfMatches)
            {
                _isRobotThinking = false;
                _round++;
                return;
            }

            UpdateLog("Challenging users if possible...", 1);
            var players = Client.GetPlayers(_jwtToken).Result;
            if (players == null)
            {
                UpdateLog("ERROR: Couldn't get list of players.", 2);
                return;
            }

            var nonChallengedPlayer = players.Where(x =>
                !inProgressMatches.Any(y => y.InitiatedBy.UserName == x.Name || y.Opponent.UserName == x.Name))
                .Select(x => x.Name)
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefault();

            if (nonChallengedPlayer == null)
            {
                UpdateLog($"Found no one to challenge. :(", 2);
                _isRobotThinking = false;
                _round++;
                return;
            }

            UpdateLog($"Challenging {nonChallengedPlayer}...", 2);

            var newGame = Client.ChallengePlayer(_jwtToken, nonChallengedPlayer).Result;
            if (newGame == null)
            {
                UpdateLog($"ERROR: Failed...", 3);
                _isRobotThinking = false;
                _round++;
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
                result = await Client.GetVersion().ConfigureAwait(true);
            }
            catch (Exception)
            {
                MessageBox.Show("Service seems down");
                return;
            }

            Text = $"Bot (Server version: v{result})";

            labelStatus.Text = string.Empty;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            StopPlaying();

            RefreshLoginToken();

            StartPlaying();
        }

        private async void RefreshLoginToken(string username, string password)
        {
            labelStatus.Text = "Logging in...";
            LoginResult result;

            try
            {
                result = await Client.Login(username, password).ConfigureAwait(true);
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

            _jwtToken = result.TokenString;
            _username = result.Username;

            labelLoginStatus.Text = $"({username}) logged in.";
            labelStatus.Text = string.Empty;
        }

        private void RefreshLoginToken()
        {
            var username = textboxUsername.Text;
            var password = textboxPassword.Text;

            RefreshLoginToken(username, password);
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
            _jwtToken = null;
            labelLoginStatus.Text = $"({_username}) logged out.";
            _username = null;

            StopPlaying();
        }
    }

}
