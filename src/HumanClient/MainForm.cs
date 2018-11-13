using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Game.Chess;
using Game.Chess.Moves;
using Model.Api.ChessGamesControllerModels;
using ServiceClient;

namespace HumanClient
{
    public partial class MainForm : Form
    {
        private ChessMechanism _mechanism;
        private ChessRepresentation _game;
        private Guid? _selectedMatchId;
        private ChessServiceClientSession _client;
        private DateTime _lastUpdate = DateTime.MinValue;

        //private readonly string _baseUrl = "http://localhost/BoardGame.Service";
        private readonly string _baseUrl = "http://poseen-001-site1.gtempurl.com";

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

            tabPagePlayers.Enabled = false;
            var players = await _client.GetPlayers();
            if (players == null)
            {
                tabPagePlayers.Enabled = true;
                MessageBox.Show("Couldn't get list of players.");
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

            tabPagePlayers.Enabled = true;
        }

        private async void RefreshMatches()
        {
            if (await CheckSessionValidity() == false)
            {
                return;
            }

            tabPageMatches.Enabled = false;
            var matches = await _client.GetMatches();
            if (matches == null)
            {
                tabPageMatches.Enabled = true;
                MessageBox.Show("Couldn't get list of matches.");
                return;
            }

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

        private async void RefreshGame()
        {
            if (await CheckSessionValidity() == false || !_selectedMatchId.HasValue)
            {
                return;
            }

            tabPageGame.Enabled = false;
            var details = await _client.GetMatch(_selectedMatchId.Value.ToString());
            if (details == null)
            {
                MessageBox.Show("Couldn't load match details.");
                tabPageGame.Enabled = true;
                return;
            }

            listboxMoves.Items.Clear();

            foreach (var t in details.Representation.History)
            {
                listboxMoves.Items.Add(t.ToString());
            }

            chessBoardGamePanel1.ChessRepresentation = details.Representation;

            var isItMyTurn = IsItMyTurn(details);

            if (!isItMyTurn)
            {
                btnAcceptDraw.Enabled = false;
                btnDeclineDraw.Enabled = false;
                btnOfferDraw.Enabled = false;
                btnResign.Enabled = false;
                tabPageGame.Enabled = true;
                return;
            }

            var myColor = MyColorInGame(details);
            var possibleSpecialMoves = _mechanism.GenerateMoves(details.Representation).Where(x => x.Owner == myColor).OfType<SpecialMove>().ToList();

            btnAcceptDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawAccept);
            btnDeclineDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawDecline);
            btnOfferDraw.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.DrawOffer);
            btnResign.Enabled = possibleSpecialMoves.Any(x => x.Message == MessageType.Resign);

            tabPageGame.Enabled = true;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private async void ToggleLoginControls()
        {
            _game = new ChessRepresentationInitializer().Create();
            _mechanism = new ChessMechanism();
            var isSessionAlive = await CheckSessionValidity() == false;

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
            }
            else
            {
                labelLoginStatus.Text = $"{_client.LoginInformation.Username} logged in.";
                RefreshAll();
                ToggleLoginControls();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
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
            if (!force && DateTime.Now - _lastUpdate < TimeSpan.FromSeconds(30))
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
        }

        private void listViewMatches_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listViewMatches_ItemActivate(sender, e);

            if (_selectedMatchId == null)
            {
                return;
            }

            RefreshGame();
            tabMain.SelectedTab = tabPageGame;
            tabPageGame.Show();
        }

        private bool IsItMyTurn(ChessGameDetails details)
        {
            var representation = details.Representation;
            var state = _mechanism.GetGameState(details.Representation);

            if (state == GameState.InProgress)
            {
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
            }

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

        private async void chessBoardGamePanel1_OnValidMoveSelected(object source, Tools.Common.ChessboardMoveSelectedEventArg eventArg)
        {
            if (await CheckSessionValidity() == false || !_selectedMatchId.HasValue)
            {
                return;
            }

            var result = await _client.SendMove(_selectedMatchId.Value, eventArg.Move);

            if (!result)
            {
                MessageBox.Show("Couldn't send in move.");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshAll(true);
        }
    }
}
