using System;
using System.Windows.Forms;
using Model.Api.ChessGamesControllerModels;
using Model.Api.PlayerControllerModels;
using ServiceClient;

namespace ChessServiceTestApp
{
    public partial class MainForm : Form
    {
        private readonly ChessServiceClient _client;

        private string _jwtToken;

        public MainForm()
        {
            InitializeComponent();
            _client = new ChessServiceClient("http://localhost/BoardGame.Service");
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            labelStatus.Text = "Connecting to server...";
            string result;
            tableLayoutMain.Enabled = false;
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
            tableLayoutMain.Enabled = true;
            labelStatus.Text = string.Empty;
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            labelStatus.Text = "Logging in...";
            string result;

            try
            {
                var username = textboxUsername.Text;
                var password = textboxPassword.Text;
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
            labelLoginStatus.Text = "OK";
            labelStatus.Text = string.Empty;

            RefreshLists();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private async void RefreshLists()
        {
            labelStatus.Text = "Getting list of players...";
            var players = await _client.GetPlayers(_jwtToken);
            listboxPlayers.Items.Clear();

            if (players == null)
            {
                MessageBox.Show("Couldn't get list of players. (Maybe unauthorized?)");
                labelStatus.Text = "Error while getting list of players.";
                return;
            }

            foreach (var player in players)
            {
                listboxPlayers.Items.Add(new DecoratedPlayer(player.Name, player.IsBot));
            }

            labelStatus.Text = "Getting list of matches...";
            var matches = await _client.GetMatches(_jwtToken);
            listboxMatches.Items.Clear();

            if (matches == null)
            {
                MessageBox.Show("Couldn't get list of matches. (Maybe unauthorized?)");
                labelStatus.Text = "Error while getting list of matches.";
                return;
            }

            foreach (var match in matches)
            {
                listboxMatches.Items.Add(new DecoratedMatch
                {
                    Name = match.Name,
                    Outcome = match.Outcome,
                    Opponent = match.Opponent,
                    BlackPlayer = match.BlackPlayer,
                    ChallengeDate = match.ChallengeDate,
                    Id = match.Id,
                    InitiatedBy = match.InitiatedBy,
                    LastMoveDate = match.LastMoveDate,
                    WhitePlayer = match.WhitePlayer
                });
            }
        }

        private async void buttonChallenge_Click(object sender, EventArgs e)
        {
            if (listboxPlayers.SelectedItem == null)
            {
                return;
            }

            var selectedUsername = ((Player) listboxPlayers.SelectedItem).Name;

            labelStatus.Text = $"Creating match against {selectedUsername}...";
            var details = await _client.ChallengePlayer(_jwtToken, selectedUsername);

            if (details == null)
            {
                MessageBox.Show($"Couldn't challenge user '{selectedUsername}'.");
                return;
            }

            RefreshLists();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private async void listboxMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            var match = (DecoratedMatch) listboxMatches.SelectedItem;

            if (match == null)
            {
                return;
            }

            labelStatus.Text = $"Getting details of selected match... ({match.Name})";
            var details = await _client.GetMatch(_jwtToken, match.Id.ToString());
            listboxGameHistory.Items.Clear();

            if (details?.Representation?.History == null)
            {
                MessageBox.Show("Couldn't get details of selected match. (Maybe unauthorized?)");
                labelStatus.Text = "Error while getting details of match";
                return;
            }

            foreach (var move in details.Representation.History)
            {
                listboxGameHistory.Items.Add(move);
            }
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
