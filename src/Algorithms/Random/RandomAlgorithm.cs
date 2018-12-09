using System.Linq;
using BoardGame.Algorithms.Abstractions.Interfaces;

namespace BoardGame.Algorithms.Random
{
    /// <summary>
    /// The random-algorithm.Tries to impress you with it's speed, but is working only randomly.
    /// Let him win sometimes or it may become upset.
    /// </summary>
    /// <typeparam name="TState">Type of the states.</typeparam>
    /// <typeparam name="TMove">Type of the moves.</typeparam>
    public class RandomAlgorithm<TState, TMove> : IAlgorithm<TState, TMove>
        where TMove : class
    {
        private readonly IGenerator<TState, TMove> _moveGenerator;

        public RandomAlgorithm(IGenerator<TState, TMove> moveGenerator)
        {
            _moveGenerator = moveGenerator;
        }

        /// <inheritdoc />
        public TMove Calculate(TState state)
        {
            var moves = _moveGenerator.Generate(state).ToList();

            if (!moves.Any())
            {
                return null;
            }

            System.Random rnd = new System.Random();
            var randomMove = moves.OrderBy(x => rnd.Next()).First();

            return randomMove;
        }
    }
}
