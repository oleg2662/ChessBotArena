namespace BoardGame.Algorithms.Minimax.Exceptions
{
    public class MinimaxMaxDepthInvalidException : MinimaxException
    {
        public MinimaxMaxDepthInvalidException() 
            : base("Maximum depth of the algorithm has to be set higher than 0.")
        {
        }
    }
}
