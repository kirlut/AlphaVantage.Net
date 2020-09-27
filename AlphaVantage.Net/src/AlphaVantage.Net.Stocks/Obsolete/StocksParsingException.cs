using System;
using AlphaVantage.Net.Common.Exceptions;

// ReSharper disable once CheckNamespace
namespace AlphaVantage.Net.Stocks.Parsing.Exceptions
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
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