using System.Collections.Generic;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.TimeSeries;

namespace AlphaVantage.Net.Cryptocurrencies
{
    public class CryptoTimeSeries : TimeSeriesBase<CryptoDataPoint>
    {
        public DigitalCurrency FromSymbol { get; set; }
        
        public PhysicalCurrency ToSymbol { get; set; }
    }
}