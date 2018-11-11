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
            return MiniMax(state);
        }

        private TMove MiniMax(TState initialState)
        {
            var result = MiniMaxEvaluate(null, initialState, true, 1);
            return result?.Move;
        }

        private int Evaluate(TState state, bool isMaximizingNode)
        {
            var value = _evaluator.Evaluate(state);
            return isMaximizingNode ? value : -1 * value;
        }

        private MoveAndValue<TMove> MiniMaxEvaluate(TMove moveToNode, TState node, bool isMaximizingNode, int depth)
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
                var value = new MoveAndValue<TMove>(null, int.MinValue);

                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = _moveApplier.Apply(node, possibleMove);
                    value = MoveAndValue<TMove>.Max(MiniMaxEvaluate(possibleMove, nextNode, false, depth + 1), value);
                }

                return moveToNode == null ? value : new MoveAndValue<TMove>(moveToNode, value.Value);
            }

            if (!isMaximizingNode)
            {
                var value = new MoveAndValue<TMove>(null, int.MaxValue);

                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = _moveApplier.Apply(node, possibleMove);
                    value = MoveAndValue<TMove>.Min(MiniMaxEvaluate(possibleMove, nextNode, false, depth + 1), value);
                }

                return moveToNode == null ? value : new MoveAndValue<TMove>(moveToNode, value.Value);
            }

            return null;
        }
    }
}
