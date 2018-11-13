using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Chess.Pieces;

namespace Tools.Common
{
    public partial class PromotionForm : Form
    {
        public PromotionForm()
        {
            InitializeComponent();
        }

        private void btnBishop_Click(object sender, EventArgs e)
        {
            SelectedPieceKind = PieceKind.Bishop;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnRook_Click(object sender, EventArgs e)
        {
            SelectedPieceKind = PieceKind.Rook;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnKnight_Click(object sender, EventArgs e)
        {
            SelectedPieceKind = PieceKind.Knight;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnQueen_Click(object sender, EventArgs e)
        {
            SelectedPieceKind = PieceKind.Queen;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public PieceKind SelectedPieceKind { get; private set; }
    }
}
