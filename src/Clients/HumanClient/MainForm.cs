using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardGame.Game.Chess;
using BoardGame.Game.Chess.Moves;
using BoardGame.Model.Api.ChessGamesControllerModels;
using BoardGame.ServiceClient;
using BoardGame.Tools.Common;
using Easy.Common.Extensions;

namespace BoardGame.HumanClient
{
    public partial class MainForm : Form
    {
        private ChessMechanism _mechanism;
        private ChessRepresentation _game;
        private Guid? _selectedMatchId;
        private ChessServiceClientSession _client;
        private DateTime _lastUpdate = DateTime.MinValue;
        private int _countDown = 10;

        private readonly string _baseUrl = "http://localhost/BoardGame.Service";
        //private readonly string _baseUrl = "http://poseen-001-site1.gtempurl.com";

        public MainForm()
        {
            InitializeComponent();
            tabPageGame.Tag = Tabs.GamePage;
            tabPageMatches.Tag = Tabs.MatchesPage;
            tabPagePlayers.Tag = Tabs.PlayersPage;
        }

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            var page = (Tabs?) e.TabPage.Tag;

            panelGame.Visible = page == Tabs.GamePage;
            panelPlayers.Visible = page == Tabs.PlayersPage;
            panelMatches.Visible = page == Tabs.MatchesPage;

            RefreshAll();
        }

        private async void RefreshPlayers()
        {
            if (await CheckSessionValidity() == false)
            {
                return;
            }

            var players = await _client.GetPlayers();
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

        private async void RefreshMatches()
        {
            if (await CheckSessionValidity() == false)
            {
                return;
            }

            var matches = await _client.GetMatches();
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

                var details = await _client.GetMatch(match.Id.ToString());

                listViewMatches.Items.Add(new ListViewItem()
                {
                    ImageKey = imageKey,
                    Text = match.Name,
                    Tag = details
                });
            }

            tabPageMatches.Enabled = true;
        }

        private async void RefreshGame(bool force = false)
        {
            if (await CheckSessionValidity() == false || !_selectedMatchId.HasValue)
            {
                return;
            }

            var details = await _client.GetMatch(_selectedMatchId.Value.ToString());
            if (details == null)
            {
                MessageBox.Show("Couldn't load match details.");
                chessBoardGamePanel1.Enabled = true;
                tabPageGame.Enabled = true;
                return;
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

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private async void ToggleLoginControls()
        {
            _game = new ChessRepresentationInitializer().Create();
            _mechanism = new ChessMechanism();
            var isSessionAlive = await CheckSessionValidity();

            panelLogin.Visible = !isSessionAlive;
            panelLogout.Visible = isSessionAlive;
        }

        private async void Login()
        {
            _client = new ChessServiceClientSession(_baseUrl, textboxUsername.Text, textboxPassword.Text);
            var success = await _client.Initialize();

            if (!success)
            {
                MessageBox.Show("Login failed!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ToggleLoginControls();
                timerRefresh.Enabled = false;
            }
            else
            {
                labelLoginStatus.Text = $"{_client.LoginInformation.Username} logged in.";
                RefreshAll();
                ToggleLoginControls();
                timerRefresh.Enabled = true;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            timerRefresh.Enabled = false;
            _client?.Dispose();
            _client = null;
            ToggleLoginControls();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            chessBoardGamePanel1.Refresh();
        }

        private async Task<bool> CheckSessionValidity()
        {
            if (_client == null)
            {
                return false;
            }

            return await _client.EnsureSessionIsActive();
        }

        private async void btnChallenge_Click(object sender, EventArgs e)
        {
            if (await CheckSessionValidity() == false)
            {
                return;
            }

            if (listViewPlayers.SelectedItems.Count == 0)
            {
                return;
            }

            var selectedPlayer = listViewPlayers.SelectedItems[0].Text;

            var newGame = await _client.ChallengePlayer(selectedPlayer);

            if (newGame == null)
            {
                MessageBox.Show("Couldn't send challenge.");
                return;
            }

            _selectedMatchId = newGame.Id;
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

        private void listViewMatches_ItemActivate(object sender, EventArgs e)
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
                            if (details.WhitePlayer.UserName == _client.LoginInformation.Username)
                            {
                                sb.Append(" (You)");
                            }
                            break;
                        case ChessPlayer.Black:
                            if (details.WhitePlayer.UserName == _client.LoginInformation.Username)
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
            _selectedMatchId = details?.Id;

            if (_selectedMatchId == null)
            {
                return;
            }

            RefreshGame();
            //tabMain.SelectedTab = tabPageGame;
        }

        private void listViewMatches_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listViewMatches_ItemActivate(sender, e);

            if (_selectedMatchId == null)
            {
                return;
            }

            tabPageGame.Show();
        }

        private bool IsItMyTurn(ChessGameDetails details)
        {
            var representation = details.Representation;
            var state = _mechanism.GetGameState(details.Representation);

            if (state != GameState.InProgress)
            {
                return false;
            }

            var sb = new StringBuilder();

            switch (representation.CurrentPlayer)
            {
                case ChessPlayer.White:
                    if (details.WhitePlayer.UserName == _client.LoginInformation.Username)
                    {
                        return true;
                    }
                    break;

                case ChessPlayer.Black:
                    if (details.WhitePlayer.UserName == _client.LoginInformation.Username)
                    {
                        return true;
                    }
                    break;
            }

            labelMatchPreviewStatus.Text = sb.ToString();

            return false;
        }

        private ChessPlayer MyColorInGame(ChessGameDetails details)
        {
            var representation = details.Representation;

            switch (representation.CurrentPlayer)
            {
                case ChessPlayer.White:
                    if (details.WhitePlayer.UserName == _client.LoginInformation.Username)
                    {
                        return ChessPlayer.White;
                    }
                    else
                    {
                        return ChessPlayer.Black;
                    }

                case ChessPlayer.Black:
                    if (details.WhitePlayer.UserName == _client.LoginInformation.Username)
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
            if (await CheckSessionValidity() == false || !_selectedMatchId.HasValue)
            {
                return;
            }

            var result = await _client.SendMove(_selectedMatchId.Value, eventArg.Move);
            listboxMoves.Items.Add(eventArg.Move.ToString());

            if (!result)
            {
                MessageBox.Show("Couldn't send in move.");
                RefreshGame(true);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshAll(true);
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            _countDown--;
            btnSync.Text = $"Sync now (auto:{_countDown}s)";
            if (_countDown <= 0)
            {
                btnSync.Text = $"Syncing...";
                RefreshAll();
                _countDown = 10;
            }
        }
    }
}
