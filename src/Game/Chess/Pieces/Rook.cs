using System;

namespace Game.Chess.Pieces
{
    [Serializable]
    public class Rook : ChessPiece
    {
        public Rook(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Rook, hasMoved)
        {
        }

        public Rook(ChessPlayer owner)
            : this(owner, false)
        {
        }

        public override string ToString()
        {
            return Figure(Owner);
        }

        public static string Figure(ChessPlayer player)
        {
            return player == ChessPlayer.White ? "♖" : "♜";
        }

        public override ChessPiece Clone()
        {
            return new Rook(Owner, HasMoved);
        }
    }
}
