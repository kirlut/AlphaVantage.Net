using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Forex
{
    public class ForexTimeSeries : TimeSeriesBase<ForexDataPoint>
    {
        public PhysicalCurrency FromCurrency { get; set; }
        
        public PhysicalCurrency ToCurrency { get; set; }
    }
}