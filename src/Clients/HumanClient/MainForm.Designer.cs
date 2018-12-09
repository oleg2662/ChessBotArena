using BoardGame.Tools.Common;

namespace BoardGame.HumanClient
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
            this.imageListPlayers = new System.Windows.Forms.ImageList(this.components);
            this.imageListMatchStatuses = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.tableLayoutPanelMultiplayer = new System.Windows.Forms.TableLayoutPanel();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelMatches = new System.Windows.Forms.Panel();
            this.labelMatchPreviewStatus = new System.Windows.Forms.Label();
            this.chessBoardPreview = new BoardGame.Tools.Common.ChessBoardVisualizerPanel();
            this.panelPlayers = new System.Windows.Forms.Panel();
            this.btnChallenge = new System.Windows.Forms.Button();
            this.panelRefresh = new System.Windows.Forms.Panel();
            this.panelGame = new System.Windows.Forms.Panel();
            this.listboxMoves = new System.Windows.Forms.ListBox();
            this.labelGameState = new System.Windows.Forms.Label();
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPagePlayers = new System.Windows.Forms.TabPage();
            this.listViewPlayers = new System.Windows.Forms.ListView();
            this.playerColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageMatches = new System.Windows.Forms.TabPage();
            this.listViewMatches = new System.Windows.Forms.ListView();
            this.columnHeaderMatchName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageGame = new System.Windows.Forms.TabPage();
            this.chessBoardGamePanel1 = new BoardGame.Tools.Common.ChessBoardGamePanel();
            this.tabLadder = new System.Windows.Forms.TabPage();
            this.listviewLadder = new System.Windows.Forms.ListView();
            this.columnHeaderPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPoints = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkboxShowBots = new System.Windows.Forms.CheckBox();
            this.checkboxShowHumans = new System.Windows.Forms.CheckBox();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.textboxLog = new System.Windows.Forms.RichTextBox();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanelMultiplayer.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelMatches.SuspendLayout();
            this.panelPlayers.SuspendLayout();
            this.panelGame.SuspendLayout();
            this.panelLogout.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPagePlayers.SuspendLayout();
            this.tabPageMatches.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.tabLadder.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListPlayers
            // 
            this.imageListPlayers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPlayers.ImageStream")));
            this.imageListPlayers.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPlayers.Images.SetKeyName(0, "Brain");
            this.imageListPlayers.Images.SetKeyName(1, "Robot");
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 662);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(925, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.MarqueeAnimationSpeed = 1;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(889, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownButtonWidth = 0;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(21, 20);
            this.toolStripSplitButton1.Text = "Refresh";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // tableLayoutPanelMultiplayer
            // 
            this.tableLayoutPanelMultiplayer.ColumnCount = 2;
            this.tableLayoutPanelMultiplayer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMultiplayer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelMultiplayer.Controls.Add(this.panelSidebar, 1, 0);
            this.tableLayoutPanelMultiplayer.Controls.Add(this.tabMain, 0, 0);
            this.tableLayoutPanelMultiplayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMultiplayer.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMultiplayer.Name = "tableLayoutPanelMultiplayer";
            this.tableLayoutPanelMultiplayer.RowCount = 1;
            this.tableLayoutPanelMultiplayer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMultiplayer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 831F));
            this.tableLayoutPanelMultiplayer.Size = new System.Drawing.Size(925, 662);
            this.tableLayoutPanelMultiplayer.TabIndex = 5;
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
            this.panelSidebar.Location = new System.Drawing.Point(728, 3);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(194, 656);
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
            // chessBoardPreview
            // 
            this.chessBoardPreview.Bevel = System.Drawing.Color.Brown;
            this.chessBoardPreview.BlackSquare = System.Drawing.Color.SandyBrown;
            this.chessBoardPreview.ChessRepresentation = ((BoardGame.Game.Chess.ChessRepresentation)(resources.GetObject("chessBoardPreview.ChessRepresentation")));
            this.chessBoardPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.chessBoardPreview.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            this.chessBoardPreview.Location = new System.Drawing.Point(4, 4);
            this.chessBoardPreview.Name = "chessBoardPreview";
            this.chessBoardPreview.Size = new System.Drawing.Size(186, 186);
            this.chessBoardPreview.TabIndex = 0;
            this.chessBoardPreview.WhiteSquare = System.Drawing.Color.BlanchedAlmond;
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
            this.panelRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRefresh.Location = new System.Drawing.Point(0, 611);
            this.panelRefresh.Name = "panelRefresh";
            this.panelRefresh.Padding = new System.Windows.Forms.Padding(4);
            this.panelRefresh.Size = new System.Drawing.Size(194, 45);
            this.panelRefresh.TabIndex = 16;
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
            // listboxMoves
            // 
            this.listboxMoves.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxMoves.FormattingEnabled = true;
            this.listboxMoves.Location = new System.Drawing.Point(4, 148);
            this.listboxMoves.Name = "listboxMoves";
            this.listboxMoves.Size = new System.Drawing.Size(186, 167);
            this.listboxMoves.TabIndex = 6;
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
            // btnDeclineDraw
            // 
            this.btnDeclineDraw.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDeclineDraw.Location = new System.Drawing.Point(4, 112);
            this.btnDeclineDraw.Name = "btnDeclineDraw";
            this.btnDeclineDraw.Size = new System.Drawing.Size(186, 36);
            this.btnDeclineDraw.TabIndex = 3;
            this.btnDeclineDraw.Text = "Decline draw";
            this.btnDeclineDraw.UseVisualStyleBackColor = true;
            this.btnDeclineDraw.Click += new System.EventHandler(this.btnDeclineDraw_Click);
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
            this.btnAcceptDraw.Click += new System.EventHandler(this.btnAcceptDraw_Click);
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
            this.btnOfferDraw.Click += new System.EventHandler(this.btnOfferDraw_Click);
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
            this.btnResign.Click += new System.EventHandler(this.btnResign_Click);
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
            this.textboxPassword.Text = "testUser1@testUser1.com";
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
            this.textboxUsername.Text = "testUser1@testUser1.com";
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
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPagePlayers);
            this.tabMain.Controls.Add(this.tabPageMatches);
            this.tabMain.Controls.Add(this.tabPageGame);
            this.tabMain.Controls.Add(this.tabLadder);
            this.tabMain.Controls.Add(this.tabPageLog);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(3, 3);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(719, 656);
            this.tabMain.TabIndex = 0;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabMain_Selected);
            // 
            // tabPagePlayers
            // 
            this.tabPagePlayers.Controls.Add(this.listViewPlayers);
            this.tabPagePlayers.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlayers.Name = "tabPagePlayers";
            this.tabPagePlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlayers.Size = new System.Drawing.Size(711, 630);
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
            this.listViewPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPlayers.HideSelection = false;
            this.listViewPlayers.LargeImageList = this.imageListPlayers;
            this.listViewPlayers.Location = new System.Drawing.Point(3, 3);
            this.listViewPlayers.MultiSelect = false;
            this.listViewPlayers.Name = "listViewPlayers";
            this.listViewPlayers.Size = new System.Drawing.Size(705, 624);
            this.listViewPlayers.SmallImageList = this.imageListPlayers;
            this.listViewPlayers.TabIndex = 0;
            this.listViewPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewPlayers.View = System.Windows.Forms.View.Details;
            // 
            // playerColumnHeader
            // 
            this.playerColumnHeader.Text = "Players";
            this.playerColumnHeader.Width = 620;
            // 
            // tabPageMatches
            // 
            this.tabPageMatches.Controls.Add(this.listViewMatches);
            this.tabPageMatches.Location = new System.Drawing.Point(4, 22);
            this.tabPageMatches.Name = "tabPageMatches";
            this.tabPageMatches.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatches.Size = new System.Drawing.Size(711, 630);
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
            this.listViewMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewMatches.HideSelection = false;
            this.listViewMatches.LargeImageList = this.imageListMatchStatuses;
            this.listViewMatches.Location = new System.Drawing.Point(3, 3);
            this.listViewMatches.MultiSelect = false;
            this.listViewMatches.Name = "listViewMatches";
            this.listViewMatches.Size = new System.Drawing.Size(705, 624);
            this.listViewMatches.SmallImageList = this.imageListMatchStatuses;
            this.listViewMatches.TabIndex = 1;
            this.listViewMatches.UseCompatibleStateImageBehavior = false;
            this.listViewMatches.View = System.Windows.Forms.View.Details;
            this.listViewMatches.ItemActivate += new System.EventHandler(this.listViewMatches_ItemActivate);
            // 
            // columnHeaderMatchName
            // 
            this.columnHeaderMatchName.Text = "Matches";
            this.columnHeaderMatchName.Width = 604;
            // 
            // tabPageGame
            // 
            this.tabPageGame.Controls.Add(this.chessBoardGamePanel1);
            this.tabPageGame.Location = new System.Drawing.Point(4, 22);
            this.tabPageGame.Name = "tabPageGame";
            this.tabPageGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGame.Size = new System.Drawing.Size(711, 630);
            this.tabPageGame.TabIndex = 2;
            this.tabPageGame.Text = "Game";
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // chessBoardGamePanel1
            // 
            this.chessBoardGamePanel1.Bevel = System.Drawing.Color.Brown;
            this.chessBoardGamePanel1.BlackSquare = System.Drawing.Color.SandyBrown;
            this.chessBoardGamePanel1.ChessRepresentation = ((BoardGame.Game.Chess.ChessRepresentation)(resources.GetObject("chessBoardGamePanel1.ChessRepresentation")));
            this.chessBoardGamePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessBoardGamePanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.chessBoardGamePanel1.Location = new System.Drawing.Point(3, 3);
            this.chessBoardGamePanel1.Name = "chessBoardGamePanel1";
            this.chessBoardGamePanel1.Size = new System.Drawing.Size(705, 624);
            this.chessBoardGamePanel1.TabIndex = 0;
            this.chessBoardGamePanel1.WhiteSquare = System.Drawing.Color.BlanchedAlmond;
            this.chessBoardGamePanel1.OnValidMoveSelected += new BoardGame.Tools.Common.ChessBoardMoveSelectedEventHandler(this.chessBoardGamePanel1_OnValidMoveSelected);
            // 
            // tabLadder
            // 
            this.tabLadder.Controls.Add(this.listviewLadder);
            this.tabLadder.Controls.Add(this.panel1);
            this.tabLadder.Location = new System.Drawing.Point(4, 22);
            this.tabLadder.Name = "tabLadder";
            this.tabLadder.Padding = new System.Windows.Forms.Padding(3);
            this.tabLadder.Size = new System.Drawing.Size(711, 630);
            this.tabLadder.TabIndex = 3;
            this.tabLadder.Text = "Ladder";
            this.tabLadder.UseVisualStyleBackColor = true;
            this.tabLadder.Enter += new System.EventHandler(this.tabLadder_Enter);
            // 
            // listviewLadder
            // 
            this.listviewLadder.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listviewLadder.AutoArrange = false;
            this.listviewLadder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listviewLadder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPosition,
            this.columnHeaderName,
            this.columnHeaderPoints});
            this.listviewLadder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listviewLadder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewLadder.LargeImageList = this.imageListPlayers;
            this.listviewLadder.Location = new System.Drawing.Point(3, 42);
            this.listviewLadder.MultiSelect = false;
            this.listviewLadder.Name = "listviewLadder";
            this.listviewLadder.Size = new System.Drawing.Size(705, 585);
            this.listviewLadder.SmallImageList = this.imageListPlayers;
            this.listviewLadder.TabIndex = 3;
            this.listviewLadder.UseCompatibleStateImageBehavior = false;
            this.listviewLadder.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderPosition
            // 
            this.columnHeaderPosition.Text = "#";
            this.columnHeaderPosition.Width = 80;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 390;
            // 
            // columnHeaderPoints
            // 
            this.columnHeaderPoints.Text = "Avg/Ply";
            this.columnHeaderPoints.Width = 120;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkboxShowBots);
            this.panel1.Controls.Add(this.checkboxShowHumans);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 39);
            this.panel1.TabIndex = 2;
            // 
            // checkboxShowBots
            // 
            this.checkboxShowBots.AutoSize = true;
            this.checkboxShowBots.Checked = true;
            this.checkboxShowBots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxShowBots.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkboxShowBots.Location = new System.Drawing.Point(142, 0);
            this.checkboxShowBots.Name = "checkboxShowBots";
            this.checkboxShowBots.Padding = new System.Windows.Forms.Padding(5);
            this.checkboxShowBots.Size = new System.Drawing.Size(94, 39);
            this.checkboxShowBots.TabIndex = 1;
            this.checkboxShowBots.Text = "Include bots";
            this.checkboxShowBots.UseVisualStyleBackColor = true;
            this.checkboxShowBots.CheckedChanged += new System.EventHandler(this.checkboxShowBots_CheckedChanged);
            // 
            // checkboxShowHumans
            // 
            this.checkboxShowHumans.AutoSize = true;
            this.checkboxShowHumans.Checked = true;
            this.checkboxShowHumans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxShowHumans.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkboxShowHumans.Location = new System.Drawing.Point(0, 0);
            this.checkboxShowHumans.Name = "checkboxShowHumans";
            this.checkboxShowHumans.Padding = new System.Windows.Forms.Padding(5);
            this.checkboxShowHumans.Size = new System.Drawing.Size(142, 39);
            this.checkboxShowHumans.TabIndex = 0;
            this.checkboxShowHumans.Text = "Include human players";
            this.checkboxShowHumans.UseVisualStyleBackColor = true;
            this.checkboxShowHumans.CheckedChanged += new System.EventHandler(this.checkboxShowHumans_CheckedChanged);
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.textboxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(711, 630);
            this.tabPageLog.TabIndex = 4;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // textboxLog
            // 
            this.textboxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxLog.Location = new System.Drawing.Point(3, 3);
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.ReadOnly = true;
            this.textboxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.textboxLog.Size = new System.Drawing.Size(705, 624);
            this.textboxLog.TabIndex = 0;
            this.textboxLog.Text = "";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Interval = 15000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 684);
            this.Controls.Add(this.tableLayoutPanelMultiplayer);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Chess Client";
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanelMultiplayer.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.panelMatches.ResumeLayout(false);
            this.panelPlayers.ResumeLayout(false);
            this.panelGame.ResumeLayout(false);
            this.panelLogout.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPagePlayers.ResumeLayout(false);
            this.tabPageMatches.ResumeLayout(false);
            this.tabPageGame.ResumeLayout(false);
            this.tabLadder.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageListPlayers;
        private System.Windows.Forms.ImageList imageListMatchStatuses;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMultiplayer;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelMatches;
        private System.Windows.Forms.Label labelMatchPreviewStatus;
        private ChessBoardVisualizerPanel chessBoardPreview;
        private System.Windows.Forms.Panel panelPlayers;
        private System.Windows.Forms.Button btnChallenge;
        private System.Windows.Forms.Panel panelRefresh;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.ListBox listboxMoves;
        private System.Windows.Forms.Label labelGameState;
        private System.Windows.Forms.Button btnDeclineDraw;
        private System.Windows.Forms.Button btnAcceptDraw;
        private System.Windows.Forms.Button btnOfferDraw;
        private System.Windows.Forms.Button btnResign;
        private System.Windows.Forms.Panel panelLogout;
        private System.Windows.Forms.Label labelLoginStatus;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox textboxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPagePlayers;
        private System.Windows.Forms.ListView listViewPlayers;
        private System.Windows.Forms.ColumnHeader playerColumnHeader;
        private System.Windows.Forms.TabPage tabPageMatches;
        private System.Windows.Forms.ListView listViewMatches;
        private System.Windows.Forms.ColumnHeader columnHeaderMatchName;
        private System.Windows.Forms.TabPage tabPageGame;
        private ChessBoardGamePanel chessBoardGamePanel1;
        private System.Windows.Forms.TabPage tabLadder;
        private System.Windows.Forms.ListView listviewLadder;
        private System.Windows.Forms.ColumnHeader columnHeaderPosition;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderPoints;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkboxShowBots;
        private System.Windows.Forms.CheckBox checkboxShowHumans;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.RichTextBox textboxLog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.Timer timerRefresh;
    }
}

