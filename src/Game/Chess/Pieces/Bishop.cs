using System;

namespace Game.Chess.Pieces
{
    [Serializable]
    public class Bishop : ChessPiece
    {
        public Bishop(ChessPlayer owner) : this(owner, false)
        {
        }

        public Bishop(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Bishop, hasMoved)
        {
        }

        public override string ToString()
        {
            return Figure(Owner);
        }

        public static string Figure(ChessPlayer player)
        {
            return player == ChessPlayer.White ? "♗" : "♝";
        }

        public override ChessPiece Clone()
        {
            return new Bishop(Owner, HasMoved);
        }
    }
}
