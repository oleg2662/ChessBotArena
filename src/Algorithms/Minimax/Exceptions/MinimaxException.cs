using Algorithms.Abstractions.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Algorithms.Minimax.Exceptions
{
    public abstract class MinimaxException : AlgorithmException
    {
        protected MinimaxException()
        {
        }

        protected MinimaxException(string message)
            : base(message)
        {
        }

        protected MinimaxException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MinimaxException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
