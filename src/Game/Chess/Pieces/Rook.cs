using System;

namespace BoardGame.Game.Chess.Pieces
{
    [Serializable]
    public sealed class Rook : ChessPiece, IEquatable<Rook>
    {
        public bool Equals(Rook other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Owner, other.Owner)
                   && Equals(Kind, other.Kind)
                   && Equals(OriginalPieceKind, other.OriginalPieceKind);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Rook other && Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Rook left, Rook right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Rook left, Rook right)
        {
            return !Equals(left, right);
        }

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
