using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Crypto;
using AlphaVantage.Net.Crypto.Client;

namespace AlphaVantage.Net.Demo
{
    public partial class Demo
    {
        public static async Task CryptoDemo()
        {
            // use your AlphaVantage API key
            string apiKey = "1";
            // there are 5 more constructors available
            using var client = new AlphaVantageClient(apiKey);
            using var cryptoClient = client.Crypto();

            CryptoTimeSeries cryptoTimeSeries =
                await cryptoClient.GetTimeSeriesAsync(DigitalCurrency.BTC, PhysicalCurrency.ILS, Interval.Weekly);

            CryptoRating cryptoRating = await cryptoClient.GetCryptoRatingAsync(DigitalCurrency.BTC);

            CryptoExchangeRate exchangeRate =
                await cryptoClient.GetExchangeRateAsync(DigitalCurrency.BTC, PhysicalCurrency.ILS);
        }
    }
}