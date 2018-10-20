using System;

namespace Game.Chess.Pieces
{
    [Serializable]
    public class Knight : ChessPiece
    {
        public Knight(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Knight, hasMoved)
        {
        }

        public Knight(ChessPlayer owner)
            : this(owner, false)
        {
        }

        public override string ToString()
        {
            return Figure(Owner);
        }

        public static string Figure(ChessPlayer player)
        {
            return player == ChessPlayer.White ? "♘" : "♞";
        }

        public override ChessPiece Clone()
        {
            return new Knight(Owner, HasMoved);
        }
    }
}
