using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;

namespace AlphaVantage.Net.Cryptocurrencies.Crypto
{
    public static class CryptoClientExtensions
    {
        public static async Task<CryptoExchangeRate> GetExchangeRatesAsync(DigitalCurrency fromCurrency, PhysicalCurrency toCurrency)
        {
            return await Task.FromResult(new CryptoExchangeRate());
        }
        
        public static async Task<CryptoTimeSeries> GetTimeSeriesAsync(
            PhysicalCurrency fromCurrency, 
            PhysicalCurrency toCurrency,
            Interval interval)
        {
            return await Task.FromResult(new CryptoTimeSeries());
        }
    }
}