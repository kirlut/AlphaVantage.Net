using System.Collections.Generic;

namespace AlphaVantage.Net.Stocks.TimeSeries
{
    public abstract class TimeSeriesBase
    {
        public ICollection<StockDataPoint> DataPoints {get; set;} = new List<StockDataPoint>();

        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
    }
}