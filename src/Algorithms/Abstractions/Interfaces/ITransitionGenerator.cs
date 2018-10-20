using System.Collections.Generic;

namespace Algorithms.Abstractions.Interfaces
{
    /// <summary>
    /// Generator used to get all the possible (and/or significant) transitions from a given state.
    /// </summary>
    /// <typeparam name="TState">Type of the states.</typeparam>
    /// <typeparam name="TTransition">Type of the transitions between states.</typeparam>
    public interface ITransitionGenerator<TState, TTransition>
    {
        /// <summary>
        /// Generates the possible transitions from the given state.
        /// </summary>
        /// <param name="state">The initial state.</param>
        /// <returns>The possible transitions from the given state.</returns>
        IEnumerable<TTransition> Generate(TState state);
    }
}
