namespace AlphaVantage.Net.Core.Validation
{
    /// <summary>
    /// Result of API call validation
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Is request valid?
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}