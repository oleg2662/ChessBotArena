using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Algorithms.Abstractions.Interfaces;
using BoardGame.Algorithms.Minimax.Exceptions;
using BoardGame.Algorithms.MinimaxAverage;

namespace BoardGame.Algorithms.Minimax
{
    /// <summary>
    /// The minimax algorithm implementation.
    /// </summary>
    /// <typeparam name="TState">The type of the states which have to be evaluated.</typeparam>
    /// <typeparam name="TMove">The type of the moves between states.</typeparam>
    public class MinimaxAlgorithm<TState, TMove> : MinimaxAverageAlgorithm<TState, TMove>
        where TMove : class
    {
        /// <summary>
        /// Creates a minimax algorithm object.
        /// (Which is the same as hte minimax average with the number of nodes average calculations set to 1.
        /// </summary>
        /// <param name="evaluator">The evaluator for the nodes.</param>
        /// <param name="moveGenerator">The move generator for the nodes.</param>
        /// <param name="applier">The move applier for the moves.</param>
        public MinimaxAlgorithm(IEvaluator<TState> evaluator, IGenerator<TState, TMove> moveGenerator, IApplier<TState, TMove> applier)
        : base(evaluator, moveGenerator, applier)
        {
            MaxLevelAverageDepthInnerValue = 1;
            MinLevelAverageDepthInnerValue = 1;
        }

        /// <inheritdoc />
        public override int MaxLevelAverageDepth => MaxLevelAverageDepthInnerValue;

        /// <inheritdoc />
        public override int MinLevelAverageDepth => MinLevelAverageDepthInnerValue;
    }
}
