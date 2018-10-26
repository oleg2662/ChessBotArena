using Game.Chess;
using System;

namespace BoardGame.Service.Models.Data.Moves
{
    [Serializable]
    public class PawnEnPassantMove : DbChessMove
    {
        public Position CapturePosition => new Position(CapturePositionColumn[0], CapturePositionRow);

        public int CapturePositionRow { get; set; }

        public string CapturePositionColumn { get; set; }
    }
}
