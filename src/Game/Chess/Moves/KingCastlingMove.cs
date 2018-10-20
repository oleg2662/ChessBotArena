using Game.Chess.Extensions;
using System;

namespace Game.Chess.Moves
{
    [Serializable]
    public sealed class KingCastlingMove : ChessMove
    {
        public Position RookFrom
        {
            get
            {
                switch (CastlingType)
                {
                    case CastlingType.Long:
                        return new Position('A', From.Row);
                    case CastlingType.Short:
                        return new Position('H', From.Row);
                }

                throw new ArgumentOutOfRangeException(nameof(CastlingType));
            }
        }

        public Position RookTo
        {
            get
            {
                switch (CastlingType)
                {
                    case CastlingType.Long:
                        return new Position('A', From.Row);
                    case CastlingType.Short:
                        return new Position('D', From.Row);
                }

                throw new ArgumentOutOfRangeException(nameof(CastlingType));
            }
        }

        public override Position To
        {
            get
            {
                switch (CastlingType)
                {
                    case CastlingType.Long:
                        return From.East(2);
                    case CastlingType.Short:
                        return From.West(2);
                }

                throw new ArgumentOutOfRangeException(nameof(CastlingType));
            }

            set { }
        }

        public CastlingType CastlingType { get; set; }

        public override bool Equals(ChessMove other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var specificMove = (KingCastlingMove)other;

            if (specificMove == null)
            {
                return false;
            }

            return CastlingType.Equals(specificMove.CastlingType)
                && RookFrom.Equals(specificMove.RookFrom)
                && RookTo.Equals(specificMove.RookTo);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = base.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ this.RookFrom.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ this.RookTo.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ this.CastlingType.GetHashCode();
                hash = (hash ^ Constants.HashXor) ^ nameof(KingCastlingMove).GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return CastlingType == CastlingType.Long ? "0-0-0" : "0-0";
        }

        public override ChessMove Clone()
        {
            return new KingCastlingMove()
            {
                ChessPiece = this.ChessPiece.Clone(),
                From = this.From,
                To = this.To,
                IsCaptureMove = this.IsCaptureMove,
                CastlingType = this.CastlingType
            };
        }
    }
}
