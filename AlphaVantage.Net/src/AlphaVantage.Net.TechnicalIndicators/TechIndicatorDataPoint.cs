using System.Collections.Generic;
using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.TechnicalIndicators
{
    public class TechIndicatorDataPoint : DataPointBase
    {
        public ICollection<TechIndicatorParameter> Parameters { get; set; } = new List<TechIndicatorParameter>();
    }
}