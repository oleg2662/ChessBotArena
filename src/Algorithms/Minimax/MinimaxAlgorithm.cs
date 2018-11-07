using System.Linq;
using Algorithms.Abstractions.Interfaces;
using Algorithms.Minimax.Exceptions;

namespace Algorithms.Minimax
{
    /// <summary>
    /// The minimax algorithm implementation.
    /// </summary>
    /// <typeparam name="TState">The type of the states which have to be evaluated.</typeparam>
    /// <typeparam name="TMove">The type of the moves between states.</typeparam>
    public class MinimaxAlgorithm<TState, TMove> : IAlgorithm<TState, TMove>
        where TMove : class
    {
        private int _maxDepth;

        private readonly IEvaluator<TState> _evaluator;
        private readonly IGenerator<TState, TMove> _moveGenerator;
        private readonly IApplier<TState, TMove> _moveApplier;

        public MinimaxAlgorithm(IEvaluator<TState> evaluator, IGenerator<TState, TMove> moveGenerator, IApplier<TState, TMove> applier)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _moveApplier = applier;
            _maxDepth = 3;
        }

        /// <summary>
        /// Gets or sets the maximum depth (number of transitions) the algorithm is checking.
        /// </summary>
        public int MaxDepth
        {
            get => _maxDepth;

            set
            {
                if(value < 1)
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

            if(!moves.Any())
            {
                return null;
            }

            var movesAndValues = moves.Select(move => new
            {
                Move = move,
                Value = Calculate(_moveApplier.Apply(state, move), 1, false)
            });

            return movesAndValues.OrderByDescending(x => x.Value).First().Move;
        }

        private int Calculate(TState state, int depth, bool maximize)
        {
            var nextMoves = _moveGenerator.Generate(state).ToList();
            var isLeaf = !nextMoves.Any();

            if (depth == MaxDepth || isLeaf)
            {
                return _evaluator.Evaluate(state);
            }

            var childrenValues = nextMoves.Select(x => _moveApplier.Apply(state, x))
                                            .Select(x => Calculate(x, depth + 1, !maximize))
                                            .ToList();
            if (maximize)
            {
                var childrenMax = childrenValues.Union(new[] { int.MinValue }).Max();
                return childrenMax;
            }
            else
            {
                var childrenMin = childrenValues.Union(new[] { int.MaxValue }).Min();
                return childrenMin;
            }
        }
    }
}
