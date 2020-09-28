using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Forex.Parsing;

namespace AlphaVantage.Net.Forex.Client
{
    public static class ForexClientExtensions
    {
        private static readonly WrappedValueParser<ForexExchangeRate> ExchangeRateParser = 
            new WrappedValueParser<ForexExchangeRate>("Realtime Currency Exchange Rate");
        
        /// <summary>
        /// Returns exchange rate for a given currencies pair
        /// </summary>
        /// <param name="forexClient"></param>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <returns></returns>
        public static async Task<ForexExchangeRate> GetExchangeRateAsync(this ForexClient forexClient, 
            PhysicalCurrency fromCurrency, PhysicalCurrency toCurrency)
        {
            var query = new Dictionary<string, string>()
            {
                {ApiQueryConstants.FromCurrencyQueryVar, fromCurrency.ToString()},
                {ApiQueryConstants.ToCurrencyQueryVar, toCurrency.ToString()}
            };

            return await forexClient.RequestApiAsync(ExchangeRateParser, ApiFunction.CURRENCY_EXCHANGE_RATE, query);        
        }

        /// <summary>
        /// Returns Forex time series for a given currencies pair
        /// </summary>
        /// <param name="forexClient"></param>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <param name="interval"></param>
        /// <param name="outputSize"></param>
        /// <returns></returns>
        public static async Task<ForexTimeSeries> GetTimeSeriesAsync(this ForexClient forexClient,
            PhysicalCurrency fromCurrency, 
            PhysicalCurrency toCurrency,
            Interval interval,
            OutputSize outputSize = OutputSize.Compact)
        {
            var parser = new ForexTimeSeriesParser(interval, fromCurrency, toCurrency);
            
            var query = new Dictionary<string, string>()
            {
                {ApiQueryConstants.FromSymbolQueryVar, fromCurrency.ToString()},
                {ApiQueryConstants.ToSymbolQueryVar, toCurrency.ToString()},
            };

            var function = interval.ConvertToApiFunction();
            
            if (function == ApiFunction.FX_INTRADAY)
            {
                query.Add(ApiQueryConstants.IntervalQueryVar, interval.ConvertToQueryString());
            }
            
            if (function == ApiFunction.FX_INTRADAY || function == ApiFunction.FX_DAILY)
            {
                query.Add(ApiQueryConstants.OutputSizeQueryVar, outputSize.ConvertToQueryString());
            }
            
            return await forexClient.RequestApiAsync(parser, function, query);
        }
    }
}