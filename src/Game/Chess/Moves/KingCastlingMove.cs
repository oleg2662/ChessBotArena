using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [Serializable]
    [DebuggerDisplay("{From}->{To}(Rook:{RookFrom}->{RookTo})")]
    public sealed class KingCastlingMove : BaseChessMove, IEquatable<KingCastlingMove>
    {
        public bool Equals(KingCastlingMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(From, other.From)
                   && Equals(To, other.To)
                   && Equals(CastlingType, other.CastlingType);
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
                int hashCode = Owner.GetHashCode();
                hashCode = (hashCode * 397) ^ (From != null ? From.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (To != null ? To.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ((int) CastlingType).GetHashCode();
                return hashCode;
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

        public Position RookFrom { get; }

        public Position RookTo { get; }

        public CastlingType CastlingType { get; }

        [JsonConstructor]
        public KingCastlingMove(ChessPlayer owner, CastlingType castlingType)
            : base(owner, CalculateFrom(owner), CalculateTo(owner, castlingType))
        {
            CastlingType = castlingType;
            switch (owner)
            {
                case ChessPlayer.White:
                    switch (castlingType)
                    {
                        case CastlingType.Long:
                            RookFrom = Positions.A1;
                            RookTo = Positions.D1;
                            break;

                        case CastlingType.Short:
                            RookFrom = Positions.H1;
                            RookTo = Positions.F1;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(castlingType), owner, null);
                    }
                    break;

                case ChessPlayer.Black:
                    switch (castlingType)
                    {
                        case CastlingType.Long:
                            RookFrom = Positions.A8;
                            RookTo = Positions.D8;
                            break;

                        case CastlingType.Short:
                            RookFrom = Positions.H8;
                            RookTo = Positions.F8;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(castlingType), owner, null);
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(owner), owner, null);
            }
        }

        public override BaseMove Clone()
        {
            return new KingCastlingMove(Owner, CastlingType);
        }

        private static Position CalculateFrom(ChessPlayer owner)
        {
            switch (owner)
            {
                case ChessPlayer.White:
                    return Positions.E1;
                case ChessPlayer.Black:
                    return Positions.E8;
                default:
                    throw new ArgumentOutOfRangeException(nameof(owner), owner, null);
            }
        }

        private static Position CalculateTo(ChessPlayer owner, CastlingType castlingType)
        {
            switch (owner)
            {
                case ChessPlayer.White:
                    switch (castlingType)
                    {
                        case CastlingType.Long: return Positions.C1;
                        case CastlingType.Short: return Positions.G1;
                        default: throw new ArgumentOutOfRangeException(nameof(castlingType), owner, null);
                    }

                case ChessPlayer.Black:
                    switch (castlingType)
                    {
                        case CastlingType.Long: return Positions.C8;
                        case CastlingType.Short: return Positions.G8;
                        default: throw new ArgumentOutOfRangeException(nameof(castlingType), owner, null);
                    }

                default: throw new ArgumentOutOfRangeException(nameof(owner), owner, null);
            }
        }
    }
}
