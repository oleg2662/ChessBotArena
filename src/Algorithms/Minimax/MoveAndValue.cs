using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGame.Algorithms.Minimax
{
    internal class MoveAndValue<TMove> : IComparable<MoveAndValue<TMove>>, IComparable
    {
        public static MoveAndValue<TMove> Max(params MoveAndValue<TMove>[] values)
        {
            return values.OrderByDescending(x => x).First();
        }

        public static MoveAndValue<TMove> Min(params MoveAndValue<TMove>[] values)
        {
            return values.OrderBy(x => x).First();
        }

        public int CompareTo(MoveAndValue<TMove> other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is AlphaBeta.MoveAndValue<TMove> other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(AlphaBeta.MoveAndValue<TMove>)}");
        }

        public static bool operator <(MoveAndValue<TMove> left, MoveAndValue<TMove> right)
        {
            return Comparer<MoveAndValue<TMove>>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(MoveAndValue<TMove> left, MoveAndValue<TMove> right)
        {
            return Comparer<MoveAndValue<TMove>>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(MoveAndValue<TMove> left, MoveAndValue<TMove> right)
        {
            return Comparer<MoveAndValue<TMove>>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(MoveAndValue<TMove> left, MoveAndValue<TMove> right)
        {
            return Comparer<MoveAndValue<TMove>>.Default.Compare(left, right) >= 0;
        }

        public MoveAndValue(TMove move, int value)
        {
            Move = move;
            Value = value;
        }

        public TMove Move { get; }
        public int Value { get; }
    }
}
