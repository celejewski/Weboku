using System;

namespace Application.Exceptions
{
    public class LoadException : ApplicationException
    {
        public LoadException() : base()
        {
        }

        public LoadException(string message) : base(message)
        {
        }

        public LoadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
