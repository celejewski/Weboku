using System;

namespace Core.Exceptions
{
    public class InvalidGridException : SudokuCoreException
    {
        public InvalidGridException() : base()
        {
        }

        public InvalidGridException(string message) : base(message)
        {
        }

        public InvalidGridException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
