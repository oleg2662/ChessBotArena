using Game.Chess.Pieces;
using System;

namespace BoardGame.Service.Models.Data.Moves
{
    [Serializable]
    public class DbPawnPromotionalMove : DbChessMove
    {
        public PieceKind PromoteTo { get; set; }
    }
}
