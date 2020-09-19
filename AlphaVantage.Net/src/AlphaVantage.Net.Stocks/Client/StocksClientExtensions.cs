using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Intervals;
using AlphaVantage.Net.Stocks.Parsing;
using AlphaVantage.Net.Stocks.TimeSeries;
using AlphaVantage.Net.Stocks.Utils;

namespace AlphaVantage.Net.Stocks.Client
{
    public static class StocksClientExtensions
    {
        private static readonly GlobalQuoteParser GlobalQuoteParser = new GlobalQuoteParser();
        private static readonly SearchResultParser SearchResultParser = new SearchResultParser();
        
        private const string OutputSizeQueryVar = "outputsize";
        
        /// <summary>
        /// Return stocks time series for requested symbol 
        /// </summary>
        /// <param name="stocksClient"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="size"></param>
        /// <param name="isAdjusted"></param>
        /// <returns></returns>
        public static async Task<StockTimeSeries> GetTimeSeriesAsync(this StocksClient stocksClient, 
            string symbol, 
            Interval interval,
            TimeSeriesSize size,
            bool isAdjusted = false)
        {
            var parser = new TimeSeriesParser(interval, isAdjusted);
            
            var query = new Dictionary<string, string>()
            {
                {ApiConstants.SymbolQueryVar, symbol},
                {OutputSizeQueryVar, size.ConvertToString()}
            };

            var function = interval.ConvertToApiFunction(isAdjusted);
            if (function == ApiFunction.TIME_SERIES_INTRADAY)
            {
                query.Add(ApiConstants.IntervalQueryVar, interval.ConvertToString());
            }
            
            return await stocksClient.RequestApiAsync(parser, function, query);
        }

        /// <summary>
        /// Returns the price and volume information for a symbol
        /// </summary>
        /// <param name="stocksClient"></param>
        /// <param name="symbol"></param>
        /// <remarks>
        /// function=GLOBAL_QUOTE
        /// </remarks>
        public static async Task<GlobalQuote?> GetGlobalQuoteAsync(this StocksClient stocksClient, string symbol)
        {
            var query = new Dictionary<string, string>(){{ApiConstants.SymbolQueryVar, symbol}};
            return await stocksClient.RequestApiAsync(GlobalQuoteParser, ApiFunction.GLOBAL_QUOTE, query);
        }
        
        /// <summary>
        /// Returns the best-matching symbols and market information based on keywords
        /// </summary>
        /// <param name="stocksClient"></param>
        /// <param name="keyWords"></param>
        /// <remarks>
        /// function=SYMBOL_SEARCH
        /// </remarks>
        public static async Task<ICollection<SymbolSearchMatch>> SearchSymbolAsync(this StocksClient stocksClient, string keyWords)
        {
            var query = new Dictionary<string, string>(){{"keywords", keyWords}};
            return await stocksClient.RequestApiAsync(SearchResultParser, ApiFunction.SYMBOL_SEARCH, query);
        }
    }
}