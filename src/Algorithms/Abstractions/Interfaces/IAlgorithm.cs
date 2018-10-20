namespace Algorithms.Abstractions.Interfaces
{
    /// <summary>
    /// Interface to the algorithm used to calculate the next move.
    /// </summary>
    /// <typeparam name="TState">The type of the states which have to be evaluated.</typeparam>
    /// <typeparam name="TMove">The type of the moves between states.</typeparam>
    public interface IAlgorithm<TState, TMove>
    {
        /// <summary>
        /// Calculates the next proposed move.
        /// </summary>
        /// <param name="state">The current state from where the calculation has to happen.</param>
        /// <returns>A proposed move.</returns>
        TMove Calculate(TState state, bool maximize);
    }
}
