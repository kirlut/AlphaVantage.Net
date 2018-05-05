using System.Collections.Generic;

namespace AlphaVantage.Net.Core.Validation
{
    /// <summary>
    /// Interface for request's validator. 
    /// </summary>
    /// <remarks>
    /// Implement it for your client in case you need pre-request validation
    /// </remarks>
    public interface IApiCallValidator
    {
        ValidationResult Validate(string apiKey, ApiFunction function, IDictionary<string, string> query);
    }
}