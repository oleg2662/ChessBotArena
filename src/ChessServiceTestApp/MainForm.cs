using System;
using System.Windows.Forms;
using Game.Chess;
using Game.Chess.Moves;
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
            _client = new ChessServiceClient("http://poseen-001-site1.gtempurl.com");
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

            Text = $"Chess Client Tester App (Server version: v{result})";
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

        private async void listboxMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            var match = (DecoratedMatch)listboxMatches.SelectedItem;

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

        private async void listboxGameHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var match = (DecoratedMatch)listboxMatches.SelectedItem;

            if (match == null)
            {
                return;
            }

            labelStatus.Text = $"Getting details of selected match... ({match.Name})";
            var details = await _client.GetMatch(_jwtToken, match.Id.ToString());

            if (details?.Representation?.History == null)
            {
                MessageBox.Show("Couldn't get details of selected match. (Maybe unauthorized?)");
                labelStatus.Text = "Error while getting details of match";
                return;
            }

            var selectedHistoryItem = (BaseMove)listboxGameHistory.SelectedItem;

            var representation = new ChessRepresentationInitializer().Create();
            var mechanism = new ChessMechanism();
            var historyEnumerator = details.Representation.History.GetEnumerator();

            while (historyEnumerator.MoveNext())
            {
                var current = historyEnumerator.Current;
                if (current == null)
                {
                    break;
                }

                representation = mechanism.ApplyMove(representation, current);

                if (current.Equals(selectedHistoryItem))
                {
                    break;
                }
            }

            chessBoardVisualizerPictureBox1.ChessRepresentation = representation;
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
