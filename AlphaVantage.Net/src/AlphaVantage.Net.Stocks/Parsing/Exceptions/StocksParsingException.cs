using System;
using AlphaVantage.Net.Core.Exceptions;

namespace AlphaVantage.Net.Stocks.Parsing.Exceptions
{
    public class StocksParsingException : AlphaVantageException
    {
        public StocksParsingException(string message) : base(message)
        {
        }

        public StocksParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}