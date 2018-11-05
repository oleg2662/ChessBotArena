namespace MinimaxBot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.labelLoginStatus = new System.Windows.Forms.Label();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayourSidebar = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonChallenge = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listboxPlayers = new System.Windows.Forms.ListBox();
            this.listboxMatches = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboboxAlgorithms = new System.Windows.Forms.ComboBox();
            this.listboxGameHistory = new System.Windows.Forms.ListBox();
            this.tableLayoutMain.SuspendLayout();
            this.tableLayourSidebar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelStatus.Location = new System.Drawing.Point(0, 554);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(1015, 13);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Messages";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(215, 6);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 36);
            this.buttonLogin.TabIndex = 1;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textboxUsername
            // 
            this.textboxUsername.Location = new System.Drawing.Point(3, 22);
            this.textboxUsername.Name = "textboxUsername";
            this.textboxUsername.Size = new System.Drawing.Size(100, 20);
            this.textboxUsername.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // textboxPassword
            // 
            this.textboxPassword.Location = new System.Drawing.Point(109, 22);
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.PasswordChar = '*';
            this.textboxPassword.Size = new System.Drawing.Size(100, 20);
            this.textboxPassword.TabIndex = 4;
            // 
            // labelLoginStatus
            // 
            this.labelLoginStatus.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelLoginStatus.Location = new System.Drawing.Point(4, 45);
            this.labelLoginStatus.Name = "labelLoginStatus";
            this.labelLoginStatus.Size = new System.Drawing.Size(205, 23);
            this.labelLoginStatus.TabIndex = 6;
            this.labelLoginStatus.Text = "Not logged in";
            this.labelLoginStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 2;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 305F));
            this.tableLayoutMain.Controls.Add(this.tableLayourSidebar, 1, 0);
            this.tableLayoutMain.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 1;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(1015, 554);
            this.tableLayoutMain.TabIndex = 7;
            // 
            // tableLayourSidebar
            // 
            this.tableLayourSidebar.ColumnCount = 1;
            this.tableLayourSidebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayourSidebar.Controls.Add(this.panel1, 0, 0);
            this.tableLayourSidebar.Controls.Add(this.buttonChallenge, 0, 2);
            this.tableLayourSidebar.Controls.Add(this.listboxPlayers, 0, 1);
            this.tableLayourSidebar.Controls.Add(this.listboxMatches, 0, 3);
            this.tableLayourSidebar.Controls.Add(this.button1, 0, 5);
            this.tableLayourSidebar.Controls.Add(this.comboboxAlgorithms, 0, 4);
            this.tableLayourSidebar.Controls.Add(this.listboxGameHistory, 0, 6);
            this.tableLayourSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayourSidebar.Location = new System.Drawing.Point(713, 3);
            this.tableLayourSidebar.Name = "tableLayourSidebar";
            this.tableLayourSidebar.RowCount = 7;
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayourSidebar.Size = new System.Drawing.Size(299, 548);
            this.tableLayourSidebar.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonRefresh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Controls.Add(this.labelLoginStatus);
            this.panel1.Controls.Add(this.textboxUsername);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textboxPassword);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 72);
            this.panel1.TabIndex = 8;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(215, 45);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonChallenge
            // 
            this.buttonChallenge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonChallenge.Location = new System.Drawing.Point(3, 203);
            this.buttonChallenge.Name = "buttonChallenge";
            this.buttonChallenge.Size = new System.Drawing.Size(293, 34);
            this.buttonChallenge.TabIndex = 9;
            this.buttonChallenge.Text = "Challenge";
            this.buttonChallenge.UseVisualStyleBackColor = true;
            this.buttonChallenge.Click += new System.EventHandler(this.buttonChallenge_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(3, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(293, 34);
            this.button1.TabIndex = 11;
            this.button1.Text = "Move";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listboxPlayers
            // 
            this.listboxPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxPlayers.FormattingEnabled = true;
            this.listboxPlayers.Location = new System.Drawing.Point(3, 83);
            this.listboxPlayers.Name = "listboxPlayers";
            this.listboxPlayers.Size = new System.Drawing.Size(293, 114);
            this.listboxPlayers.TabIndex = 12;
            // 
            // listboxMatches
            // 
            this.listboxMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxMatches.FormattingEnabled = true;
            this.listboxMatches.Location = new System.Drawing.Point(3, 243);
            this.listboxMatches.Name = "listboxMatches";
            this.listboxMatches.Size = new System.Drawing.Size(293, 114);
            this.listboxMatches.TabIndex = 13;
            this.listboxMatches.SelectedIndexChanged += new System.EventHandler(this.listboxMatches_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(704, 548);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // comboboxAlgorithms
            // 
            this.comboboxAlgorithms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboboxAlgorithms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxAlgorithms.FormattingEnabled = true;
            this.comboboxAlgorithms.Items.AddRange(new object[] {
            "Minimax algorithm",
            "Alpha-Beta algorithm",
            "Greedy algorithm",
            "Random algorithm"});
            this.comboboxAlgorithms.Location = new System.Drawing.Point(3, 363);
            this.comboboxAlgorithms.Name = "comboboxAlgorithms";
            this.comboboxAlgorithms.Size = new System.Drawing.Size(293, 21);
            this.comboboxAlgorithms.TabIndex = 14;
            // 
            // listboxGameHistory
            // 
            this.listboxGameHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listboxGameHistory.FormattingEnabled = true;
            this.listboxGameHistory.Location = new System.Drawing.Point(3, 429);
            this.listboxGameHistory.Name = "listboxGameHistory";
            this.listboxGameHistory.Size = new System.Drawing.Size(293, 116);
            this.listboxGameHistory.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 567);
            this.Controls.Add(this.tableLayoutMain);
            this.Controls.Add(this.labelStatus);
            this.Name = "MainForm";
            this.Text = "Bot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayourSidebar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxPassword;
        private System.Windows.Forms.Label labelLoginStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.TableLayoutPanel tableLayourSidebar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonChallenge;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listboxPlayers;
        private System.Windows.Forms.ListBox listboxMatches;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboboxAlgorithms;
        private System.Windows.Forms.ListBox listboxGameHistory;
    }
}

