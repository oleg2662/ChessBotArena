namespace AlgorithmOfflineTester
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
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupboxAlgorithmsRight = new System.Windows.Forms.GroupBox();
            this.labelBotActiveRight = new System.Windows.Forms.Label();
            this.progressbarBotActiveRight = new System.Windows.Forms.ProgressBar();
            this.labelAlgorithmProgress = new System.Windows.Forms.Label();
            this.progressbarAlgorithmRight = new System.Windows.Forms.ProgressBar();
            this.numericMaxDepthRight = new System.Windows.Forms.NumericUpDown();
            this.labelMaxDepthRight = new System.Windows.Forms.Label();
            this.listboxAlgorithmsRight = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.progressbarBotActiveLeft = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.progressbarAlgorithmLeft = new System.Windows.Forms.ProgressBar();
            this.numericMaxDepthLeft = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.listboxAlgorithmsLeft = new System.Windows.Forms.ListBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageChessboard = new System.Windows.Forms.TabPage();
            this.chessBoardVisualizerPanel1 = new Tools.Common.ChessBoardVisualizerPanel();
            this.tabPageLogs = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelGameStatus = new System.Windows.Forms.Label();
            this.tableLayoutMain.SuspendLayout();
            this.groupboxAlgorithmsRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxDepthRight)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxDepthLeft)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageChessboard.SuspendLayout();
            this.tabPageLogs.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 3;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutMain.Controls.Add(this.groupboxAlgorithmsRight, 2, 0);
            this.tableLayoutMain.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutMain.Controls.Add(this.btnStartStop, 1, 1);
            this.tableLayoutMain.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutMain.Controls.Add(this.btnRestart, 0, 1);
            this.tableLayoutMain.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutMain.Size = new System.Drawing.Size(865, 582);
            this.tableLayoutMain.TabIndex = 8;
            // 
            // groupboxAlgorithmsRight
            // 
            this.groupboxAlgorithmsRight.Controls.Add(this.labelBotActiveRight);
            this.groupboxAlgorithmsRight.Controls.Add(this.progressbarBotActiveRight);
            this.groupboxAlgorithmsRight.Controls.Add(this.labelAlgorithmProgress);
            this.groupboxAlgorithmsRight.Controls.Add(this.progressbarAlgorithmRight);
            this.groupboxAlgorithmsRight.Controls.Add(this.numericMaxDepthRight);
            this.groupboxAlgorithmsRight.Controls.Add(this.labelMaxDepthRight);
            this.groupboxAlgorithmsRight.Controls.Add(this.listboxAlgorithmsRight);
            this.groupboxAlgorithmsRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupboxAlgorithmsRight.Location = new System.Drawing.Point(688, 3);
            this.groupboxAlgorithmsRight.Name = "groupboxAlgorithmsRight";
            this.groupboxAlgorithmsRight.Size = new System.Drawing.Size(174, 516);
            this.groupboxAlgorithmsRight.TabIndex = 11;
            this.groupboxAlgorithmsRight.TabStop = false;
            this.groupboxAlgorithmsRight.Text = "Algorithm Settings";
            // 
            // labelBotActiveRight
            // 
            this.labelBotActiveRight.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelBotActiveRight.Location = new System.Drawing.Point(3, 443);
            this.labelBotActiveRight.Name = "labelBotActiveRight";
            this.labelBotActiveRight.Size = new System.Drawing.Size(168, 20);
            this.labelBotActiveRight.TabIndex = 16;
            this.labelBotActiveRight.Text = "Bot active...";
            this.labelBotActiveRight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // progressbarBotActiveRight
            // 
            this.progressbarBotActiveRight.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressbarBotActiveRight.ForeColor = System.Drawing.Color.IndianRed;
            this.progressbarBotActiveRight.Location = new System.Drawing.Point(3, 463);
            this.progressbarBotActiveRight.MarqueeAnimationSpeed = 0;
            this.progressbarBotActiveRight.Name = "progressbarBotActiveRight";
            this.progressbarBotActiveRight.Size = new System.Drawing.Size(168, 15);
            this.progressbarBotActiveRight.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressbarBotActiveRight.TabIndex = 15;
            // 
            // labelAlgorithmProgress
            // 
            this.labelAlgorithmProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelAlgorithmProgress.Location = new System.Drawing.Point(3, 478);
            this.labelAlgorithmProgress.Name = "labelAlgorithmProgress";
            this.labelAlgorithmProgress.Size = new System.Drawing.Size(168, 20);
            this.labelAlgorithmProgress.TabIndex = 14;
            this.labelAlgorithmProgress.Text = "Algorithm running...";
            this.labelAlgorithmProgress.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // progressbarAlgorithmRight
            // 
            this.progressbarAlgorithmRight.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressbarAlgorithmRight.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.progressbarAlgorithmRight.Location = new System.Drawing.Point(3, 498);
            this.progressbarAlgorithmRight.MarqueeAnimationSpeed = 0;
            this.progressbarAlgorithmRight.Name = "progressbarAlgorithmRight";
            this.progressbarAlgorithmRight.Size = new System.Drawing.Size(168, 15);
            this.progressbarAlgorithmRight.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressbarAlgorithmRight.TabIndex = 13;
            // 
            // numericMaxDepthRight
            // 
            this.numericMaxDepthRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.numericMaxDepthRight.Location = new System.Drawing.Point(3, 131);
            this.numericMaxDepthRight.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericMaxDepthRight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxDepthRight.Name = "numericMaxDepthRight";
            this.numericMaxDepthRight.Size = new System.Drawing.Size(168, 20);
            this.numericMaxDepthRight.TabIndex = 12;
            this.numericMaxDepthRight.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // labelMaxDepthRight
            // 
            this.labelMaxDepthRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelMaxDepthRight.Location = new System.Drawing.Point(3, 111);
            this.labelMaxDepthRight.Name = "labelMaxDepthRight";
            this.labelMaxDepthRight.Size = new System.Drawing.Size(168, 20);
            this.labelMaxDepthRight.TabIndex = 11;
            this.labelMaxDepthRight.Text = "Max depth (where applicable)";
            this.labelMaxDepthRight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // listboxAlgorithmsRight
            // 
            this.listboxAlgorithmsRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.listboxAlgorithmsRight.FormattingEnabled = true;
            this.listboxAlgorithmsRight.Location = new System.Drawing.Point(3, 16);
            this.listboxAlgorithmsRight.Name = "listboxAlgorithmsRight";
            this.listboxAlgorithmsRight.Size = new System.Drawing.Size(168, 95);
            this.listboxAlgorithmsRight.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.progressbarBotActiveLeft);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.progressbarAlgorithmLeft);
            this.groupBox1.Controls.Add(this.numericMaxDepthLeft);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.listboxAlgorithmsLeft);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 516);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algorithm Settings";
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(3, 443);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Bot active...";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // progressbarBotActiveLeft
            // 
            this.progressbarBotActiveLeft.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressbarBotActiveLeft.ForeColor = System.Drawing.Color.IndianRed;
            this.progressbarBotActiveLeft.Location = new System.Drawing.Point(3, 463);
            this.progressbarBotActiveLeft.MarqueeAnimationSpeed = 0;
            this.progressbarBotActiveLeft.Name = "progressbarBotActiveLeft";
            this.progressbarBotActiveLeft.Size = new System.Drawing.Size(168, 15);
            this.progressbarBotActiveLeft.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressbarBotActiveLeft.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 478);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Algorithm running...";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // progressbarAlgorithmLeft
            // 
            this.progressbarAlgorithmLeft.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressbarAlgorithmLeft.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.progressbarAlgorithmLeft.Location = new System.Drawing.Point(3, 498);
            this.progressbarAlgorithmLeft.MarqueeAnimationSpeed = 0;
            this.progressbarAlgorithmLeft.Name = "progressbarAlgorithmLeft";
            this.progressbarAlgorithmLeft.Size = new System.Drawing.Size(168, 15);
            this.progressbarAlgorithmLeft.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressbarAlgorithmLeft.TabIndex = 13;
            // 
            // numericMaxDepthLeft
            // 
            this.numericMaxDepthLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.numericMaxDepthLeft.Location = new System.Drawing.Point(3, 131);
            this.numericMaxDepthLeft.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericMaxDepthLeft.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxDepthLeft.Name = "numericMaxDepthLeft";
            this.numericMaxDepthLeft.Size = new System.Drawing.Size(168, 20);
            this.numericMaxDepthLeft.TabIndex = 12;
            this.numericMaxDepthLeft.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(168, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Max depth (where applicable)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // listboxAlgorithmsLeft
            // 
            this.listboxAlgorithmsLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.listboxAlgorithmsLeft.FormattingEnabled = true;
            this.listboxAlgorithmsLeft.Location = new System.Drawing.Point(3, 16);
            this.listboxAlgorithmsLeft.Name = "listboxAlgorithmsLeft";
            this.listboxAlgorithmsLeft.Size = new System.Drawing.Size(168, 95);
            this.listboxAlgorithmsLeft.TabIndex = 0;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartStop.Location = new System.Drawing.Point(183, 525);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(499, 54);
            this.btnStartStop.TabIndex = 12;
            this.btnStartStop.Text = "Start / Stop";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageChessboard);
            this.tabControl1.Controls.Add(this.tabPageLogs);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(183, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(499, 516);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPageChessboard
            // 
            this.tabPageChessboard.Controls.Add(this.chessBoardVisualizerPanel1);
            this.tabPageChessboard.Location = new System.Drawing.Point(4, 22);
            this.tabPageChessboard.Name = "tabPageChessboard";
            this.tabPageChessboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChessboard.Size = new System.Drawing.Size(491, 490);
            this.tabPageChessboard.TabIndex = 0;
            this.tabPageChessboard.Text = "Board";
            this.tabPageChessboard.UseVisualStyleBackColor = true;
            // 
            // chessBoardVisualizerPanel1
            // 
            this.chessBoardVisualizerPanel1.Bevel = System.Drawing.Color.Brown;
            this.chessBoardVisualizerPanel1.BlackSquare = System.Drawing.Color.SandyBrown;
            this.chessBoardVisualizerPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessBoardVisualizerPanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.chessBoardVisualizerPanel1.Location = new System.Drawing.Point(3, 3);
            this.chessBoardVisualizerPanel1.Name = "chessBoardVisualizerPanel1";
            this.chessBoardVisualizerPanel1.Size = new System.Drawing.Size(485, 484);
            this.chessBoardVisualizerPanel1.TabIndex = 0;
            this.chessBoardVisualizerPanel1.WhiteSquare = System.Drawing.Color.BlanchedAlmond;
            // 
            // tabPageLogs
            // 
            this.tabPageLogs.Controls.Add(this.textBox1);
            this.tabPageLogs.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogs.Name = "tabPageLogs";
            this.tabPageLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogs.Size = new System.Drawing.Size(491, 490);
            this.tabPageLogs.TabIndex = 1;
            this.tabPageLogs.Text = "Log";
            this.tabPageLogs.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(485, 484);
            this.textBox1.TabIndex = 0;
            // 
            // btnRestart
            // 
            this.btnRestart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRestart.Location = new System.Drawing.Point(3, 525);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(174, 54);
            this.btnRestart.TabIndex = 14;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelGameStatus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(688, 525);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 54);
            this.panel1.TabIndex = 15;
            // 
            // labelGameStatus
            // 
            this.labelGameStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGameStatus.Location = new System.Drawing.Point(0, 0);
            this.labelGameStatus.Name = "labelGameStatus";
            this.labelGameStatus.Size = new System.Drawing.Size(174, 54);
            this.labelGameStatus.TabIndex = 0;
            this.labelGameStatus.Text = "Game Status";
            this.labelGameStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 582);
            this.Controls.Add(this.tableLayoutMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(881, 621);
            this.MinimumSize = new System.Drawing.Size(881, 621);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Offline Bot Tester";
            this.tableLayoutMain.ResumeLayout(false);
            this.groupboxAlgorithmsRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxDepthRight)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxDepthLeft)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageChessboard.ResumeLayout(false);
            this.tabPageLogs.ResumeLayout(false);
            this.tabPageLogs.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.GroupBox groupboxAlgorithmsRight;
        private System.Windows.Forms.ListBox listboxAlgorithmsRight;
        private System.Windows.Forms.Label labelAlgorithmProgress;
        private System.Windows.Forms.ProgressBar progressbarAlgorithmRight;
        private System.Windows.Forms.NumericUpDown numericMaxDepthRight;
        private System.Windows.Forms.Label labelMaxDepthRight;
        private System.Windows.Forms.Label labelBotActiveRight;
        private System.Windows.Forms.ProgressBar progressbarBotActiveRight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressbarBotActiveLeft;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressbarAlgorithmLeft;
        private System.Windows.Forms.NumericUpDown numericMaxDepthLeft;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listboxAlgorithmsLeft;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageChessboard;
        private System.Windows.Forms.TabPage tabPageLogs;
        private System.Windows.Forms.Button btnRestart;
        private Tools.Common.ChessBoardVisualizerPanel chessBoardVisualizerPanel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelGameStatus;
    }
}

