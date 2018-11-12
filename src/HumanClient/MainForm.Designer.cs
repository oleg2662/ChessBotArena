namespace HumanClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("wertw", "Brain");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("wertwrt");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("wertwrt");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "sgrsgsf",
            "a",
            "b",
            "gfsgfs"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPagePlayers = new System.Windows.Forms.TabPage();
            this.tabPageMatches = new System.Windows.Forms.TabPage();
            this.tabPageGame = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageListPlayers = new System.Windows.Forms.ImageList(this.components);
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.chessBoardGamePanel1 = new Tools.Common.ChessBoardGamePanel();
            this.panelLogout = new System.Windows.Forms.Panel();
            this.labelLoginStatus = new System.Windows.Forms.Label();
            this.panelGame = new System.Windows.Forms.Panel();
            this.listboxMoves = new System.Windows.Forms.ListBox();
            this.btnDeclineDraw = new System.Windows.Forms.Button();
            this.btnAcceptDraw = new System.Windows.Forms.Button();
            this.btnOfferDraw = new System.Windows.Forms.Button();
            this.btnResign = new System.Windows.Forms.Button();
            this.panelRefresh = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelPlayers = new System.Windows.Forms.Panel();
            this.btnChallenge = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPagePlayers.SuspendLayout();
            this.tabPageMatches.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.panelLogout.SuspendLayout();
            this.panelGame.SuspendLayout();
            this.panelRefresh.SuspendLayout();
            this.panelPlayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.tabMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelSidebar, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 642F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(939, 725);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPagePlayers);
            this.tabMain.Controls.Add(this.tabPageMatches);
            this.tabMain.Controls.Add(this.tabPageGame);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(3, 3);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(733, 719);
            this.tabMain.TabIndex = 0;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabMain_Selected);
            // 
            // tabPagePlayers
            // 
            this.tabPagePlayers.Controls.Add(this.listView1);
            this.tabPagePlayers.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlayers.Name = "tabPagePlayers";
            this.tabPagePlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlayers.Size = new System.Drawing.Size(677, 673);
            this.tabPagePlayers.TabIndex = 0;
            this.tabPagePlayers.Text = "Players";
            this.tabPagePlayers.UseVisualStyleBackColor = true;
            // 
            // tabPageMatches
            // 
            this.tabPageMatches.Controls.Add(this.listBox1);
            this.tabPageMatches.Location = new System.Drawing.Point(4, 22);
            this.tabPageMatches.Name = "tabPageMatches";
            this.tabPageMatches.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatches.Size = new System.Drawing.Size(677, 673);
            this.tabPageMatches.TabIndex = 1;
            this.tabPageMatches.Text = "Matches";
            this.tabPageMatches.UseVisualStyleBackColor = true;
            // 
            // tabPageGame
            // 
            this.tabPageGame.Controls.Add(this.chessBoardGamePanel1);
            this.tabPageGame.Location = new System.Drawing.Point(4, 22);
            this.tabPageGame.Name = "tabPageGame";
            this.tabPageGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGame.Size = new System.Drawing.Size(725, 693);
            this.tabPageGame.TabIndex = 2;
            this.tabPageGame.Text = "Game";
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(671, 667);
            this.listBox1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.AutoArrange = false;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listView1.LargeImageList = this.imageListPlayers;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(671, 667);
            this.listView1.SmallImageList = this.imageListPlayers;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // imageListPlayers
            // 
            this.imageListPlayers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPlayers.ImageStream")));
            this.imageListPlayers.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPlayers.Images.SetKeyName(0, "Robot");
            this.imageListPlayers.Images.SetKeyName(1, "Brain");
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Players";
            this.columnHeader1.Width = 604;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.panelPlayers);
            this.panelSidebar.Controls.Add(this.panelRefresh);
            this.panelSidebar.Controls.Add(this.panelGame);
            this.panelSidebar.Controls.Add(this.panelLogout);
            this.panelSidebar.Controls.Add(this.panelLogin);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSidebar.Location = new System.Drawing.Point(742, 3);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(194, 719);
            this.panelSidebar.TabIndex = 1;
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.textboxPassword);
            this.panelLogin.Controls.Add(this.labelPassword);
            this.panelLogin.Controls.Add(this.textboxUsername);
            this.panelLogin.Controls.Add(this.labelUsername);
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogin.Location = new System.Drawing.Point(0, 0);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Padding = new System.Windows.Forms.Padding(4);
            this.panelLogin.Size = new System.Drawing.Size(194, 113);
            this.panelLogin.TabIndex = 10;
            // 
            // btnLogout
            // 
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogout.Location = new System.Drawing.Point(4, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(186, 25);
            this.btnLogout.TabIndex = 14;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogin.Location = new System.Drawing.Point(4, 84);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(186, 25);
            this.btnLogin.TabIndex = 12;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textboxPassword
            // 
            this.textboxPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.textboxPassword.Location = new System.Drawing.Point(4, 64);
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.PasswordChar = '*';
            this.textboxPassword.Size = new System.Drawing.Size(186, 20);
            this.textboxPassword.TabIndex = 11;
            // 
            // labelPassword
            // 
            this.labelPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPassword.Location = new System.Drawing.Point(4, 44);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(186, 20);
            this.labelPassword.TabIndex = 10;
            this.labelPassword.Text = "Password";
            this.labelPassword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textboxUsername
            // 
            this.textboxUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.textboxUsername.Location = new System.Drawing.Point(4, 24);
            this.textboxUsername.Name = "textboxUsername";
            this.textboxUsername.Size = new System.Drawing.Size(186, 20);
            this.textboxUsername.TabIndex = 9;
            // 
            // labelUsername
            // 
            this.labelUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelUsername.Location = new System.Drawing.Point(4, 4);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(186, 20);
            this.labelUsername.TabIndex = 8;
            this.labelUsername.Text = "Username";
            this.labelUsername.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // chessBoardGamePanel1
            // 
            this.chessBoardGamePanel1.Bevel = System.Drawing.Color.Brown;
            this.chessBoardGamePanel1.BlackSquare = System.Drawing.Color.SandyBrown;
            this.chessBoardGamePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessBoardGamePanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.chessBoardGamePanel1.Location = new System.Drawing.Point(3, 3);
            this.chessBoardGamePanel1.Name = "chessBoardGamePanel1";
            this.chessBoardGamePanel1.Size = new System.Drawing.Size(719, 687);
            this.chessBoardGamePanel1.TabIndex = 0;
            this.chessBoardGamePanel1.WhiteSquare = System.Drawing.Color.BlanchedAlmond;
            // 
            // panelLogout
            // 
            this.panelLogout.Controls.Add(this.labelLoginStatus);
            this.panelLogout.Controls.Add(this.btnLogout);
            this.panelLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogout.Location = new System.Drawing.Point(0, 113);
            this.panelLogout.Name = "panelLogout";
            this.panelLogout.Padding = new System.Windows.Forms.Padding(4);
            this.panelLogout.Size = new System.Drawing.Size(194, 85);
            this.panelLogout.TabIndex = 14;
            this.panelLogout.Visible = false;
            // 
            // labelLoginStatus
            // 
            this.labelLoginStatus.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelLoginStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLoginStatus.Location = new System.Drawing.Point(4, 29);
            this.labelLoginStatus.Name = "labelLoginStatus";
            this.labelLoginStatus.Size = new System.Drawing.Size(186, 52);
            this.labelLoginStatus.TabIndex = 15;
            this.labelLoginStatus.Text = "Not logged in";
            this.labelLoginStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelGame
            // 
            this.panelGame.Controls.Add(this.listboxMoves);
            this.panelGame.Controls.Add(this.btnDeclineDraw);
            this.panelGame.Controls.Add(this.btnAcceptDraw);
            this.panelGame.Controls.Add(this.btnOfferDraw);
            this.panelGame.Controls.Add(this.btnResign);
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGame.Location = new System.Drawing.Point(0, 198);
            this.panelGame.Name = "panelGame";
            this.panelGame.Padding = new System.Windows.Forms.Padding(4);
            this.panelGame.Size = new System.Drawing.Size(194, 426);
            this.panelGame.TabIndex = 15;
            // 
            // listboxMoves
            // 
            this.listboxMoves.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxMoves.FormattingEnabled = true;
            this.listboxMoves.Location = new System.Drawing.Point(4, 148);
            this.listboxMoves.Name = "listboxMoves";
            this.listboxMoves.Size = new System.Drawing.Size(186, 274);
            this.listboxMoves.TabIndex = 4;
            // 
            // btnDeclineDraw
            // 
            this.btnDeclineDraw.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDeclineDraw.Location = new System.Drawing.Point(4, 112);
            this.btnDeclineDraw.Name = "btnDeclineDraw";
            this.btnDeclineDraw.Size = new System.Drawing.Size(186, 36);
            this.btnDeclineDraw.TabIndex = 3;
            this.btnDeclineDraw.Text = "Decline draw";
            this.btnDeclineDraw.UseVisualStyleBackColor = true;
            // 
            // btnAcceptDraw
            // 
            this.btnAcceptDraw.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAcceptDraw.Location = new System.Drawing.Point(4, 76);
            this.btnAcceptDraw.Name = "btnAcceptDraw";
            this.btnAcceptDraw.Size = new System.Drawing.Size(186, 36);
            this.btnAcceptDraw.TabIndex = 2;
            this.btnAcceptDraw.Text = "Accept draw";
            this.btnAcceptDraw.UseVisualStyleBackColor = true;
            // 
            // btnOfferDraw
            // 
            this.btnOfferDraw.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOfferDraw.Location = new System.Drawing.Point(4, 40);
            this.btnOfferDraw.Name = "btnOfferDraw";
            this.btnOfferDraw.Size = new System.Drawing.Size(186, 36);
            this.btnOfferDraw.TabIndex = 1;
            this.btnOfferDraw.Text = "Offer draw";
            this.btnOfferDraw.UseVisualStyleBackColor = true;
            // 
            // btnResign
            // 
            this.btnResign.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnResign.Location = new System.Drawing.Point(4, 4);
            this.btnResign.Name = "btnResign";
            this.btnResign.Size = new System.Drawing.Size(186, 36);
            this.btnResign.TabIndex = 0;
            this.btnResign.Text = "Resign";
            this.btnResign.UseVisualStyleBackColor = true;
            // 
            // panelRefresh
            // 
            this.panelRefresh.Controls.Add(this.btnRefresh);
            this.panelRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRefresh.Location = new System.Drawing.Point(0, 674);
            this.panelRefresh.Name = "panelRefresh";
            this.panelRefresh.Padding = new System.Windows.Forms.Padding(4);
            this.panelRefresh.Size = new System.Drawing.Size(194, 45);
            this.panelRefresh.TabIndex = 16;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRefresh.Location = new System.Drawing.Point(4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(186, 36);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh All";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // panelPlayers
            // 
            this.panelPlayers.Controls.Add(this.btnChallenge);
            this.panelPlayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPlayers.Location = new System.Drawing.Point(0, 624);
            this.panelPlayers.Name = "panelPlayers";
            this.panelPlayers.Padding = new System.Windows.Forms.Padding(4);
            this.panelPlayers.Size = new System.Drawing.Size(194, 45);
            this.panelPlayers.TabIndex = 17;
            // 
            // btnChallenge
            // 
            this.btnChallenge.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnChallenge.Location = new System.Drawing.Point(4, 4);
            this.btnChallenge.Name = "btnChallenge";
            this.btnChallenge.Size = new System.Drawing.Size(186, 36);
            this.btnChallenge.TabIndex = 1;
            this.btnChallenge.Text = "Challenge";
            this.btnChallenge.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 725);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Chess Client";
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabPagePlayers.ResumeLayout(false);
            this.tabPageMatches.ResumeLayout(false);
            this.tabPageGame.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelLogout.ResumeLayout(false);
            this.panelGame.ResumeLayout(false);
            this.panelRefresh.ResumeLayout(false);
            this.panelPlayers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPagePlayers;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabPage tabPageMatches;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabPage tabPageGame;
        private Tools.Common.ChessBoardGamePanel chessBoardGamePanel1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageListPlayers;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox textboxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Panel panelPlayers;
        private System.Windows.Forms.Button btnChallenge;
        private System.Windows.Forms.Panel panelRefresh;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.ListBox listboxMoves;
        private System.Windows.Forms.Button btnDeclineDraw;
        private System.Windows.Forms.Button btnAcceptDraw;
        private System.Windows.Forms.Button btnOfferDraw;
        private System.Windows.Forms.Button btnResign;
        private System.Windows.Forms.Panel panelLogout;
        private System.Windows.Forms.Label labelLoginStatus;
    }
}

