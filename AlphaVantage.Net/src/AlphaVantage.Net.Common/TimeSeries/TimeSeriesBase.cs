using System.Collections.Generic;
using AlphaVantage.Net.Common.Intervals;

namespace AlphaVantage.Net.Common.TimeSeries
{
    public abstract class TimeSeriesBase<TDataPoint>
    where TDataPoint: DataPointBase
    {
        public Interval Interval { get; set; }
        
        public ICollection<TDataPoint> DataPoints {get; set;} = new List<TDataPoint>();

        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
    }
}