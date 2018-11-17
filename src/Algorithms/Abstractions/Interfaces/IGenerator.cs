using System.Collections.Generic;

namespace BoardGame.Model.Abstractions.Interfaces
{
    /// <summary>
    /// Generator used to get all the possible (and/or significant) transitions from a given state.
    /// </summary>
    /// <typeparam name="TState">Type of the states.</typeparam>
    /// <typeparam name="TMove">Type of the transitions between states.</typeparam>
    public interface IGenerator<in TState, out TMove>
    {
        /// <summary>
        /// Generates the possible transitions from the given state.
        /// </summary>
        /// <param name="state">The initial state.</param>
        /// <returns>The possible transitions from the given state.</returns>
        IEnumerable<TMove> Generate(TState state);
    }
}
