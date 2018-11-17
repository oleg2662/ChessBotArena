using System;
using System.Windows.Forms;
using BoardGame.Game.Chess.Pieces;

namespace BoardGame.Tools.Common
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
