using System;

namespace Game.Chess.Pieces
{
    [Serializable]
    public class Queen : ChessPiece
    {
        public Queen(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Queen, hasMoved)
        {
        }

        public Queen(ChessPlayer owner)
            : this(owner, false)
        {
        }

        public override string ToString()
        {
            return Figure(Owner);
        }

        public static string Figure(ChessPlayer player)
        {
            return player == ChessPlayer.White ? "♕" : "♛";
        }

        public override ChessPiece Clone()
        {
            return new Queen(Owner, HasMoved);
        }
    }
}
