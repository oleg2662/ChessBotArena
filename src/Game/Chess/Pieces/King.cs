using System;

namespace Game.Chess.Pieces
{
    [Serializable]
    public sealed class King : ChessPiece, IEquatable<King>
    {
        public bool Equals(King other)
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
            return obj is King other && Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(King left, King right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(King left, King right)
        {
            return !Equals(left, right);
        }

        public King(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.King, hasMoved)
        {
        }

        public King(ChessPlayer owner) 
            : this(owner, false)
        {
        }

        public override ChessPiece Clone()
        {
            return new King(Owner, HasMoved);
        }
    }
}
