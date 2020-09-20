using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Intervals;
using AlphaVantage.Net.TechnicalIndicators.Parsing;
using JetBrains.Annotations;

namespace AlphaVantage.Net.TechnicalIndicators.Client
{
    public static class CoreClientExtension
    {
        private static readonly TechIndicatorsResultParser Parser = new TechIndicatorsResultParser();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol"></param>
        /// <param name="indicatorType"></param>
        /// <param name="interval"></param>
        /// <param name="additionalParameters"></param>
        /// <returns></returns>
        /// <exception cref="AlphaVantageException"></exception>
        public static async Task<TechIndicatorResult> GetTechnicalIndicatorAsync(this AlphaVantageClient client,
            string symbol,
            TechIndicatorType indicatorType, 
            Interval interval,
            Dictionary<string, string>? additionalParameters = null)
        {
            if (indicatorType == TechIndicatorType.VWAP && interval.IsIntraday() == false)
            {
                throw new AlphaVantageException("VWAP support only intraday intervals: 1min, 5min, 15min, 30min, 60min");
            }

            var query = additionalParameters ?? new Dictionary<string, string>();
            
            query.Add(ApiConstants.IntervalQueryVar, interval.ConvertToString());
            query.Add(ApiConstants.SymbolQueryVar, symbol);
            var function = indicatorType.ToApiFunction();
            
            var typedClient = new TechnicalIndicatorsClient(client);
            var result = await typedClient.RequestApiAsync(Parser, function, query);

            result.IndicatorType = indicatorType;
            result.Interval = interval;
            
            return result;
        }
    }
}