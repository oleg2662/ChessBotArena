using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Game.Chess.Exceptions
{
    public abstract class ChessException : Exception
    {
        protected ChessException() : base()
        {
        }

        protected ChessException(string message)
            : base(message)
        {
        }

        protected ChessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ChessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
