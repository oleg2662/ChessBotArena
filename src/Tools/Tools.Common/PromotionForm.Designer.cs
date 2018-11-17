namespace BoardGame.Tools.Common
{
    partial class PromotionForm
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
            this.btnBishop = new System.Windows.Forms.Button();
            this.btnRook = new System.Windows.Forms.Button();
            this.btnKnight = new System.Windows.Forms.Button();
            this.btnQueen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBishop
            // 
            this.btnBishop.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBishop.Location = new System.Drawing.Point(12, 12);
            this.btnBishop.Name = "btnBishop";
            this.btnBishop.Size = new System.Drawing.Size(120, 120);
            this.btnBishop.TabIndex = 0;
            this.btnBishop.Text = "♗";
            this.btnBishop.UseVisualStyleBackColor = true;
            this.btnBishop.Click += new System.EventHandler(this.btnBishop_Click);
            // 
            // btnRook
            // 
            this.btnRook.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRook.Location = new System.Drawing.Point(138, 10);
            this.btnRook.Name = "btnRook";
            this.btnRook.Size = new System.Drawing.Size(120, 120);
            this.btnRook.TabIndex = 1;
            this.btnRook.Text = "♖";
            this.btnRook.UseVisualStyleBackColor = true;
            this.btnRook.Click += new System.EventHandler(this.btnRook_Click);
            // 
            // btnKnight
            // 
            this.btnKnight.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKnight.Location = new System.Drawing.Point(264, 10);
            this.btnKnight.Name = "btnKnight";
            this.btnKnight.Size = new System.Drawing.Size(120, 120);
            this.btnKnight.TabIndex = 2;
            this.btnKnight.Text = "♘";
            this.btnKnight.UseVisualStyleBackColor = true;
            this.btnKnight.Click += new System.EventHandler(this.btnKnight_Click);
            // 
            // btnQueen
            // 
            this.btnQueen.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQueen.Location = new System.Drawing.Point(390, 10);
            this.btnQueen.Name = "btnQueen";
            this.btnQueen.Size = new System.Drawing.Size(120, 120);
            this.btnQueen.TabIndex = 3;
            this.btnQueen.Text = "♕";
            this.btnQueen.UseVisualStyleBackColor = true;
            this.btnQueen.Click += new System.EventHandler(this.btnQueen_Click);
            // 
            // PromotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 142);
            this.ControlBox = false;
            this.Controls.Add(this.btnQueen);
            this.Controls.Add(this.btnKnight);
            this.Controls.Add(this.btnRook);
            this.Controls.Add(this.btnBishop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromotionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Promote to...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBishop;
        private System.Windows.Forms.Button btnRook;
        private System.Windows.Forms.Button btnKnight;
        private System.Windows.Forms.Button btnQueen;
    }
}