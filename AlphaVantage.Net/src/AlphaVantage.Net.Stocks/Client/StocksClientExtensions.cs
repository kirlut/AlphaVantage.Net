using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Stocks.Parsing;
using AlphaVantage.Net.Stocks.TimeSeries;
using AlphaVantage.Net.Stocks.Utils;

namespace AlphaVantage.Net.Stocks.Client
{
    public static class StocksClientExtensions
    {
        private static readonly GlobalQuoteParser GlobalQuoteParser = new GlobalQuoteParser();
        private static readonly SearchResultParser SearchResultParser = new SearchResultParser();
        
        private const string SymbolQueryVar = "symbol";
        private const string IntradayIntervalQueryVar = "interval";
        private const string OutputSizeQueryVar = "outputsize";
        
        /// <summary>
        /// Return stocks time series for requested symbol 
        /// </summary>
        /// <param name="stocksClient"></param>
        /// <param name="symbol"></param>
        /// <param name="seriesType"></param>
        /// <param name="size"></param>
        /// <param name="isAdjusted"></param>
        /// <returns></returns>
        public static async Task<StockTimeSeries> GetTimeSeriesAsync(this StocksClient stocksClient, 
            string symbol, 
            TimeSeriesType seriesType,
            TimeSeriesSize size,
            bool isAdjusted = false)
        {
#pragma warning disable 618
            if(seriesType == TimeSeriesType.Intraday ) throw new AlphaVantageException(
                "Please consider using GetIntradayTimeSeriesAsync() method for Intraday time series");
#pragma warning restore 618
            
            var parser = new StockTimeSeriesParser(seriesType, isAdjusted);
            
            var query = new Dictionary<string, string>()
            {
                {SymbolQueryVar, symbol},
                {OutputSizeQueryVar, size.ConvertToString()}
            };

            var function = seriesType.ConvertToApiFunction(isAdjusted);
            
            return await stocksClient.RequestApiAsync(parser, function, query);
        }
        
        /// <summary>
        /// Return intraday stocks time series for requested symbol
        /// </summary>
        /// <param name="stocksClient"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static async Task<IntradayTimeSeries> GetIntradayTimeSeriesAsync(this StocksClient stocksClient, 
            string symbol, 
            IntradayInterval interval,
            TimeSeriesSize size)
        {
            var parser = new IntradayTimeSeriesParser(interval);
            
            var query = new Dictionary<string, string>()
            {
                {SymbolQueryVar, symbol},
                {IntradayIntervalQueryVar, interval.ConvertToString()},
                {OutputSizeQueryVar, size.ConvertToString()}
            };
            
            return await stocksClient.RequestApiAsync(parser, ApiFunction.TIME_SERIES_INTRADAY, query);
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
            var query = new Dictionary<string, string>(){{SymbolQueryVar, symbol}};
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