using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
                RefreshAll();
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
                return;
            }

            var players = _client.Players;
            if (players == null)
            {
                tabPagePlayers.Enabled = true;
                MessageBox.Show("Couldn't get list of players.");
                return;
            }

            var newList = players.Select(x => x.Name).OrderBy(x => x).AsEnumerable();
            var oldList = listViewPlayers.Items.OfType<ListViewItem>().Select(x => x.Text).OrderBy(x => x)
                .AsEnumerable();

            if (RandomOrderedSequelEquals(oldList, newList))
            {
                return;
            }

            tabPagePlayers.Enabled = false;
            listViewPlayers.Items.Clear();

            foreach (var player in players)
            {
                listViewPlayers.Items.Add(new ListViewItem()
                {
                    ImageKey = player.IsBot ? "Robot" : "Brain",
                    Text = player.Name,
                });
            }

            tabPagePlayers.Enabled = true;
        }

        private static bool RandomOrderedSequelEquals<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstSet = first?.ToHashSet() ?? new HashSet<T>();
            var secondSet = second?.ToHashSet() ?? new HashSet<T>();
            return firstSet.SetEquals(secondSet);
        }

        private void RefreshMatches()
        {
            if (_client.IsAnonymous)
            {
                return;
            }

            var matches = _client.Matches;
            if (matches == null)
            {
                tabPageMatches.Enabled = true;
                MessageBox.Show("Couldn't get list of matches.");
                return;
            }

            var oldList = listViewMatches.Items.OfType<ListViewItem>()
                .Select(x => x.Tag as ChessGameDetails)
                .Where(x => x != null)
                .Select(x => x.Id)
                .OrderBy(x => x)
                .AsEnumerable();

            var selectedMatchId = listViewMatches?.SelectedItems.Count > 0
                                    ? ((ChessGameDetails)listViewMatches.SelectedItems[0].Tag)?.Id
                                    : null;

            var newList = matches.Select(x => x.Id).OrderBy(x => x).AsEnumerable();

            if (RandomOrderedSequelEquals(oldList, newList))
            {
                return;
            }

            tabPageMatches.Enabled = false;
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

            tabPageMatches.Enabled = true;
        }

        private void RefreshGame()
        {
            if (_client.IsAnonymous || _client.CurrentGame == null)
            {
                return;
            }

            var details = _client.CurrentGame;
            if (details == null)
            {
                chessBoardGamePanel1.Enabled = false;
                return;
            }
            else
            {
                chessBoardGamePanel1.Enabled = true;
            }

            var newList = details.Representation.History.Select(x => x.ToString());
            var oldList = listboxMoves.Items.OfType<string>();

            if (RandomOrderedSequelEquals(newList, oldList))
            {
                tabPageGame.Enabled = true;
                chessBoardGamePanel1.Enabled = true;
                return;
            }

            tabPageGame.Enabled = false;
            chessBoardGamePanel1.Enabled = false;

            listboxMoves.Items.Clear();

            var game = new ChessRepresentationInitializer().Create();

            foreach (var t in details.Representation.History)
            {
                game = _mechanism.ApplyMove(game, t);
                listboxMoves.Items.Add(t.ToString());
            }

            chessBoardGamePanel1.ChessRepresentation = game;

            var gameState = _mechanism.GetGameState(game);
            labelGameState.Text = GameStateToString(gameState, game.CurrentPlayer);

            var isItMyTurn = IsItMyTurn(details);

            if (!isItMyTurn)
            {
                btnAcceptDraw.Enabled = false;
                btnDeclineDraw.Enabled = false;
                btnOfferDraw.Enabled = false;
                btnResign.Enabled = false;
                tabPageGame.Enabled = true;
                chessBoardGamePanel1.Enabled = true;
                return;
            }

            var myColor = MyColorInGame(details);
            var possibleSpecialMoves = _mechanism.GenerateMoves(game).Where(x => x.Owner == myColor).OfType<SpecialMove>().ToList();

            btnAcceptDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawAccept);
            btnDeclineDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawDecline);
            btnOfferDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawOffer);
            btnResign.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.Resign);

            tabPageGame.Enabled = true;
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
                _client.StartAutoRefresh();
            }

            ToggleLoginControls();
            btnLogin.Enabled = true;

            RefreshAll();
        }

        private async Task Logout()
        {
            _client.StopAutoRefresh();
            await _client.Logout();
            ToggleLoginControls();
        }

        private void RefreshAll()
        {
            RefreshMatches();
            RefreshPlayers();
            RefreshGame();
        }

        private bool IsItMyTurn(ChessGameDetails details)
        {
            var representation = details.Representation;
            var state = _mechanism.GetGameState(details.Representation);

            if (state != GameState.InProgress)
            {
                return false;
            }

            switch (representation.CurrentPlayer)
            {
                case ChessPlayer.White:
                    if (details.WhitePlayer.UserName == _client.LoggedInUser)
                    {
                        return true;
                    }
                    break;

                case ChessPlayer.Black:
                    if (details.WhitePlayer.UserName == _client.LoggedInUser)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        private ChessPlayer MyColorInGame(ChessGameDetails details)
        {
            var representation = details.Representation;

            switch (representation.CurrentPlayer)
            {
                case ChessPlayer.White:
                    if (details.WhitePlayer.UserName == _client.LoggedInUser)
                    {
                        return ChessPlayer.White;
                    }
                    else
                    {
                        return ChessPlayer.Black;
                    }

                case ChessPlayer.Black:
                    if (details.WhitePlayer.UserName == _client.LoggedInUser)
                    {
                        return ChessPlayer.Black;
                    }
                    else
                    {
                        return ChessPlayer.White;
                    }
            }

            throw new ArgumentOutOfRangeException();
        }

        private async Task RefreshLadder()
        {
            var result = await _client.GetLadder();

            if (result == null)
            {
                MessageBox.Show("Couldn't get ladder.");
                return;
            }

            listviewLadder.Items.Clear();

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

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            var page = (Tabs?)e.TabPage.Tag;
            panelGame.Visible = page == Tabs.GamePage;
            panelPlayers.Visible = page == Tabs.PlayersPage;
            panelMatches.Visible = page == Tabs.MatchesPage;
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

            var originalState = chessBoardGamePanel1.ChessRepresentation;

            bool result;
            try
            {
                await _client.SendMoveAsync(eventArg.Move);
            }
            catch (Exception ex)
            {
                LogError(nameof(chessBoardGamePanel1_OnValidMoveSelected), ex);
                throw;
            }

            await _client.Refresh();
            chessBoardGamePanel1.ChessRepresentation = _client.CurrentGame.Representation;
            chessBoardGamePanel1.Refresh();
            RefreshAll();
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
            var needRefresh = await _client.Refresh();
            if (needRefresh)
            {
                RefreshAll();
            }
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

            var newGame = await _client.ChallengePlayerAsync(selectedPlayer);

            if (newGame == null)
            {
                MessageBox.Show("Couldn't send challenge.");
                return;
            }

            await _client.MakeCurrentMatchAsync(newGame.Id);
            tabPageMatches.Select();
        }

        private async void listViewMatches_ItemActivate(object sender, EventArgs e)
        {
            var listView = (sender as ListView);

            if (listView == null)
            {
                LogError("Couldn't find list view of matches.");
                return;
            }

            var item = (listView?.SelectedItems.Count ?? 0) > 0 ? listView.SelectedItems[0] : null;

            var details = (ChessGameDetails)item?.Tag;
            var representation = details?.Representation;

            labelMatchPreviewStatus.Text = string.Empty;

            if (representation != null)
            {
                chessBoardPreview.ChessRepresentation = representation;
                var state = _mechanism.GetGameState(representation);
                if (state == GameState.InProgress)
                {
                    var sb = new StringBuilder();

                    sb.Append($"Next: {representation.CurrentPlayer}");
                    switch (representation.CurrentPlayer)
                    {
                        case ChessPlayer.White:
                            if (details.WhitePlayer.UserName == _client.LoggedInUser)
                            {
                                sb.Append(" (You)");
                            }
                            break;
                        case ChessPlayer.Black:
                            if (details.BlackPlayer.UserName == _client.LoggedInUser)
                            {
                                sb.Append(" (You)");
                            }
                            break;

                        default:
                            break;
                    }

                    labelMatchPreviewStatus.Text = sb.ToString();
                }
                else
                {
                    labelMatchPreviewStatus.Text = state.ToString();
                }
            }

            try
            {
                listView.Enabled = false;
                await _client.MakeCurrentMatchAsync(details?.Id);
                RefreshGame();
            }
            catch (Exception ex)
            {
                LogError(nameof(listViewMatches), ex);
            }
            finally
            {
                listView.Enabled = true;
                if (item != null)
                {
                    item.Selected = true;
                }
            }
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
                RefreshAll();
                HideToolstripProgressBar();
            });
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            chessBoardGamePanel1.Refresh();
        }

        private static string GameStateToString(GameState state, ChessPlayer? nextPlayer = null)
        {
            switch (state)
            {
                case GameState.InProgress:
                    switch (nextPlayer)
                    {
                        case ChessPlayer.White: return "Next: White";
                        case ChessPlayer.Black: return "Next: Black";
                        default: return "In progress";
                    }
                case GameState.WhiteWon: return "White won";
                case GameState.BlackWon: return "Black won";
                case GameState.Draw: return "Draw";
                default: throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}
