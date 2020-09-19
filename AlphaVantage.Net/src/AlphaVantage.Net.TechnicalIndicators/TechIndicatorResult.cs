using System.Collections.Generic;
using AlphaVantage.Net.Core.Intervals;

namespace AlphaVantage.Net.TechnicalIndicators
{
    public class TechIndicatorResult
    {
        public TechIndicatorType IndicatorType { get; set; }
        
        public Interval Interval { get; set; }
        
        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
        
        public ICollection<TechIndicatorDataPoint> DataPoints {get; set;} = new List<TechIndicatorDataPoint>();
    }
}