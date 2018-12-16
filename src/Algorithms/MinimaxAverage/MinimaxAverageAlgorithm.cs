using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Inner value of the maximum depth.
        /// </summary>
        protected int MaxDepthInnerValue;

        /// <summary>
        /// Inner value of the maximum number of nodes calculated in the average calculation.
        /// </summary>
        protected int MaxLevelAverageDepthInnerValue = 2;

        /// <summary>
        /// Inner value of the minimum number of nodes calculated in the average calculation.
        /// </summary>
        protected int MinLevelAverageDepthInnerValue = 2;

        protected readonly IEvaluator<TState> Evaluator;
        protected readonly IGenerator<TState, TMove> MoveGenerator;
        protected readonly IApplier<TState, TMove> MoveApplier;

        /// <summary>
        /// Creates a minimax average algorithm object.
        /// </summary>
        /// <param name="evaluator">The evaluator for the nodes.</param>
        /// <param name="moveGenerator">The move generator for the nodes.</param>
        /// <param name="applier">The move applier for the moves.</param>
        public MinimaxAverageAlgorithm(IEvaluator<TState> evaluator, IGenerator<TState, TMove> moveGenerator, IApplier<TState, TMove> applier)
        {
            Evaluator = evaluator;
            MoveGenerator = moveGenerator;
            MoveApplier = applier;
            MaxDepthInnerValue = 3;
        }

        /// <summary>
        /// Gets or sets the number of minimum elements for average calculation.
        /// </summary>
        public virtual int MinLevelAverageDepth
        {
            get => MinLevelAverageDepthInnerValue;

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(MinLevelAverageDepth));
                }

                MinLevelAverageDepthInnerValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of maximum elements for average calculation.
        /// </summary>
        public virtual int MaxLevelAverageDepth
        {
            get => MaxLevelAverageDepthInnerValue;

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(MaxLevelAverageDepth));
                }

                MaxLevelAverageDepthInnerValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum depth (number of transitions) the algorithm is checking.
        /// </summary>
        public int MaxDepth
        {
            get => MaxDepthInnerValue;

            set
            {
                if (value < 1)
                {
                    throw new MinimaxMaxDepthInvalidException();
                }

                MaxDepthInnerValue = value;
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
            var firstSteps = MoveGenerator.Generate(initialState);
            var firstLevelValues = firstSteps.Select(x => new
            {
                SelectedMove = x,
                Value = MiniMaxEvaluate(MoveApplier.Apply(initialState, x), false, 1)
            });

            return firstLevelValues.OrderByDescending(x => x.Value).FirstOrDefault()?.SelectedMove;
        }

        private int MiniMaxEvaluate(TState node, bool isMaximizingNode, int depth)
        {
            if (depth >= MaxDepth)
            {
                return Evaluator.Evaluate(node);
            }

            var possibleMoves = MoveGenerator.Generate(node).ToArray();
            var nodeIsLeaf =!possibleMoves.Any();

            if (nodeIsLeaf)
            {
                return Evaluator.Evaluate(node);
            }

            if (isMaximizingNode)
            {
                var value = int.MinValue;
                var values = new List<int>(possibleMoves.Length);

                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = MoveApplier.Apply(node, possibleMove);
                    values.Add(MiniMaxEvaluate(nextNode, false, depth + 1));
                }

                var newValue = values.OrderByDescending(x => x).Take(MaxLevelAverageDepth).Average(x => x);

                return (int) Math.Max(value, newValue);
            }
            else
            {
                var value = int.MaxValue;
                var values = new List<int>(possibleMoves.Length);

                foreach (var possibleMove in possibleMoves)
                {
                    var nextNode = MoveApplier.Apply(node, possibleMove);
                    values.Add(MiniMaxEvaluate(nextNode, true, depth + 1));
                }

                var newValue = values.OrderBy(x => x).Take(MinLevelAverageDepth).Average(x => x);

                return (int) Math.Min(value, newValue);
            }
        }
    }
}
