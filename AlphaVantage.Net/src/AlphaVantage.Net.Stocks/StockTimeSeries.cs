using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Stocks
{
    public class StockTimeSeries : TimeSeriesBase<StockDataPoint>
    {
        public bool IsAdjusted { get; set; }
    }
}