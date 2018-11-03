using System;

namespace Game.Chess.Pieces
{
    [Serializable]
    public sealed class Pawn : ChessPiece, IEquatable<Pawn>
    {
        public bool Equals(Pawn other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Owner, other.Owner)
                   && Equals(Kind, other.Kind)
                   && Equals(OriginalPieceKind, other.OriginalPieceKind)
                   && Equals(IsEnPassantCapturable, other.IsEnPassantCapturable);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Pawn other && Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Pawn left, Pawn right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Pawn left, Pawn right)
        {
            return !Equals(left, right);
        }

        public bool IsEnPassantCapturable { get; set; }

        public Pawn(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Pawn, hasMoved)
        {
        }

        public Pawn(ChessPlayer owner)
            : this(owner, false)
        {
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
