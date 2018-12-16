namespace BoardGame.Clients.BotClient
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
            this.btnCalculate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboboxEvaluators = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboboxAlgorithms = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.textboxBotLog = new System.Windows.Forms.RichTextBox();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.textboxLog = new System.Windows.Forms.RichTextBox();
            this.tabPageReadme = new System.Windows.Forms.TabPage();
            this.textboxReadme = new System.Windows.Forms.RichTextBox();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanelMultiplayer.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelMatches.SuspendLayout();
            this.panelPlayers.SuspendLayout();
            this.panelGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panelLogout.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPagePlayers.SuspendLayout();
            this.tabPageMatches.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabPageReadme.SuspendLayout();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 600);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1082, 22);
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
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1046, 17);
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
            this.tableLayoutPanelMultiplayer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 651F));
            this.tableLayoutPanelMultiplayer.Size = new System.Drawing.Size(1082, 600);
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
            this.panelSidebar.Location = new System.Drawing.Point(885, 3);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(194, 594);
            this.panelSidebar.TabIndex = 1;
            // 
            // panelMatches
            // 
            this.panelMatches.Controls.Add(this.labelMatchPreviewStatus);
            this.panelMatches.Controls.Add(this.chessBoardPreview);
            this.panelMatches.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMatches.Location = new System.Drawing.Point(0, 726);
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
            this.panelPlayers.Location = new System.Drawing.Point(0, 681);
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
            this.panelRefresh.Location = new System.Drawing.Point(0, 549);
            this.panelRefresh.Name = "panelRefresh";
            this.panelRefresh.Padding = new System.Windows.Forms.Padding(4);
            this.panelRefresh.Size = new System.Drawing.Size(194, 45);
            this.panelRefresh.TabIndex = 16;
            // 
            // panelGame
            // 
            this.panelGame.Controls.Add(this.btnCalculate);
            this.panelGame.Controls.Add(this.pictureBox1);
            this.panelGame.Controls.Add(this.numericUpDown1);
            this.panelGame.Controls.Add(this.label3);
            this.panelGame.Controls.Add(this.comboboxEvaluators);
            this.panelGame.Controls.Add(this.label2);
            this.panelGame.Controls.Add(this.comboboxAlgorithms);
            this.panelGame.Controls.Add(this.label1);
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGame.Location = new System.Drawing.Point(0, 198);
            this.panelGame.Name = "panelGame";
            this.panelGame.Padding = new System.Windows.Forms.Padding(4);
            this.panelGame.Size = new System.Drawing.Size(194, 483);
            this.panelGame.TabIndex = 15;
            this.panelGame.Visible = false;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCalculate.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculate.Location = new System.Drawing.Point(4, 123);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Padding = new System.Windows.Forms.Padding(3);
            this.btnCalculate.Size = new System.Drawing.Size(186, 53);
            this.btnCalculate.TabIndex = 18;
            this.btnCalculate.Text = "Start";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 375);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(186, 104);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Top;
            this.numericUpDown1.InterceptArrowKeys = false;
            this.numericUpDown1.Location = new System.Drawing.Point(4, 103);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(186, 20);
            this.numericUpDown1.TabIndex = 13;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(4, 84);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(186, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Max depth";
            // 
            // comboboxEvaluators
            // 
            this.comboboxEvaluators.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboboxEvaluators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxEvaluators.FormattingEnabled = true;
            this.comboboxEvaluators.Location = new System.Drawing.Point(4, 63);
            this.comboboxEvaluators.Name = "comboboxEvaluators";
            this.comboboxEvaluators.Size = new System.Drawing.Size(186, 21);
            this.comboboxEvaluators.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 44);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(186, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "Evaulator";
            // 
            // comboboxAlgorithms
            // 
            this.comboboxAlgorithms.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboboxAlgorithms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxAlgorithms.FormattingEnabled = true;
            this.comboboxAlgorithms.Location = new System.Drawing.Point(4, 23);
            this.comboboxAlgorithms.Name = "comboboxAlgorithms";
            this.comboboxAlgorithms.Size = new System.Drawing.Size(186, 21);
            this.comboboxAlgorithms.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(186, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Algorithm";
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
            this.tabMain.Controls.Add(this.tabPageLog);
            this.tabMain.Controls.Add(this.tabPageReadme);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(3, 3);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(876, 594);
            this.tabMain.TabIndex = 0;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabMain_Selected);
            // 
            // tabPagePlayers
            // 
            this.tabPagePlayers.Controls.Add(this.listViewPlayers);
            this.tabPagePlayers.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlayers.Name = "tabPagePlayers";
            this.tabPagePlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlayers.Size = new System.Drawing.Size(868, 568);
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
            this.listViewPlayers.Size = new System.Drawing.Size(862, 562);
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
            this.tabPageMatches.Size = new System.Drawing.Size(868, 568);
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
            this.listViewMatches.Size = new System.Drawing.Size(862, 562);
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
            this.tabPageGame.Controls.Add(this.textboxBotLog);
            this.tabPageGame.Location = new System.Drawing.Point(4, 22);
            this.tabPageGame.Name = "tabPageGame";
            this.tabPageGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGame.Size = new System.Drawing.Size(868, 568);
            this.tabPageGame.TabIndex = 2;
            this.tabPageGame.Text = "Game";
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // textboxBotLog
            // 
            this.textboxBotLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxBotLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxBotLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxBotLog.Location = new System.Drawing.Point(3, 3);
            this.textboxBotLog.Name = "textboxBotLog";
            this.textboxBotLog.ReadOnly = true;
            this.textboxBotLog.ShowSelectionMargin = true;
            this.textboxBotLog.Size = new System.Drawing.Size(862, 562);
            this.textboxBotLog.TabIndex = 1;
            this.textboxBotLog.Text = "";
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.textboxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(868, 568);
            this.tabPageLog.TabIndex = 4;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // textboxLog
            // 
            this.textboxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxLog.Location = new System.Drawing.Point(3, 3);
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.ReadOnly = true;
            this.textboxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.textboxLog.Size = new System.Drawing.Size(862, 562);
            this.textboxLog.TabIndex = 0;
            this.textboxLog.Text = "";
            // 
            // tabPageReadme
            // 
            this.tabPageReadme.Controls.Add(this.textboxReadme);
            this.tabPageReadme.Location = new System.Drawing.Point(4, 22);
            this.tabPageReadme.Name = "tabPageReadme";
            this.tabPageReadme.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReadme.Size = new System.Drawing.Size(868, 568);
            this.tabPageReadme.TabIndex = 5;
            this.tabPageReadme.Text = "Read Me!";
            this.tabPageReadme.UseVisualStyleBackColor = true;
            // 
            // textboxReadme
            // 
            this.textboxReadme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxReadme.Location = new System.Drawing.Point(3, 3);
            this.textboxReadme.Name = "textboxReadme";
            this.textboxReadme.Size = new System.Drawing.Size(862, 562);
            this.textboxReadme.TabIndex = 0;
            this.textboxReadme.Text = resources.GetString("textboxReadme.Text");
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 15000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 622);
            this.Controls.Add(this.tableLayoutPanelMultiplayer);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Chess Bot Client";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanelMultiplayer.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.panelMatches.ResumeLayout(false);
            this.panelPlayers.ResumeLayout(false);
            this.panelGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panelLogout.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPagePlayers.ResumeLayout(false);
            this.tabPageMatches.ResumeLayout(false);
            this.tabPageGame.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageReadme.ResumeLayout(false);
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
        private Tools.Common.ChessBoardVisualizerPanel chessBoardPreview;
        private System.Windows.Forms.Panel panelPlayers;
        private System.Windows.Forms.Button btnChallenge;
        private System.Windows.Forms.Panel panelRefresh;
        private System.Windows.Forms.Panel panelGame;
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
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.RichTextBox textboxLog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboboxEvaluators;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboboxAlgorithms;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.RichTextBox textboxBotLog;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.TabPage tabPageReadme;
        private System.Windows.Forms.RichTextBox textboxReadme;
    }
}

