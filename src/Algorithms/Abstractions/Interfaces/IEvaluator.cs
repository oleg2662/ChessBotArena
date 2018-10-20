namespace Algorithms.Abstractions.Interfaces
{
    /// <summary>
    /// Interface to the evaluator of the supplied states.
    /// </summary>
    /// <typeparam name="TState">The type of the states which have to be evaluated.</typeparam>
    /// <typeparam name="TEvaluationResult">The result type of the evaluation. Please make sure the result type to be minimum partially ordered!</typeparam>
    public interface IEvaluator<TState>
    {
        /// <summary>
        /// Evaulates the given state.
        /// </summary>
        /// <param name="state">The state which has to be evaluated.</param>
        /// <returns>The result of the evaluation.</returns>
        int Evaluate(TState state);
    }
}
