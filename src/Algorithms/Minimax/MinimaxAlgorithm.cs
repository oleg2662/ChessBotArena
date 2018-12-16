using System;
using System.Linq;
using BoardGame.Algorithms.Abstractions.Interfaces;
using BoardGame.Algorithms.Minimax.Exceptions;

namespace BoardGame.Algorithms.Minimax
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
            var result = MiniMax(state);
            return result;
        }

        private TMove MiniMax(TState initialState)
        {
            var firstSteps = _moveGenerator.Generate(initialState);
            var firstLevelValues = firstSteps.Select(x => new
            {
                SelectedMove = x,
                Value = MiniMaxEvaluate(_moveApplier.Apply(initialState, x), false, 2)
            });

            return firstLevelValues.OrderByDescending(x => x.Value).FirstOrDefault()?.SelectedMove;
        }

        private int Evaluate(TState state, bool isMaximizingNode)
        {
            var value = _evaluator.Evaluate(state);
            return isMaximizingNode ? value : -1 * value;
        }

        private int MiniMaxEvaluate(TState node, bool isMaximizingNode, int depth)
        {
            var possibleMoves = _moveGenerator.Generate(node).ToArray();
            var nodeIsLeaf = depth == MaxDepth || !possibleMoves.Any();

            if (nodeIsLeaf)
            {
                var value = Evaluate(node, isMaximizingNode);
                return value;
            }

            if (isMaximizingNode)
            {
                var value = int.MinValue;

                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = _moveApplier.Apply(node, possibleMove);
                    value = (int) Math.Max(value, MiniMaxEvaluate(nextNode, false, depth + 1));
                }

                return value;
            }
            else
            {
                var value = int.MaxValue;

                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = _moveApplier.Apply(node, possibleMove);
                    value = Math.Min(value, MiniMaxEvaluate(nextNode, true, depth + 1));
                }

                return value;
            }
        }
    }
}
