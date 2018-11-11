﻿using System.Linq;
using System.Threading;
using Algorithms.Abstractions.Interfaces;
using Algorithms.Minimax.Exceptions;

namespace Algorithms.AlphaBeta
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
            var alpha = new MoveAndValue<TMove>(null, int.MinValue);
            var beta = new MoveAndValue<TMove>(null, int.MaxValue);

            var result = AlphaBetaEvaluate(null, initialState, alpha, beta, true, 1);

            return result?.Move;
        }

        private int Evaluate(TState state, bool isMaximizingNode)
        {
            var value = _evaluator.Evaluate(state);

            return isMaximizingNode ? value : -1 * value;
        }

        private MoveAndValue<TMove> AlphaBetaEvaluate(TMove moveToNode, TState node, MoveAndValue<TMove> alpha, MoveAndValue<TMove> beta, bool isMaximizingNode, int depth)
        {
            var possibleMoves = _moveGenerator.Generate(node).ToArray();
            var nodeIsLeaf = depth == MaxDepth || !possibleMoves.Any();

            if (nodeIsLeaf)
            {
                var value = Evaluate(node, isMaximizingNode);
                return new MoveAndValue<TMove>(moveToNode, value);
            }

            if (isMaximizingNode)
            {
                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = _moveApplier.Apply(node, possibleMove);
                    var value = AlphaBetaEvaluate(possibleMove, nextNode, alpha, beta, false, depth + 1);
                    alpha = MoveAndValue<TMove>.Max(alpha, value);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return moveToNode == null ? alpha : new MoveAndValue<TMove>(moveToNode, alpha.Value);
            }

            if (!isMaximizingNode)
            {
                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = _moveApplier.Apply(node, possibleMove);
                    var value = AlphaBetaEvaluate(possibleMove, nextNode, alpha, beta, true, depth + 1);
                    beta = MoveAndValue<TMove>.Min(beta, value);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                //return beta;
                return moveToNode == null ? beta : new MoveAndValue<TMove>(moveToNode, beta.Value);
            }

            return null;
        }
    }
}
