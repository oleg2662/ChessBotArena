using System;
using Newtonsoft.Json;

namespace Game.Chess.Pieces
{
    [Serializable]
    public sealed class Queen : ChessPiece, IEquatable<Queen>
    {
        public bool Equals(Queen other)
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
            return obj is Queen other && Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Queen left, Queen right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Queen left, Queen right)
        {
            return !Equals(left, right);
        }

        public Queen(ChessPlayer owner, bool hasMoved)
            : base(owner, PieceKind.Queen, hasMoved)
        {
        }

        [JsonConstructor]
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
