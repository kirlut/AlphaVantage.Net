using System.Threading.Tasks;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Forex;
using AlphaVantage.Net.Forex.Client;

namespace AlphaVantage.Net.Demo
{
    public partial class Demo
    {
        public static async Task ForexDemo()
        {
            // use your AlphaVantage API key
            string apiKey = "1";
            // there are 5 more constructors available
            using var client = new AlphaVantageClient(apiKey);
            using var forexClient = client.Forex();

            ForexTimeSeries forexTimeSeries = await forexClient.GetTimeSeriesAsync(
                PhysicalCurrency.USD, 
                PhysicalCurrency.ILS,
                Interval.Daily, 
                OutputSize.Compact);
            
            ForexExchangeRate forexExchangeRate = await forexClient.GetExchangeRateAsync(PhysicalCurrency.USD, PhysicalCurrency.ILS);
        }
    }
}