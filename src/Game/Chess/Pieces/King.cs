using System;

namespace Game.Chess.Pieces
{
    [Serializable]
    public class King : ChessPiece
    {
        public King(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.King, hasMoved)
        {
        }

        public King(ChessPlayer owner) 
            : this(owner, false)
        {
        }

        public override string ToString()
        {
            return Figure(Owner);
        }

        public static string Figure(ChessPlayer player)
        {
            return player == ChessPlayer.White ? "♔ " : "♚";
        }

        public override ChessPiece Clone()
        {
            return new King(Owner, HasMoved);
        }
    }
}
