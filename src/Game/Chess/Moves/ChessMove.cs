using Game.Chess.Pieces;
using Game.Abstraction;
using System;

namespace Game.Chess.Moves
{
    [Serializable]
    public class ChessMove : IEquatable<ChessMove>, ICloneable<ChessMove>
    {
        public Position From { get; set; }

        public virtual Position To { get; set; }

        public bool IsCaptureMove { get; set; }

        public ChessPlayer Owner { get; set; }

        public PieceKind ChessPiece { get; set; }

        public ChessMoveResult ChessMoveResult { get; set; }

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

            var nonNullSide = x is null ? y : x;
            var possibleNullSide = y is null ? x : y;

            return nonNullSide.Equals(possibleNullSide);
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
                    && To.Equals(other.To)
                    && ChessMoveResult.Equals(other.ChessMoveResult);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Constants.HashBase;

                hash = (hash ^ Constants.HashXor) ^ From.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ To.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ IsCaptureMove.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ ChessMoveResult.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ ChessPiece.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ GetType().Name.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            var moveText =  IsCaptureMove
                                ? $"{ChessPiece.ToString()}{From}x{To}"
                                : $"{ChessPiece.ToString()}{From}{To}";
            switch (ChessMoveResult)
            {
                case ChessMoveResult.Check:
                    return $"{moveText}+";
                case ChessMoveResult.CheckMate:
                    return $"{moveText}#";
                default:
                    return moveText;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (ChessMove)obj;
            if (other == null)
            {
                return false;
            }

            return Equals(other);
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
                ChessMoveResult = ChessMoveResult
            };
        }
    }
}
