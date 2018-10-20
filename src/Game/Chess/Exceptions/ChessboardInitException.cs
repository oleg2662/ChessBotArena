using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Chess.Exceptions
{
    public class ChessBoardInitException : ChessException
    {
        public ChessBoardInitException() : base()
        {
        }

        public ChessBoardInitException(string message)
            : base(message)
        {
        }

        public ChessBoardInitException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
