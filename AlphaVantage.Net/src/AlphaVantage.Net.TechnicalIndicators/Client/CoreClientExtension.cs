using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Intervals;
using AlphaVantage.Net.TechnicalIndicators.Parsing;

namespace AlphaVantage.Net.TechnicalIndicators.Client
{
    public static class CoreClientExtension
    {
        private static readonly TechIndicatorsResultParser Parser = new TechIndicatorsResultParser();
        
        public static async Task<TechIndicatorResult> GetTechnicalIndicatorAsync(this AlphaVantageClient client,
            string symbol,
            TechIndicatorType type, 
            Interval interval, 
            Dictionary<string, string> query)
        {
            if (type == TechIndicatorType.VWAP && interval.IsIntraday() == false)
            {
                throw new AlphaVantageException("VWAP support only intraday intervals: 1min, 5min, 15min, 30min, 60min");
            }
            
            query.Add(ApiConstants.IntervalQueryVar, interval.ConvertToString());
            var function = type.ToApiFunction();
            
            var typedClient = new TechnicalIndicatorsClient(client);
            var result = await typedClient.RequestApiAsync(Parser, function, query);

            result.IndicatorType = type;
            result.Interval = interval;
            
            return result;
        }
    }
}