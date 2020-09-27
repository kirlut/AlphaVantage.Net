using System.Collections.Generic;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;

namespace AlphaVantage.Net.Forex
{
    public class ForexTimeSeries
    {
        public Interval Interval { get; set; }
        
        public PhysicalCurrency FromSymbol { get; set; }
        
        public PhysicalCurrency ToSymbol { get; set; }
        
        public ICollection<ForexDataPoint> DataPoints {get; set;} = new List<ForexDataPoint>();
        
        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
    }
}