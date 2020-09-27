using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Forex
{
    public class ForexTimeSeries : TimeSeriesBase<ForexDataPoint>
    {
        public PhysicalCurrency FromSymbol { get; set; }
        
        public PhysicalCurrency ToSymbol { get; set; }
    }
}