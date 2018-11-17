using System;
using Newtonsoft.Json;

namespace BoardGame.Game.Chess.Pieces
{
    [Serializable]
    public sealed class Knight : ChessPiece, IEquatable<Knight>
    {
        public bool Equals(Knight other)
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
            return obj is Knight other && Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Knight left, Knight right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Knight left, Knight right)
        {
            return !Equals(left, right);
        }

        public Knight(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Knight, hasMoved)
        {
        }

        [JsonConstructor]
        public Knight(ChessPlayer owner)
            : this(owner, false)
        {
        }

        public override ChessPiece Clone()
        {
            return new Knight(Owner, HasMoved);
        }
    }
}
