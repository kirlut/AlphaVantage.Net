using System;
using System.Collections.Generic;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Validation;
// ReSharper disable CheckNamespace

namespace AlphaVantage.Net.Stocks.Validation
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    internal class StocksApiCallValidator : IApiCallValidator
    {
        public ValidationResult Validate(ApiFunction function, IDictionary<string, string>? query)
        {
            throw new System.NotImplementedException();
        }
    }
}