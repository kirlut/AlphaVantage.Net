using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;

namespace AlphaVantage.Net.Forex.Client
{
    public static class ForexClientExtensions
    {
        public static async Task<ForexExchangeRate> GetExchangeRatesAsync(PhysicalCurrency fromCurrency, PhysicalCurrency toCurrency)
        {
            return await Task.FromResult(new ForexExchangeRate());
        }
        
        public static async Task<ForexTimeSeries> GetTimeSeriesAsync(
            PhysicalCurrency fromCurrency, 
            PhysicalCurrency toCurrency,
            Interval interval,
            OutputSize outputSize = OutputSize.Compact)
        {
            return await Task.FromResult(new ForexTimeSeries());
        }
    }
}