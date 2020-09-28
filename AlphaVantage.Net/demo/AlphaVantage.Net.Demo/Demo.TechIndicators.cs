using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.TechnicalIndicators;
using AlphaVantage.Net.TechnicalIndicators.Client;

namespace AlphaVantage.Net.Demo
{
    public partial class Demo
    {
        public static async Task TechIndicatorsDemo()
        {
            // use your AlphaVantage API key
            string apiKey = "1";
            // there are 5 more constructors available
            using var client = new AlphaVantageClient(apiKey);

            var symbol = "IBM";
            var indicatorType = TechIndicatorType.SMA;
            var query = new Dictionary<string, string>()
            {
                {"time_period", "20"},
                {"series_type", "close"}
            };

            TechIndicatorTimeSeries result = await client.GetTechIndicatorTimeSeriesAsync(symbol, indicatorType, Interval.Min15, query);
        }
    }
}