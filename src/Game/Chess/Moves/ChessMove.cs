using Game.Chess.Pieces;
using Game.Abstraction;
using System;
using Game.Chess.Extensions;

namespace Game.Chess.Moves
{
    [Serializable]
    public class ChessMove : IEquatable<ChessMove>, ICloneable<ChessMove>
    {
        public Position From { get; set; }

        public virtual Position To { get; set; }

        public virtual bool IsCaptureMove { get; set; }

        public ChessPlayer Owner { get; set; }

        public virtual PieceKind ChessPiece { get; set; }

        public static bool operator ==(ChessMove x, ChessMove y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Equals(y);
        }

        public static bool operator !=(ChessMove obj1, ChessMove obj2)
        {
            return !(obj1 == obj2);
        }

        public virtual bool Equals(ChessMove other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            return From.Equals(other.From) 
                    && To.Equals(other.To);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Constants.HashBase;

                hash = (hash ^ Constants.HashXor) ^ From.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ To.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ IsCaptureMove.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ ChessPiece.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ GetType().Name.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return IsCaptureMove
                    ? $"{ChessPiece.ToFigure(Owner)}{From}x{To}"
                    : $"{ChessPiece.ToFigure(Owner)}{From}{To}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as ChessMove;

            return other != null && Equals(other);
        }

        public virtual ChessMove Clone()
        {
            return new ChessMove()
            {
                ChessPiece = ChessPiece,
                Owner = Owner,
                From = From,
                To = To,
                IsCaptureMove = IsCaptureMove,
            };
        }
    }
}
