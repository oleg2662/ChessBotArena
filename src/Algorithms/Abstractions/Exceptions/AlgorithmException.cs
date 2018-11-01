using System;
using System.Runtime.Serialization;

namespace Algorithms.Abstractions.Exceptions
{
    public abstract class AlgorithmException : Exception
    {
        protected AlgorithmException()
        {
        }

        protected AlgorithmException(string message)
            : base(message)
        {
        }

        protected AlgorithmException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AlgorithmException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
