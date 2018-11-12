using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string _jwtToken;

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            panelGame.Visible = e.TabPage == tabPageGame;
            panelPlayers.Visible = e.TabPage == tabPagePlayers;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            ToggleLoginControls(false);
        }

        private void ToggleLoginControls(bool visible)
        {
            panelLogin.Visible = visible;
            panelLogout.Visible = !visible;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ToggleLoginControls(true);
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            chessBoardGamePanel1.Refresh();
        }
    }
}
