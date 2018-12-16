using System;
using System.Linq;
using BoardGame.Algorithms.Abstractions.Interfaces;
using BoardGame.Algorithms.Minimax.Exceptions;

namespace BoardGame.Algorithms.AlphaBeta
{
    /// <summary>
    /// The alpha-beta algorithm implementation.
    /// </summary>
    /// <typeparam name="TState">The type of the states which have to be evaluated.</typeparam>
    /// <typeparam name="TMove">The type of the moves between states.</typeparam>
    public class AlphaBetaAlgorithm<TState, TMove> : IAlgorithm<TState, TMove>
        where TMove : class
    {
        private int _maxDepth;

        private readonly IEvaluator<TState> _evaluator;
        private readonly IGenerator<TState, TMove> _moveGenerator;
        private readonly IApplier<TState, TMove> _moveApplier;

        public AlphaBetaAlgorithm(IEvaluator<TState> evaluator, IGenerator<TState, TMove> moveGenerator,
            IApplier<TState, TMove> applier)
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
            var result = AlphaBeta(state);
            return result;
        }

        private TMove AlphaBeta(TState initialState)
        {
            var firstSteps = _moveGenerator.Generate(initialState);
            var firstLevelValues = firstSteps.Select(x => new
            {
                SelectedMove = x,
                Value = AlphaBetaEvaluate(_moveApplier.Apply(initialState, x), int.MinValue, int.MaxValue, false, 1)
            });

            return firstLevelValues.OrderByDescending(x => x.Value).FirstOrDefault()?.SelectedMove;
        }

        private int AlphaBetaEvaluate(TState node, int alpha, int beta, bool isMaximizingNode, int depth)
        {
            if (depth >= MaxDepth)
            {
                return _evaluator.Evaluate(node);
            }

            var possibleMoves = _moveGenerator.Generate(node).ToArray();
            var nodeIsLeaf = !possibleMoves.Any();
            int value;

            if (nodeIsLeaf)
            {
                return _evaluator.Evaluate(node);
            }

            if (isMaximizingNode)
            {
                value = int.MinValue;
                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = _moveApplier.Apply(node, possibleMove);
                    value = Math.Max(value, AlphaBetaEvaluate(nextNode, alpha, beta, false, depth + 1));
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                    {
                        // beta cut-off
                        break;
                    }
                }

                return value;
            }
            else
            {
                value = int.MaxValue;
                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = _moveApplier.Apply(node, possibleMove);
                    value = Math.Min(value, AlphaBetaEvaluate(nextNode, alpha, beta, true, depth + 1));
                    beta = Math.Min(beta, value);
                    if (alpha >= beta)
                    {
                        // alpha cut-off
                        break;
                    }
                }

                return value;
            }
        }
    }
}
