using System;
using System.Collections.Generic;
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
        private ChessMechanism _mechanism;
        private ChessRepresentation _game;
        private Guid? _selectedMatchId;
        private readonly ServiceConnection _client;
        private DateTime _lastUpdate = DateTime.MinValue;
        private int _countDown = 10;

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

            _client = new ServiceConnection(_baseUrl);
            _client.CurrentGameChanged += ClientOnCurrentGameChanged;
            _client.LoginStarted += ClientOnLoginStarted;
            _client.LoginFinished += ClientOnLoginFinished;
            _client.MatchesListChanged += ClientOnMatchesListChanged;
            _client.PlayersListChanged += ClientOnPlayersListChanged;
            _client.PollFinished += ClientOnPollFinished;
            _client.PollStarted += ClientOnPollStarted;
        }

        private void ShowToolstripProgressBar()
        {
            statusStrip1.InvokeIfRequired(() => { toolStripProgressBar1.Visible = true; });
        }

        private void HideToolstripProgressBar()
        {
            statusStrip1.InvokeIfRequired(() => { toolStripProgressBar1.Visible = false; });
        }

        private void ClientOnPollStarted(object sender, ServiceConnectionEventArgs e)
        {
            ShowToolstripProgressBar();
        }

        private void ClientOnPollFinished(object sender, ServiceConnectionEventArgs e)
        {
            HideToolstripProgressBar();
        }

        private void ClientOnPlayersListChanged(object sender, ServiceConnectionEventArgs e)
        {
            RefreshPlayers();
        }

        private void ClientOnMatchesListChanged(object sender, ServiceConnectionEventArgs e)
        {
            RefreshMatches();
        }

        private void ClientOnLoginStarted(object sender, ServiceConnectionEventArgs e)
        {
            ShowToolstripProgressBar();
        }

        private void ClientOnLoginFinished(object sender, ServiceConnectionEventArgs e)
        {
            HideToolstripProgressBar();
        }

        private void ClientOnCurrentGameChanged(object sender, ServiceConnectionEventArgs e)
        {
            RefreshGame();
        }

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            var page = (Tabs?) e.TabPage.Tag;
            panelGame.Visible = page == Tabs.GamePage;
            panelPlayers.Visible = page == Tabs.PlayersPage;
            panelMatches.Visible = page == Tabs.MatchesPage;
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
                    Tag = match
                });
            }

            tabPageMatches.Enabled = true;
        }

        private void RefreshGame(bool force = false)
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

            if (!force)
            {
                var newList = details.Representation.History.Select(x => x.ToString());
                var oldList = listboxMoves.Items.OfType<string>();

                if (RandomOrderedSequelEquals(newList, oldList))
                {
                    tabPageGame.Enabled = true;
                    chessBoardGamePanel1.Enabled = true;
                    return;
                }
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

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            await Login();
        }

        private void ToggleLoginControls()
        {
            _game = new ChessRepresentationInitializer().Create();
            _mechanism = new ChessMechanism();
            var isSessionAlive = !_client.IsAnonymous;

            panelLogin.Visible = !isSessionAlive;
            panelLogout.Visible = isSessionAlive;
            timerRefresh.Enabled = isSessionAlive;
        }

        private async Task Login()
        {
            var success = await _client.LoginAsync(textboxUsername.Text, textboxPassword.Text);

            if (!success)
            {
                MessageBox.Show("Login failed!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ToggleLoginControls();
                //timerRefresh.Enabled = false;
            }
            else
            {
                labelLoginStatus.Text = $"{_client.LoggedInUser} logged in.";
                //RefreshAll();
                ToggleLoginControls();
                //timerRefresh.Enabled = true;
            }
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            //timerRefresh.Enabled = false;
            await _client.Logout();
            ToggleLoginControls();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            chessBoardGamePanel1.Refresh();
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

        private void RefreshAll(bool force = false)
        {
            if (!force && DateTime.Now - _lastUpdate < TimeSpan.FromSeconds(10))
            {
                return;
            }

            RefreshMatches();
            RefreshPlayers();
            RefreshGame();

            _lastUpdate = DateTime.Now;
        }

        private async void listViewMatches_ItemActivate(object sender, EventArgs e)
        {
            var listView = (sender as ListView);

            // ReSharper disable once PossibleNullReferenceException
            var item = (listView?.Items.Count ?? 0) > 0 ? listView.SelectedItems[0] : null;

            var details = (ChessGameDetails) item?.Tag;
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
                            if (details.WhitePlayer.UserName == _client.LoggedInUser)
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

            await _client.MakeCurrentMatchAsync(details?.Id);

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

        private async void chessBoardGamePanel1_OnValidMoveSelected(object source, ChessboardMoveSelectedEventArg eventArg)
        {
            if (_client.IsAnonymous || _client.CurrentGame == null)
            {
                return;
            }

            var result = await _client.SendMoveAsync(eventArg.Move);

            if (!result)
            {
                MessageBox.Show("Couldn't send in move.");
            }
        }

        private async void timerRefresh_Tick(object sender, EventArgs e)
        {
            _nextUpdate = DateTime.Now.AddMilliseconds(timerRefresh.Interval);
            await _client.Refresh();
        }

        private DateTime _nextUpdate = DateTime.Now;

        private async void tabLadder_Enter(object sender, EventArgs e)
        {
            await RefreshLadder();
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

        private async void checkboxShowHumans_CheckedChanged(object sender, EventArgs e)
        {
            await RefreshLadder();
        }

        private async void checkboxShowBots_CheckedChanged(object sender, EventArgs e)
        {
            await RefreshLadder();
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

        private void timerLabelRefresh_Tick(object sender, EventArgs e)
        {
            var remainingSeconds = (int)Math.Max((_nextUpdate - DateTime.Now).TotalSeconds, 0);
            toolStripStatusLabel1.Text = $"Next update in {remainingSeconds}s.";
        }
    }
}
