using System;

namespace Weboku.Core.Exceptions
{
    public class SudokuCoreException : Exception
    {
        public SudokuCoreException() : base()
        {
        }

        public SudokuCoreException(string message) : base(message)
        {
        }

        public SudokuCoreException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}