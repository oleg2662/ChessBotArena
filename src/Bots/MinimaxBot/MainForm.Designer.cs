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
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonReconnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayourSidebar = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.labelLoginStatus = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textboxBotMessages = new System.Windows.Forms.TextBox();
            this.statusStrip2.SuspendLayout();
            this.tableLayoutMain.SuspendLayout();
            this.tableLayourSidebar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.buttonReconnect});
            this.statusStrip2.Location = new System.Drawing.Point(0, 545);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1015, 22);
            this.statusStrip2.TabIndex = 3;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(954, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Message";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonReconnect
            // 
            this.buttonReconnect.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.buttonReconnect.IsLink = true;
            this.buttonReconnect.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.buttonReconnect.Name = "buttonReconnect";
            this.buttonReconnect.Size = new System.Drawing.Size(46, 17);
            this.buttonReconnect.Text = "Refresh";
            this.buttonReconnect.ToolTipText = "Checks the connection to the service.";
            this.buttonReconnect.Click += new System.EventHandler(this.buttonReconnect_Click);
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 2;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutMain.Controls.Add(this.tableLayourSidebar, 1, 0);
            this.tableLayoutMain.Controls.Add(this.textboxBotMessages, 0, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 1;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(1015, 545);
            this.tableLayoutMain.TabIndex = 8;
            // 
            // tableLayourSidebar
            // 
            this.tableLayourSidebar.ColumnCount = 1;
            this.tableLayourSidebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayourSidebar.Controls.Add(this.panel1, 0, 0);
            this.tableLayourSidebar.Controls.Add(this.pictureBox1, 0, 3);
            this.tableLayourSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayourSidebar.Location = new System.Drawing.Point(838, 3);
            this.tableLayourSidebar.Name = "tableLayourSidebar";
            this.tableLayourSidebar.RowCount = 4;
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayourSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayourSidebar.Size = new System.Drawing.Size(174, 539);
            this.tableLayourSidebar.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonLogout);
            this.panel1.Controls.Add(this.labelLoginStatus);
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Controls.Add(this.textboxPassword);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textboxUsername);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 174);
            this.panel1.TabIndex = 8;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonLogout.Location = new System.Drawing.Point(0, 116);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(168, 36);
            this.buttonLogout.TabIndex = 14;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // labelLoginStatus
            // 
            this.labelLoginStatus.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelLoginStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelLoginStatus.Location = new System.Drawing.Point(0, 151);
            this.labelLoginStatus.Name = "labelLoginStatus";
            this.labelLoginStatus.Size = new System.Drawing.Size(168, 23);
            this.labelLoginStatus.TabIndex = 13;
            this.labelLoginStatus.Text = "Not logged in";
            this.labelLoginStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonLogin.Location = new System.Drawing.Point(0, 80);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(168, 36);
            this.buttonLogin.TabIndex = 12;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textboxPassword
            // 
            this.textboxPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.textboxPassword.Location = new System.Drawing.Point(0, 60);
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.PasswordChar = '*';
            this.textboxPassword.Size = new System.Drawing.Size(168, 20);
            this.textboxPassword.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textboxUsername
            // 
            this.textboxUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.textboxUsername.Location = new System.Drawing.Point(0, 20);
            this.textboxUsername.Name = "textboxUsername";
            this.textboxUsername.Size = new System.Drawing.Size(168, 20);
            this.textboxUsername.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Username";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 411);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(168, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // textboxBotMessages
            // 
            this.textboxBotMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxBotMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxBotMessages.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxBotMessages.Location = new System.Drawing.Point(3, 3);
            this.textboxBotMessages.Multiline = true;
            this.textboxBotMessages.Name = "textboxBotMessages";
            this.textboxBotMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxBotMessages.Size = new System.Drawing.Size(829, 539);
            this.textboxBotMessages.TabIndex = 10;
            this.textboxBotMessages.Text = resources.GetString("textboxBotMessages.Text");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 567);
            this.Controls.Add(this.tableLayoutMain);
            this.Controls.Add(this.statusStrip2);
            this.Name = "MainForm";
            this.Text = "Bot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            this.tableLayourSidebar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripStatusLabel buttonReconnect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.TableLayoutPanel tableLayourSidebar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelLoginStatus;
        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.TextBox textboxBotMessages;
    }
}

