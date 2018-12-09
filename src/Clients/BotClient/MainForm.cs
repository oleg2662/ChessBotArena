using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardGame.Algorithms.Abstractions.Interfaces;
using BoardGame.Algorithms.AlphaBeta;
using BoardGame.Algorithms.Greedy;
using BoardGame.Algorithms.Minimax;
using BoardGame.Algorithms.MinimaxAverage;
using BoardGame.Algorithms.Random;
using BoardGame.BotClient.Evaluators;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.ChessGamesControllerModels;
using BoardGame.ServiceClient;
using BoardGame.Tools.Common;

namespace BoardGame.BotClient
{
    public partial class MainForm : Form
    {
        private readonly ChessMechanism _mechanism;
        private readonly ServiceConnection _client;

        private static readonly SemaphoreSlim RefreshSemaphore = new SemaphoreSlim(1, 1);

#if DEBUG
        private readonly string _baseUrl = "http://localhost/BoardGame.Service";
#else
        private readonly string _baseUrl = "http://poseen-001-site1.gtempurl.com";
#endif

        public MainForm()
        {
            InitializeComponent();
            tabPageGame.Tag = Tabs.GamePage;
            tabPageMatches.Tag = Tabs.MatchesPage;
            tabPagePlayers.Tag = Tabs.PlayersPage;
            tabPageLog.Tag = Tabs.LogPage;
            _mechanism = new ChessMechanism();

            _client = new ServiceConnection(_baseUrl);
            _client.PollFinished += ClientOnPollFinished;
            _client.PollStarted += ClientOnPollStarted;
            _client.BackgroundError += ClientOnBackgroundError;

            comboboxAlgorithms.Items.Add(new AlgorithmItem("Minimax", typeof(MinimaxAlgorithm<ChessRepresentation, BaseMove>)));
            comboboxAlgorithms.Items.Add(new AlgorithmItem("Minimax Average", typeof(MinimaxAverageAlgorithm<ChessRepresentation, BaseMove>)));
            comboboxAlgorithms.Items.Add(new AlgorithmItem("Alpha Beta", typeof(AlphaBetaAlgorithm<ChessRepresentation, BaseMove>)));
            comboboxAlgorithms.Items.Add(new AlgorithmItem("Greedy", typeof(GreedyAlgorithm<ChessRepresentation, BaseMove>)));
            comboboxAlgorithms.Items.Add(new AlgorithmItem("Random", typeof(RandomAlgorithm<ChessRepresentation, BaseMove>)));

            comboboxEvaluators.Items.Add(new EvaluatorItem("Version 1", typeof(Version1Evaluator)));
            comboboxEvaluators.Items.Add(new EvaluatorItem("Version 2", typeof(Version1Evaluator)));

            comboboxAlgorithms.SelectedIndex = 0;
            comboboxEvaluators.SelectedIndex = 0;

            textboxLog.SelectionFont = new Font(FontFamily.GenericMonospace, 8.25f);
            textboxBotLog.SelectionFont = new Font(FontFamily.GenericMonospace, 8.25f);

            textboxReadme.Rtf = BotClientResources.BotClientDoc;

#if DEBUG
            textboxUsername.Text = "testBot1@testBot1.com";
            textboxPassword.Text = "testBot1@testBot1.com";
#else
            textboxUsername.Text = string.Empty;
            textboxPassword.Text = string.Empty;
#endif
        }

        private void LogNotification(string text)
        {
            textboxLog.InvokeIfRequired(() => 
            {
                textboxLog.DeselectAll();
                textboxLog.AppendText(Environment.NewLine);
                textboxLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Regular);
                textboxLog.SelectionColor = Color.Black;
                textboxLog.AppendText(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));

                textboxLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Regular);
                textboxLog.SelectionColor = Color.DarkGreen;
                textboxLog.AppendText(text);

                // set the current caret position to the end
                textboxLog.SelectionStart = textboxLog.Text.Length;
                // scroll it automatically
                textboxLog.ScrollToCaret();
            });
        }

        private void LogError(string text, Exception ex = null)
        {
            textboxLog.InvokeIfRequired(() =>
            {
                textboxLog.DeselectAll();
                textboxLog.AppendText(Environment.NewLine);
                textboxLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Bold);
                textboxLog.SelectionColor = Color.Black;
                textboxLog.AppendText(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));

                textboxLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Bold);
                textboxLog.SelectionColor = Color.DarkRed;
                textboxLog.AppendText(text);

                if (ex == null)
                {
                    // set the current caret position to the end
                    textboxLog.SelectionStart = textboxLog.Text.Length;
                    // scroll it automatically
                    textboxLog.ScrollToCaret();

                    return;
                }

                textboxLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Regular);
                textboxLog.SelectionColor = Color.DarkOrange;
                textboxLog.AppendText(Environment.NewLine);
                textboxLog.AppendText(ex.Source);
                textboxLog.AppendText(Environment.NewLine);
                textboxLog.AppendText(ex.Message);
                textboxLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Italic);
                textboxLog.AppendText(Environment.NewLine);
                textboxLog.AppendText(ex.StackTrace);

                // set the current caret position to the end
                textboxLog.SelectionStart = textboxLog.Text.Length;
                // scroll it automatically
                textboxLog.ScrollToCaret();
            });
        }

        /// <summary>
        /// Used by the bot's task to log what she is doing. Second parameter defines if it's a normal, warning or error message.
        /// </summary>
        /// <param name="text">The text to be logged.</param>
        /// <param name="isError">
        /// If NULL (default) then it's a normal message (green).
        /// If it's false, then it's an warning (orange).
        /// If it's true then it's an error message (red).
        /// </param>
        private void LogBotWork(string text, bool? isError = null)
        {
            textboxLog.InvokeIfRequired(() =>
            {
                var colour = !isError.HasValue
                    ? Color.Green
                    : isError.Value
                        ? Color.Red
                        : Color.Orange;

                textboxBotLog.DeselectAll();
                textboxBotLog.AppendText(Environment.NewLine);
                textboxBotLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Bold);
                textboxBotLog.SelectionColor = Color.Black;
                textboxBotLog.AppendText(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));

                textboxBotLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Bold);
                textboxBotLog.SelectionColor = colour;
                textboxBotLog.AppendText(text);

                // set the current caret position to the end
                textboxBotLog.SelectionStart = textboxBotLog.Text.Length;
                // scroll it automatically
                textboxBotLog.ScrollToCaret();
            });
        }

        private void ClientOnBackgroundError(object sender, ServiceConnectionEventArgs e)
        {
            this.InvokeIfRequired(() =>
            {
                LogError($"({e.CallerMemberName}){e.Message}", e.Exception);
            });
        }

        private void ShowToolstripProgressBar()
        {
            toolStripProgressBar1.Visible = true;
        }

        private void HideToolstripProgressBar()
        {
            toolStripProgressBar1.Visible = false;
        }

        private void RefreshPlayers()
        {
            if (_client.IsAnonymous)
            {
                listViewPlayers.Items.Clear();
                return;
            }

            var players = _client.Players;
            if (players == null)
            {
                LogError("Couldn't get list of players.");
                return;
            }

            listViewPlayers.Items.Clear();

            foreach (var player in players)
            {
                listViewPlayers.Items.Add(new ListViewItem()
                {
                    ImageKey = player.IsBot ? "Robot" : "Brain",
                    Text = player.Name,
                });
            }
        }

        private async Task RefreshClient()
        {
            try
            {
                await _client.Refresh();
            }
            catch (Exception ex)
            {
                LogError(nameof(RefreshMatches), ex);
            }
        }

        private void RefreshMatches()
        {
            if (_client.IsAnonymous)
            {
                listViewMatches.Items.Clear();
                return;
            }

            listViewMatches.Enabled = false;
            var matches = _client.Matches;
            if (matches == null)
            {
                listViewMatches.Enabled = true;
                LogError("Couldn't get list of matches.");
                return;
            }

            var selectedMatch = listViewMatches?.SelectedItems.Count > 0
                ? (ChessGameDetails)listViewMatches.SelectedItems[0].Tag
                : null;

            var selectedMatchId = selectedMatch?.Id;

            listViewMatches.Items.Clear();

            foreach (var match in matches)
            {
                string imageKey;
                switch (match.Outcome)
                {
                    case GameState.InProgress:
                        imageKey = "InProgress";
                        break;

                    case GameState.WhiteWon:
                        imageKey = "WhiteWon";
                        break;

                    case GameState.BlackWon:
                        imageKey = "BlackWon";
                        break;

                    case GameState.Draw:
                        imageKey = "Draw";
                        break;

                    default:
                        imageKey = string.Empty;
                        break;
                }

                listViewMatches.Items.Add(new ListViewItem()
                {
                    ImageKey = imageKey,
                    Text = match.Name,
                    Tag = match,
                    Selected = match.Id.Equals(selectedMatchId)
                });
            }

            RefreshMatchPreview();
            listViewMatches.Enabled = true;
        }

        private void ToggleLoginControls()
        {

            var isSessionAlive = !_client.IsAnonymous;

            panelLogin.Visible = !isSessionAlive;
            panelLogout.Visible = isSessionAlive;
        }

        private async Task Login()
        {
            btnLogin.Enabled = false;
            bool success = false;
            try
            {
                success = await _client.LoginAsync(textboxUsername.Text, textboxPassword.Text);
                if (success)
                {
                    await _client.Refresh();
                }
            }
            catch (Exception e)
            {
                LogError("Error at login.", e);
                success = false;
            }

            if (!success)
            {
                MessageBox.Show("Login failed!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError($"Unsuccessful login for {textboxUsername.Text}.");
            }
            else
            {
                labelLoginStatus.Text = $"{_client.LoggedInUser} logged in.";
                LogNotification($"Successful login: {_client.LoggedInUser}");
            }

            ToggleLoginControls();
            btnLogin.Enabled = true;

            await RefreshAll();
        }

        private async Task Logout()
        {
            _client.StopAutoRefresh();
            await _client.Logout();
            await RefreshAll();
            ToggleLoginControls();
        }

        private async Task RefreshAll()
        {
            await RefreshSemaphore.WaitAsync();
            try
            {
                await RefreshClient();
                RefreshPlayers();
                RefreshMatches();
            }
            catch (Exception ex)
            {
                LogError(nameof(RefreshAll), ex);
            }
            finally
            {
                RefreshSemaphore.Release(1);
            }
        }

        private async void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            var page = (Tabs?)e.TabPage.Tag;
            panelGame.Visible = page == Tabs.GamePage;
            panelPlayers.Visible = page == Tabs.PlayersPage;
            panelMatches.Visible = page == Tabs.MatchesPage;

            switch (page)
            {
                case Tabs.PlayersPage:
                    await RefreshClient();
                    RefreshPlayers();
                    break;

                case Tabs.MatchesPage:
                    await RefreshClient();
                    RefreshMatches();
                    break;

                case Tabs.GamePage:
                    await RefreshClient();
                    break;

                case Tabs.ReadmePage:
                    break;

                case Tabs.LogPage:
                    textboxLog.SelectionStart = textboxLog.TextLength;
                    textboxLog.ScrollToCaret();
                    break;
            }
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            await Login();
        }

        private async void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            await RefreshAll();
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            await Logout();
        }

        private async void btnChallenge_Click(object sender, EventArgs e)
        {
            if (_client.IsAnonymous)
            {
                return;
            }

            if (listViewPlayers.SelectedItems.Count == 0)
            {
                return;
            }

            var selectedPlayer = listViewPlayers.SelectedItems[0].Text;

            try
            {
                btnChallenge.Enabled = false;
                var newGame = await _client.ChallengePlayerAsync(selectedPlayer);
                if (newGame == null)
                {
                    LogError("Couldn't send challenge.");
                    btnChallenge.Enabled = true;
                    return;
                }
                _client.SetCurrentMatchById(newGame.Id);
                tabPageMatches.Select();
            }
            finally
            {
                btnChallenge.Enabled = true;
            }
        }

        private string GetStatusLabelText()
        {
            var gameState = _client.CurrentGameState;
            var isItMyTurn = _client.IsItMyTurn;

            if (!isItMyTurn.HasValue)
            {
                return string.Empty;
            }

            switch (gameState)
            {
                case GameState.InProgress:
                    var currentPlayer = _client.CurrentGame.Representation.CurrentPlayer.ToString();
                    return isItMyTurn.Value
                        ? $"{currentPlayer} (you!)"
                        : $"{currentPlayer}";

                case GameState.WhiteWon:
                    return isItMyTurn.Value ? "White won! (You!)" : "White won!";

                case GameState.BlackWon:
                    return isItMyTurn.Value ? "Black won! (You!)" : "Black won!";

                case GameState.Draw:
                    return "Draw!";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void listViewMatches_ItemActivate(object sender, EventArgs e)
        {
            RefreshMatchPreview();
        }

        private void RefreshMatchPreview()
        {
            if (listViewMatches == null)
            {
                LogError("Couldn't find list view of matches.");
                return;
            }

            var item = (listViewMatches?.SelectedItems.Count ?? 0) > 0 ? listViewMatches.SelectedItems[0] : null;

            var details = (ChessGameDetails)item?.Tag;
            if (details == null)
            {
                labelMatchPreviewStatus.Text = "-";
                chessBoardPreview.ChessRepresentation = new ChessRepresentation();
                return;
            }
            _client.SetCurrentMatchById(details.Id);
            labelMatchPreviewStatus.Text = GetStatusLabelText();
            chessBoardPreview.ChessRepresentation = details.Representation;
        }

        private async Task DoBotWork()
        {
            await RefreshAll();
            await RefreshSemaphore.WaitAsync();
            try
            {
                var matches = _client.Matches
                    .Where(x => x?.Representation?.CurrentPlayer != null)
                    .Where(x => _mechanism.GetGameState(x.Representation) == GameState.InProgress)
                    .ToArray();

                if (!matches.Any())
                {
                    //LogNotification($"{nameof(DoBotWork)}: No match found to answer to.");
                    //LogBotWork("No match found to answer to.", false);
                    return;
                }

                foreach (var match in matches)
                {
                    if (match?.Outcome != GameState.InProgress)
                    {
                        continue;
                    }

                    var myColour = match?.BlackPlayer?.UserName == _client.LoggedInUser
                        ? ChessPlayer.Black
                        : ChessPlayer.White;

                    if (match?.Representation?.CurrentPlayer != myColour)
                    {
                        continue;
                    }

                    _client.SetCurrentMatchById(match.Id);

                    var algoItem = (AlgorithmItem)comboboxAlgorithms.SelectedItem;
                    var evaulatorItem = (EvaluatorItem)comboboxEvaluators.SelectedItem;
                    if (algoItem == null || evaulatorItem == null)
                    {
                        LogError($"{nameof(DoBotWork)}: No algorithm or evaluator selected!");
                        LogBotWork("No algorithm or evaluator selected!", true);
                        continue;
                    }

                    // Running algorithm...
                    var algorithm = GetAlgorithm(algoItem.AlgorithmType, evaulatorItem.EvaluatorType);
                    var start = DateTime.Now;
                    LogBotWork($"Generating answer for match '{match.Name}'.{Environment.NewLine}{match.Id}");
                    var algoTask = Task.Run(() => { return algorithm.Calculate(match.Representation); });
                    var move = await algoTask;
                    var duration = (DateTime.Now - start).TotalSeconds;
                    LogBotWork($"Generated move: {duration} sec.");
                    if (move != null)
                    {
                        _client.SetCurrentMatchById(match.Id);
                        await _client.SendMoveAsync(move);
                        LogBotWork($"Generated move:{Environment.NewLine}{move.ToString()}{Environment.NewLine}{Environment.NewLine}");
                    }
                    else
                    {
                        LogBotWork($"NULL move returned!", true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(nameof(DoBotWork), ex);
            }
            finally
            {
                if (!timerRefresh.Enabled)
                {
                    btnCalculate.InvokeIfRequired(() =>
                    {
                        btnCalculate.Enabled = true;
                        btnCalculate.Text = "Start";
                    });
                }
                else
                {
                    btnCalculate.InvokeIfRequired(() =>
                    {
                        HideToolstripProgressBar();
                        btnCalculate.Enabled = true;
                        btnCalculate.Text = "Stop";
                    });
                }
                RefreshSemaphore.Release(1);
            }
        }

        private IAlgorithm<ChessRepresentation, BaseMove> GetAlgorithm(Type algorithmType, Type evaluatorType)
        {
            var generator = new MoveGenerator(_mechanism);
            var applier = new MoveApplier(_mechanism);
            var evaluator = (IEvaluator<ChessRepresentation>) Activator.CreateInstance(evaluatorType, new[] {_mechanism});
            
            // TODO : A bit hacky, refactor later!
            if (algorithmType == typeof(MinimaxAlgorithm<ChessRepresentation, BaseMove>))
            {
                var minimax = new MinimaxAlgorithm<ChessRepresentation, BaseMove>(evaluator, generator, applier)
                {
                    MaxDepth = (int) numericUpDown1.Value
                };
                return minimax;
            }

            if (algorithmType == typeof(AlphaBetaAlgorithm<ChessRepresentation, BaseMove>))
            {
                var ab = new AlphaBetaAlgorithm<ChessRepresentation, BaseMove>(evaluator, generator, applier)
                {
                    MaxDepth = (int) numericUpDown1.Value
                };
                return ab;
            }

            if (algorithmType == typeof(MinimaxAverageAlgorithm<ChessRepresentation, BaseMove>))
            {
                var minimaxAvg = new MinimaxAverageAlgorithm<ChessRepresentation, BaseMove>(evaluator, generator, applier)
                {
                    MaxDepth = (int) numericUpDown1.Value
                };
                return minimaxAvg;
            }

            if (algorithmType == typeof(GreedyAlgorithm<ChessRepresentation, BaseMove>))
            {
                var greedy = new GreedyAlgorithm<ChessRepresentation, BaseMove>(evaluator, generator, applier);
                return greedy;
            }

            if (algorithmType == typeof(RandomAlgorithm<ChessRepresentation, BaseMove>))
            {
                var randomAlgorithm = new RandomAlgorithm<ChessRepresentation, BaseMove>(generator);
                return randomAlgorithm;
            }

            throw new ArgumentOutOfRangeException(nameof(algorithmType));
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (_client.IsAnonymous)
            {
                return;
            }

            if (RefreshSemaphore.CurrentCount > 0)
            {
                btnCalculate.Text = "Stop";
                btnCalculate.Enabled = true;
                timerRefresh.Enabled = true;
            }
            else
            {
                btnCalculate.Text = "Stopping...";
                btnCalculate.Enabled = false;
                timerRefresh.Enabled = false;
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ClientOnPollStarted(object sender, ServiceConnectionEventArgs e)
        {
            this.InvokeIfRequired(() =>
            {
                ShowToolstripProgressBar();
                LogNotification($"[POLLSTART]({e.CallerMemberName}){e.Message}");
            });
        }

        private void ClientOnPollFinished(object sender, ServiceConnectionEventArgs e)
        {
            this.InvokeIfRequired(() =>
            {
                LogNotification($"[POLLEND]({e.CallerMemberName}){e.Message}");
                HideToolstripProgressBar();
            });
        }

        private async void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (RefreshSemaphore.CurrentCount == 0)
            {
                return;
            }

            await DoBotWork();
        }
    }
}
