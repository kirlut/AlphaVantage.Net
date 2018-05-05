using System;

namespace AlphaVantage.Net.Core.Exceptions
{
    public class AlphaVantageException : Exception
    {
        public AlphaVantageException(string message) : base(message)
        {
        }

        public AlphaVantageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}