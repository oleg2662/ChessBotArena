namespace BoardGame.Model.Abstractions.Interfaces
{
    public interface IApplier<TState, in TMove>
    {
        /// <summary>
        /// Applies the given move to the given state and returns the new state.
        /// </summary>
        /// <param name="state">The original state on which the move will be applied.</param>
        /// <param name="move">The move which will be applied to the state.</param>
        /// <returns>The new state.</returns>
        TState Apply(TState state, TMove move);
    }
}
