using System.Collections.Generic;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Validation;

namespace AlphaVantage.Net.Stocks.Validation
{
    internal class StocksApiCallValidator : IApiCallValidator
    {
        public ValidationResult Validate(ApiFunction function, IDictionary<string, string> query)
        {
            throw new System.NotImplementedException();
        }
    }
}