using System;

namespace Game.Chess.Exceptions
{
    public class ChessBoardInitException : ChessException
    {
        public ChessBoardInitException()
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
