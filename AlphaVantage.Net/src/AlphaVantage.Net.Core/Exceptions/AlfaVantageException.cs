using System;

namespace AlphaVantage.Net.Core.Exceptions
{
    public class AlfaVantageException : Exception
    {
        public AlfaVantageException(string message) : base(message)
        {
        }

        public AlfaVantageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}