using System;
using JetBrains.Annotations;

namespace AlphaVantage.Net.Common.Exceptions
{
    public class AlphaVantageParsingException : AlphaVantageException
    {
        public AlphaVantageParsingException([NotNull] string message) : base(message)
        {
        }

        public AlphaVantageParsingException([NotNull] string message, [NotNull] Exception innerException) : base(message, innerException)
        {
        }
    }
}