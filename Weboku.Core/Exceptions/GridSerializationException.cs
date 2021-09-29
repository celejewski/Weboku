using System;

namespace Weboku.Core.Exceptions
{
    public class GridSerializationException : SudokuCoreException
    {
        public GridSerializationException() : base()
        {
        }

        public GridSerializationException(string message) : base(message)
        {
        }

        public GridSerializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}