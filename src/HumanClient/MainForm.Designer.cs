using Tools.Common;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPagePlayers = new System.Windows.Forms.TabPage();
            this.listViewPlayers = new System.Windows.Forms.ListView();
            this.imageListPlayers = new System.Windows.Forms.ImageList(this.components);
            this.tabPageMatches = new System.Windows.Forms.TabPage();
            this.listViewMatches = new System.Windows.Forms.ListView();
            this.columnHeaderMatchName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListMatchStatuses = new System.Windows.Forms.ImageList(this.components);
            this.tabPageGame = new System.Windows.Forms.TabPage();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelMatches = new System.Windows.Forms.Panel();
            this.labelMatchPreviewStatus = new System.Windows.Forms.Label();
            this.panelPlayers = new System.Windows.Forms.Panel();
            this.btnChallenge = new System.Windows.Forms.Button();
            this.panelRefresh = new System.Windows.Forms.Panel();
            this.btnSync = new System.Windows.Forms.Button();
            this.panelGame = new System.Windows.Forms.Panel();
            this.btnDeclineDraw = new System.Windows.Forms.Button();
            this.btnAcceptDraw = new System.Windows.Forms.Button();
            this.btnOfferDraw = new System.Windows.Forms.Button();
            this.btnResign = new System.Windows.Forms.Button();
            this.panelLogout = new System.Windows.Forms.Panel();
            this.labelLoginStatus = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.playerColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.chessBoardGamePanel1 = new Tools.Common.ChessBoardGamePanel();
            this.chessBoardPreview = new Tools.Common.ChessBoardVisualizerPanel();
            this.labelGameState = new System.Windows.Forms.Label();
            this.listboxMoves = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPagePlayers.SuspendLayout();
            this.tabPageMatches.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelMatches.SuspendLayout();
            this.panelPlayers.SuspendLayout();
            this.panelRefresh.SuspendLayout();
            this.panelGame.SuspendLayout();
            this.panelLogout.SuspendLayout();
            this.panelLogin.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 658F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(840, 658);
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
            this.tabMain.Size = new System.Drawing.Size(634, 652);
            this.tabMain.TabIndex = 0;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabMain_Selected);
            // 
            // tabPagePlayers
            // 
            this.tabPagePlayers.Controls.Add(this.listViewPlayers);
            this.tabPagePlayers.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlayers.Name = "tabPagePlayers";
            this.tabPagePlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlayers.Size = new System.Drawing.Size(626, 626);
            this.tabPagePlayers.TabIndex = 0;
            this.tabPagePlayers.Text = "Players";
            this.tabPagePlayers.UseVisualStyleBackColor = true;
            // 
            // listViewPlayers
            // 
            this.listViewPlayers.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewPlayers.AutoArrange = false;
            this.listViewPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.playerColumnHeader});
            this.listViewPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPlayers.LargeImageList = this.imageListPlayers;
            this.listViewPlayers.Location = new System.Drawing.Point(3, 3);
            this.listViewPlayers.MultiSelect = false;
            this.listViewPlayers.Name = "listViewPlayers";
            this.listViewPlayers.Size = new System.Drawing.Size(620, 620);
            this.listViewPlayers.SmallImageList = this.imageListPlayers;
            this.listViewPlayers.TabIndex = 0;
            this.listViewPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewPlayers.View = System.Windows.Forms.View.Details;
            // 
            // imageListPlayers
            // 
            this.imageListPlayers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPlayers.ImageStream")));
            this.imageListPlayers.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPlayers.Images.SetKeyName(0, "Brain");
            this.imageListPlayers.Images.SetKeyName(1, "Robot");
            // 
            // tabPageMatches
            // 
            this.tabPageMatches.Controls.Add(this.listViewMatches);
            this.tabPageMatches.Location = new System.Drawing.Point(4, 22);
            this.tabPageMatches.Name = "tabPageMatches";
            this.tabPageMatches.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatches.Size = new System.Drawing.Size(626, 626);
            this.tabPageMatches.TabIndex = 1;
            this.tabPageMatches.Text = "Matches";
            this.tabPageMatches.UseVisualStyleBackColor = true;
            // 
            // listViewMatches
            // 
            this.listViewMatches.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewMatches.AutoArrange = false;
            this.listViewMatches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewMatches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMatchName});
            this.listViewMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewMatches.LargeImageList = this.imageListMatchStatuses;
            this.listViewMatches.Location = new System.Drawing.Point(3, 3);
            this.listViewMatches.MultiSelect = false;
            this.listViewMatches.Name = "listViewMatches";
            this.listViewMatches.Size = new System.Drawing.Size(620, 620);
            this.listViewMatches.SmallImageList = this.imageListMatchStatuses;
            this.listViewMatches.TabIndex = 1;
            this.listViewMatches.UseCompatibleStateImageBehavior = false;
            this.listViewMatches.View = System.Windows.Forms.View.Details;
            this.listViewMatches.ItemActivate += new System.EventHandler(this.listViewMatches_ItemActivate);
            this.listViewMatches.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewMatches_MouseDoubleClick);
            // 
            // columnHeaderMatchName
            // 
            this.columnHeaderMatchName.Text = "Matches";
            this.columnHeaderMatchName.Width = 604;
            // 
            // imageListMatchStatuses
            // 
            this.imageListMatchStatuses.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMatchStatuses.ImageStream")));
            this.imageListMatchStatuses.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMatchStatuses.Images.SetKeyName(0, "BlackWins");
            this.imageListMatchStatuses.Images.SetKeyName(1, "WhiteWins");
            this.imageListMatchStatuses.Images.SetKeyName(2, "InProgress");
            this.imageListMatchStatuses.Images.SetKeyName(3, "Draw");
            // 
            // tabPageGame
            // 
            this.tabPageGame.Controls.Add(this.chessBoardGamePanel1);
            this.tabPageGame.Location = new System.Drawing.Point(4, 22);
            this.tabPageGame.Name = "tabPageGame";
            this.tabPageGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGame.Size = new System.Drawing.Size(626, 626);
            this.tabPageGame.TabIndex = 2;
            this.tabPageGame.Text = "Game";
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.panelMatches);
            this.panelSidebar.Controls.Add(this.panelPlayers);
            this.panelSidebar.Controls.Add(this.panelRefresh);
            this.panelSidebar.Controls.Add(this.panelGame);
            this.panelSidebar.Controls.Add(this.panelLogout);
            this.panelSidebar.Controls.Add(this.panelLogin);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSidebar.Location = new System.Drawing.Point(643, 3);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(194, 652);
            this.panelSidebar.TabIndex = 1;
            // 
            // panelMatches
            // 
            this.panelMatches.Controls.Add(this.labelMatchPreviewStatus);
            this.panelMatches.Controls.Add(this.chessBoardPreview);
            this.panelMatches.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMatches.Location = new System.Drawing.Point(0, 595);
            this.panelMatches.Name = "panelMatches";
            this.panelMatches.Padding = new System.Windows.Forms.Padding(4);
            this.panelMatches.Size = new System.Drawing.Size(194, 215);
            this.panelMatches.TabIndex = 18;
            this.panelMatches.Visible = false;
            // 
            // labelMatchPreviewStatus
            // 
            this.labelMatchPreviewStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMatchPreviewStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMatchPreviewStatus.Location = new System.Drawing.Point(4, 190);
            this.labelMatchPreviewStatus.Name = "labelMatchPreviewStatus";
            this.labelMatchPreviewStatus.Size = new System.Drawing.Size(186, 21);
            this.labelMatchPreviewStatus.TabIndex = 1;
            this.labelMatchPreviewStatus.Text = "STATUS";
            this.labelMatchPreviewStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPlayers
            // 
            this.panelPlayers.Controls.Add(this.btnChallenge);
            this.panelPlayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPlayers.Location = new System.Drawing.Point(0, 550);
            this.panelPlayers.Name = "panelPlayers";
            this.panelPlayers.Padding = new System.Windows.Forms.Padding(4);
            this.panelPlayers.Size = new System.Drawing.Size(194, 45);
            this.panelPlayers.TabIndex = 17;
            this.panelPlayers.Visible = false;
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
            this.btnChallenge.Click += new System.EventHandler(this.btnChallenge_Click);
            // 
            // panelRefresh
            // 
            this.panelRefresh.Controls.Add(this.btnSync);
            this.panelRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRefresh.Location = new System.Drawing.Point(0, 607);
            this.panelRefresh.Name = "panelRefresh";
            this.panelRefresh.Padding = new System.Windows.Forms.Padding(4);
            this.panelRefresh.Size = new System.Drawing.Size(194, 45);
            this.panelRefresh.TabIndex = 16;
            // 
            // btnSync
            // 
            this.btnSync.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSync.Location = new System.Drawing.Point(4, 4);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(186, 36);
            this.btnSync.TabIndex = 1;
            this.btnSync.Text = "Sync all";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelGame
            // 
            this.panelGame.Controls.Add(this.listboxMoves);
            this.panelGame.Controls.Add(this.labelGameState);
            this.panelGame.Controls.Add(this.btnDeclineDraw);
            this.panelGame.Controls.Add(this.btnAcceptDraw);
            this.panelGame.Controls.Add(this.btnOfferDraw);
            this.panelGame.Controls.Add(this.btnResign);
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGame.Location = new System.Drawing.Point(0, 198);
            this.panelGame.Name = "panelGame";
            this.panelGame.Padding = new System.Windows.Forms.Padding(4);
            this.panelGame.Size = new System.Drawing.Size(194, 352);
            this.panelGame.TabIndex = 15;
            this.panelGame.Visible = false;
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
            // playerColumnHeader
            // 
            this.playerColumnHeader.Text = "Players";
            this.playerColumnHeader.Width = 620;
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 1000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // chessBoardGamePanel1
            // 
            this.chessBoardGamePanel1.Bevel = System.Drawing.Color.Brown;
            this.chessBoardGamePanel1.BlackSquare = System.Drawing.Color.SandyBrown;
            this.chessBoardGamePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessBoardGamePanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.chessBoardGamePanel1.Location = new System.Drawing.Point(3, 3);
            this.chessBoardGamePanel1.Name = "chessBoardGamePanel1";
            this.chessBoardGamePanel1.Size = new System.Drawing.Size(620, 620);
            this.chessBoardGamePanel1.TabIndex = 0;
            this.chessBoardGamePanel1.WhiteSquare = System.Drawing.Color.BlanchedAlmond;
            this.chessBoardGamePanel1.OnValidMoveSelected += new Tools.Common.ChessBoardMoveSelectedEventHandler(this.chessBoardGamePanel1_OnValidMoveSelected);
            // 
            // chessBoardPreview
            // 
            this.chessBoardPreview.Bevel = System.Drawing.Color.Brown;
            this.chessBoardPreview.BlackSquare = System.Drawing.Color.SandyBrown;
            this.chessBoardPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.chessBoardPreview.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            this.chessBoardPreview.Location = new System.Drawing.Point(4, 4);
            this.chessBoardPreview.Name = "chessBoardPreview";
            this.chessBoardPreview.Size = new System.Drawing.Size(186, 186);
            this.chessBoardPreview.TabIndex = 0;
            this.chessBoardPreview.WhiteSquare = System.Drawing.Color.BlanchedAlmond;
            // 
            // labelGameState
            // 
            this.labelGameState.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelGameState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameState.Location = new System.Drawing.Point(4, 315);
            this.labelGameState.Name = "labelGameState";
            this.labelGameState.Size = new System.Drawing.Size(186, 33);
            this.labelGameState.TabIndex = 5;
            this.labelGameState.Text = "-";
            this.labelGameState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listboxMoves
            // 
            this.listboxMoves.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxMoves.FormattingEnabled = true;
            this.listboxMoves.Location = new System.Drawing.Point(4, 148);
            this.listboxMoves.Name = "listboxMoves";
            this.listboxMoves.Size = new System.Drawing.Size(186, 167);
            this.listboxMoves.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 658);
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
            this.panelMatches.ResumeLayout(false);
            this.panelPlayers.ResumeLayout(false);
            this.panelRefresh.ResumeLayout(false);
            this.panelGame.ResumeLayout(false);
            this.panelLogout.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPagePlayers;
        private System.Windows.Forms.ListView listViewPlayers;
        private System.Windows.Forms.TabPage tabPageMatches;
        private System.Windows.Forms.TabPage tabPageGame;
        private Tools.Common.ChessBoardGamePanel chessBoardGamePanel1;
        private System.Windows.Forms.ImageList imageListPlayers;
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
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Button btnDeclineDraw;
        private System.Windows.Forms.Button btnAcceptDraw;
        private System.Windows.Forms.Button btnOfferDraw;
        private System.Windows.Forms.Button btnResign;
        private System.Windows.Forms.Panel panelLogout;
        private System.Windows.Forms.Label labelLoginStatus;
        private System.Windows.Forms.ListView listViewMatches;
        private System.Windows.Forms.ColumnHeader columnHeaderMatchName;
        private System.Windows.Forms.Panel panelMatches;
        private Tools.Common.ChessBoardVisualizerPanel chessBoardPreview;
        private System.Windows.Forms.Label labelMatchPreviewStatus;
        private System.Windows.Forms.ImageList imageListMatchStatuses;
        private System.Windows.Forms.ColumnHeader playerColumnHeader;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ListBox listboxMoves;
        private System.Windows.Forms.Label labelGameState;
    }
}

