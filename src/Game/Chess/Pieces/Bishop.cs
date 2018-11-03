using System;
using Newtonsoft.Json;

namespace Game.Chess.Pieces
{
    [Serializable]
    public sealed class Bishop : ChessPiece, IEquatable<Bishop>
    {
        public bool Equals(Bishop other)
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
            return obj is Bishop other && Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Bishop left, Bishop right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Bishop left, Bishop right)
        {
            return !Equals(left, right);
        }

        [JsonConstructor]
        public Bishop(ChessPlayer owner) : this(owner, false)
        {
        }

        public Bishop(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Bishop, hasMoved)
        {
        }

        public override ChessPiece Clone()
        {
            return new Bishop(Owner, HasMoved);
        }
    }
}
