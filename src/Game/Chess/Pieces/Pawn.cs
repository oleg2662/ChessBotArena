using System;

namespace Game.Chess.Pieces
{
    [Serializable]
    public class Pawn : ChessPiece
    {
        public bool IsEnPassantCapturable { get; set; }

        public Pawn(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Pawn, hasMoved)
        {
        }

        public Pawn(ChessPlayer owner)
            : this(owner, false)
        {
        }

        public override string ToString()
        {
            return Figure(Owner);
        }

        public static string Figure(ChessPlayer player)
        {
            return player == ChessPlayer.White ? "♙" : "♟";
        }

        public override ChessPiece Clone()
        {
            return new Pawn(Owner, HasMoved)
            {
                IsEnPassantCapturable = IsEnPassantCapturable
            };
        }
    }
}
