using System;
using System.Linq;
using Algorithms.Abstractions.Interfaces;

namespace Algorithms.Dumb
{
    /// <summary>
    /// The random-algorithm.Tries to impress you with it's speed, but is working only randomly.
    /// Let him win sometimes or it may become upset.
    /// </summary>
    /// <typeparam name="TState">Type of the states.</typeparam>
    /// <typeparam name="TMove">Type of the moves.</typeparam>
    public class DumbAlgorithm<TState, TMove> : IAlgorithm<TState, TMove>
        where TMove : class
    {
        private int _maxDepth;

        protected readonly IEvaluator<TState> _evaluator;
        protected readonly IGenerator<TState, TMove> _moveGenerator;
        protected readonly IApplier<TState, TMove> _moveApplier;

        public DumbAlgorithm(IEvaluator<TState> evaluator, IGenerator<TState, TMove> moveGenerator, IApplier<TState, TMove> applier)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _moveApplier = applier;
        }

        /// <inheritdoc />
        public TMove Calculate(TState state)
        {
            var moves = _moveGenerator.Generate(state);

            if (!moves.Any())
            {
                return null;
            }

            Random rnd = new Random();
            var randomMove = moves.OrderBy(x => rnd.Next()).First();

            return randomMove;
        }
    }
}
