using Game.Chess.Extensions;
using System;

namespace Game.Chess.Moves
{
    [Serializable]
    public sealed class KingCastlingMove : ChessMove, IEquatable<KingCastlingMove>
    {
        public bool Equals(KingCastlingMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return base.Equals(other) 
                   && CastlingType == other.CastlingType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is KingCastlingMove other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (int) CastlingType;
            }
        }

        public static bool operator ==(KingCastlingMove left, KingCastlingMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(KingCastlingMove left, KingCastlingMove right)
        {
            return !Equals(left, right);
        }

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

        private CastlingType _castlingType;
        public CastlingType CastlingType
        {
            get => _castlingType;
            set
            {
                _castlingType = value;
                To = CalculateTo(value, From);
            }
        }

        public KingCastlingMove(ChessPlayer owner, Position from, CastlingType castlingType)
            : base(owner, from, CalculateTo(castlingType, from))
        {
            CastlingType = castlingType;
        }

        public override BaseMove Clone()
        {
            return new KingCastlingMove(Owner, From, CastlingType);
        }

        
        private static Position CalculateTo(CastlingType castlingType, Position from)
        {
            switch (castlingType)
            {
                case CastlingType.Long:
                    return from.East(2);

                case CastlingType.Short:
                    return from.West(2);

                default: throw new ArgumentOutOfRangeException(nameof(CastlingType));
            }
        }
    }
}
