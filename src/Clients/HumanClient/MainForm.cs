using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.ChessGamesControllerModels;
using BoardGame.Model.Api.LadderControllerModels;
using BoardGame.ServiceClient;
using BoardGame.Tools.Common;

namespace BoardGame.HumanClient
{
    /// <summary>
    /// The main form's UI logic.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ChessMechanism _mechanism;
        private readonly ServiceConnection _client;

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

#if DEBUG
            textboxUsername.Text = "testUser1@testUser1.com";
            textboxPassword.Text = "testUser1@testUser1.com";
#else
            textboxUsername.Text = string.Empty;
            textboxPassword.Text = string.Empty;
#endif
        }

        private void LogNotification(string text)
        {
            textboxLog.DeselectAll();
            textboxLog.AppendText(Environment.NewLine);
            textboxLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Regular);
            textboxLog.SelectionColor = Color.Black;
            textboxLog.AppendText(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));

            textboxLog.SelectionFont = new Font(textboxLog.SelectionFont, FontStyle.Regular);
            textboxLog.SelectionColor = Color.DarkGreen;
            textboxLog.AppendText(text);
        }

        private void LogError(string text, Exception ex = null)
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

        private void RefreshGame()
        {
            if (_client.IsAnonymous || _client.CurrentGame?.Representation == null)
            {
                chessBoardGamePanel1.ChessRepresentation = new ChessRepresentation();
                chessBoardGamePanel1.Enabled = false;
                chessBoardGamePanel1.Refresh();
                return;
            }

            var details = _client.CurrentGame;
            listboxMoves.Items.Clear();

            var game = new ChessRepresentationInitializer().Create();

            foreach (var t in details.Representation.History)
            {
                game = _mechanism.ApplyMove(game, t);
                listboxMoves.Items.Add(t.ToString());
            }

            chessBoardGamePanel1.ChessRepresentation = game;

            labelGameState.Text = GetStatusLabelText();

            var isItMyTurn = _client.IsItMyTurn ?? false;

            if (!isItMyTurn)
            {
                btnAcceptDraw.Enabled = false;
                btnDeclineDraw.Enabled = false;
                btnOfferDraw.Enabled = false;
                btnResign.Enabled = false;
                chessBoardGamePanel1.Enabled = false;
            }
            else
            {
                var myColor = _client.CurrentGame.Representation.CurrentPlayer;
                var possibleSpecialMoves = _mechanism.GenerateMoves(game).Where(x => x.Owner == myColor).OfType<SpecialMove>().ToList();

                btnAcceptDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawAccept);
                btnDeclineDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawDecline);
                btnOfferDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawOffer);
                btnResign.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.Resign);
                chessBoardGamePanel1.Enabled = true;
            }

            chessBoardGamePanel1.Refresh();
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
            try
            {
                await RefreshClient();
                RefreshPlayers();
                RefreshMatches();
                RefreshGame();
            }
            catch (Exception ex)
            {
                LogError(nameof(RefreshAll), ex);
            }
        }

        private async Task RefreshLadder()
        {
            listviewLadder.Items.Clear();
            ICollection<LadderItem> result = null;

            try
            {
                result = await _client.GetLadder();
            }
            catch (Exception ex)
            {
                LogError(nameof(RefreshLadder), ex);
            }

            if (result == null)
            {
                LogError("Couldn't get ladder.");
                return;
            }

            var ordering = new List<LadderItem>();

            if (checkboxShowHumans.Checked)
            {
                ordering.AddRange(result.Where(x => !x.IsBot));
            }

            if (checkboxShowBots.Checked)
            {
                ordering.AddRange(result.Where(x => x.IsBot));
            }

            foreach (var ladderItem in ordering.OrderBy(x => x.Place))
            {
                listviewLadder.Items.Add(new ListViewItem()
                {
                    Text = $"{ladderItem.Place}",
                    ImageKey = ladderItem.IsBot ? "Robot" : "Brain",
                    SubItems =
                    {
                        ladderItem.Name,
                        $"{ladderItem.Points:F}"
                    }
                });
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
                    RefreshGame();
                    break;

                case Tabs.LadderPage:
                    await RefreshClient();
                    await RefreshLadder();
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

        private async void chessBoardGamePanel1_OnValidMoveSelected(object source, ChessboardMoveSelectedEventArg eventArg)
        {
            if (_client.IsAnonymous || _client.CurrentGame == null)
            {
                return;
            }

            try
            {
                await _client.SendMoveAsync(eventArg.Move);
            }
            catch (Exception ex)
            {
                LogError(nameof(chessBoardGamePanel1_OnValidMoveSelected), ex);
            }

            RefreshGame();

            chessBoardGamePanel1.ChessRepresentation = _client.CurrentGame.Representation;
            chessBoardGamePanel1.Refresh();
        }

        private async void tabLadder_Enter(object sender, EventArgs e)
        {
            await RefreshLadder();
        }

        private async void checkboxShowHumans_CheckedChanged(object sender, EventArgs e)
        {
            await RefreshLadder();
        }

        private async void checkboxShowBots_CheckedChanged(object sender, EventArgs e)
        {
            await RefreshLadder();
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

        private void btnResign_Click(object sender, EventArgs e)
        {

        }

        private void btnOfferDraw_Click(object sender, EventArgs e)
        {

        }

        private void btnAcceptDraw_Click(object sender, EventArgs e)
        {

        }

        private void btnDeclineDraw_Click(object sender, EventArgs e)
        {

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

        private void MainForm_Move(object sender, EventArgs e)
        {
            chessBoardGamePanel1.Refresh();
        }
    }
}
