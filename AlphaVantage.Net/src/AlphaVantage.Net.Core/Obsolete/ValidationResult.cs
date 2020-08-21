using System;

// ReSharper disable once CheckNamespace
namespace AlphaVantage.Net.Core.Validation
{
    /// <summary>
    /// Result of API call validation
    /// </summary>
    
    [Obsolete("This class is from old version of AlfaVantage.Net library and will be removed in version 3.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    public class ValidationResult
    {
        /// <summary>
        /// Is request valid?
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// Error message
        /// </summary>
        public string? ErrorMsg { get; set; }
    }
}