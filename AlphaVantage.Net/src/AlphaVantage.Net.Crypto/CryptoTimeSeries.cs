using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Crypto
{
    public class CryptoTimeSeries : TimeSeriesBase<CryptoDataPoint>
    {
        public DigitalCurrency FromCurrency { get; set; }
        
        public PhysicalCurrency ToCurrency { get; set; }
    }
}