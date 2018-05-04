using System.Collections.Generic;
using AlphaVantage.Net.Core.Validation;

namespace AlphaVantage.Net.Stocks.Validation
{
    public class StocksApiCallValidator : IApiCallValidator
    {
        public ValidationResult Validate(string apiKey, string function, IDictionary<string, string> query)
        {
            throw new System.NotImplementedException();
        }
    }
}