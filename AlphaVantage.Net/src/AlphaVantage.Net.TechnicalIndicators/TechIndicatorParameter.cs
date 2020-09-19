using System;

namespace AlphaVantage.Net.TechnicalIndicators
{
    public class TechIndicatorParameter
    {
        public string ParameterName { get; set; } = String.Empty;
        
        public decimal ParameterValue { get; set; }
    }
}