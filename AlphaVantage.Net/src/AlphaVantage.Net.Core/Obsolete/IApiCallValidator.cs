using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace AlphaVantage.Net.Core.Validation
{
    /// <summary>
    /// Interface for requests validator. 
    /// </summary>
    /// <remarks>
    /// Implement it for your client in case you need pre-request validation
    /// </remarks>
    
    [Obsolete("This class is from old version of AlfaVantage.Net library and will be removed in version 3.1. " +
    "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    public interface IApiCallValidator
    {
        ValidationResult Validate(ApiFunction function, IDictionary<string, string>? query);
    }
}