using System;

namespace Weboku.Application.Exceptions
{
    public class HistoryManagerInvalidOperation : ApplicationException
    {
        public HistoryManagerInvalidOperation() : base()
        {
        }

        public HistoryManagerInvalidOperation(string message) : base(message)
        {
        }

        public HistoryManagerInvalidOperation(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}