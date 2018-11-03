using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Game.Chess.Moves
{
    [Serializable]
    [DebuggerDisplay("{From}->{To}(ep:{CapturePosition})")]
    public sealed class PawnEnPassantMove : BaseChessMove, IEquatable<PawnEnPassantMove>
    {
        public bool Equals(PawnEnPassantMove other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(From, other.From)
                   && Equals(To, other.To)
                   && Equals(CapturePosition, other.CapturePosition);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((PawnEnPassantMove) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Owner.GetHashCode();
                hashCode = (hashCode * 397) ^ (From != null ? From.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (To != null ? To.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CapturePosition != null ? CapturePosition.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(PawnEnPassantMove left, PawnEnPassantMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PawnEnPassantMove left, PawnEnPassantMove right)
        {
            return !Equals(left, right);
        }

        public Position CapturePosition { get; }

        [JsonConstructor]
        public PawnEnPassantMove(ChessPlayer owner, Position from, Position to, Position capturePosition)
            : base(owner, from, to)
        {
            CapturePosition = capturePosition;
        }

        public override BaseMove Clone()
        {
            return new PawnEnPassantMove(Owner, From, To, CapturePosition);
        }
    }
}
