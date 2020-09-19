using System;
using System.Collections.Generic;

namespace AlphaVantage.Net.TechnicalIndicators
{
    public class TechIndicatorDataPoint
    {
        public DateTime Time {get; set;}
        
        public ICollection<TechIndicatorParameter> Parameters { get; set; } = new List<TechIndicatorParameter>();
    }
}