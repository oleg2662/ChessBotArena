namespace BoardGame.Model.Abstractions.Interfaces
{
    /// <summary>
    /// Interface to the evaluator of the supplied states.
    /// </summary>
    /// <typeparam name="TState">The type of the states which have to be evaluated.</typeparam>
    public interface IEvaluator<in TState>
    {
        /// <summary>
        /// Evaluates the given state.
        /// </summary>
        /// <param name="state">The state which has to be evaluated.</param>
        /// <returns>The result of the evaluation.</returns>
        int Evaluate(TState state);
    }
}
