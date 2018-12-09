using System;
using System.Linq;
using BoardGame.Algorithms.Abstractions.Interfaces;
using BoardGame.Algorithms.Minimax.Exceptions;

namespace BoardGame.Algorithms.MinimaxAverage
{
    /// <summary>
    /// The minimax algorithm implementation.
    /// </summary>
    /// <typeparam name="TState">The type of the states which have to be evaluated.</typeparam>
    /// <typeparam name="TMove">The type of the moves between states.</typeparam>
    public class MinimaxAverageAlgorithm<TState, TMove> : IAlgorithm<TState, TMove>
        where TMove : class
    {
        private int _maxDepth;
        private int _maxLevelAverageDepth = 2;
        private int _minLevelAverageDepth = 2;

        private readonly IEvaluator<TState> _evaluator;
        private readonly IGenerator<TState, TMove> _moveGenerator;
        private readonly IApplier<TState, TMove> _moveApplier;

        public MinimaxAverageAlgorithm(IEvaluator<TState> evaluator, IGenerator<TState, TMove> moveGenerator, IApplier<TState, TMove> applier)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _moveApplier = applier;
            _maxDepth = 3;
        }

        /// <summary>
        /// Gets or sets the maximum number of minimum elements for average calculation.
        /// </summary>
        public int MinLevelAverageDepth
        {
            get => _minLevelAverageDepth;

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(MinLevelAverageDepth));
                }

                _minLevelAverageDepth = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of maximum elements for average calculation.
        /// </summary>
        public int MaxLevelAverageDepth
        {
            get => _maxLevelAverageDepth;

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(MaxLevelAverageDepth));
                }

                _maxLevelAverageDepth = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum depth (number of transitions) the algorithm is checking.
        /// </summary>
        public int MaxDepth
        {
            get => _maxDepth;

            set
            {
                if (value < 1)
                {
                    throw new MinimaxMaxDepthInvalidException();
                }

                _maxDepth = value;
            }
        }

        /// <inheritdoc />
        public TMove Calculate(TState state)
        {
            var moves = _moveGenerator.Generate(state).ToList();

            if (!moves.Any())
            {
                return null;
            }

            var movesAndValues = moves.Select(move => new
            {
                Move = move,
                Value = Calculate(_moveApplier.Apply(state, move), 1, false)
            }).ToList();

            var max = movesAndValues.Max(x => x.Value);
            var result = movesAndValues.First(x => Equals(x.Value, max)).Move;

            return result;
        }

        private int Calculate(TState state, int depth, bool maximize)
        {
            if (depth == MaxDepth)
            {
                return _evaluator.Evaluate(state);
            }

            var nextMoves = _moveGenerator.Generate(state).ToList();
            var isLeaf = !nextMoves.Any();

            if (isLeaf)
            {
                return _evaluator.Evaluate(state);
            }

            var childrenValues = nextMoves
                                    .Select(x => _moveApplier.Apply(state, x))
                                    .Select(x => Calculate(x, depth + 1, !maximize))
                                    .ToList();
            if (maximize)
            {
                var childrenMax = (int)Math.Round(childrenValues.Union(new[] { int.MinValue }).OrderByDescending(x => x).Take(MaxLevelAverageDepth).Average());
                return childrenMax;
            }
            else
            {
                var childrenMin = (int)Math.Round(childrenValues.Union(new[] { int.MaxValue }).OrderBy(x => x).Take(MinLevelAverageDepth).Average());
                return childrenMin;
            }
        }
    }
}
