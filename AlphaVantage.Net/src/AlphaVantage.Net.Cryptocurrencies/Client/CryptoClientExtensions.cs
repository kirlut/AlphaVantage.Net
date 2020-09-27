using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Common;
using AlphaVantage.Net.Common.Currencies;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Parsing;
using AlphaVantage.Net.Cryptocurrencies.Parsing;

namespace AlphaVantage.Net.Cryptocurrencies.Client
{
    public static class CryptoClientExtensions
    {
        private static readonly WrappedValueParser<CryptoExchangeRate> ExchangeRateParser =
            new WrappedValueParser<CryptoExchangeRate>("Realtime Currency Exchange Rate");
        
        private static readonly WrappedValueParser<CryptoRating> CryptoRatingParser =
            new WrappedValueParser<CryptoRating>("Crypto Rating (FCAS)");

        /// <summary>
        /// Returns exchange rate for requested currencies pair
        /// </summary>
        /// <param name="cryptoClient"></param>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <returns></returns>
        public static async Task<CryptoExchangeRate> GetExchangeRatesAsync(this CryptoClient cryptoClient,
            DigitalCurrency fromCurrency, PhysicalCurrency toCurrency)
        {
            var query = new Dictionary<string, string>()
            {
                {ApiQueryConstants.FromCurrencyQueryVar, fromCurrency.ToString().Replace("_", "")},
                {ApiQueryConstants.ToCurrencyQueryVar, toCurrency.ToString()}
            };

            return await cryptoClient.RequestApiAsync(ExchangeRateParser, ApiFunction.CURRENCY_EXCHANGE_RATE, query);
        }

        public static async Task<CryptoRating> GetCryptoRatingAsync(this CryptoClient cryptoClient,
            DigitalCurrency currency)
        {
            var query = new Dictionary<string, string>()
            {
                {ApiQueryConstants.SymbolQueryVar, currency.ToString().Replace("_", "")},
            };

            return await cryptoClient.RequestApiAsync(CryptoRatingParser, ApiFunction.CRYPTO_RATING, query);
        }
        

        /// <summary>
        /// Returns cryptocurrency time series for requested currencies pair
        /// </summary>
        /// <param name="cryptoClient"></param>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static async Task<CryptoTimeSeries> GetTimeSeriesAsync(this CryptoClient cryptoClient,
            DigitalCurrency fromCurrency, 
            PhysicalCurrency toCurrency,
            Interval interval)
        {
            var parser = new CryptoTimeSeriesParser(interval, fromCurrency, toCurrency);
            
            var function = interval.ConvertToApiFunction();
            
            var query = new Dictionary<string, string>()
            {
                {ApiQueryConstants.SymbolQueryVar, fromCurrency.ToString().Replace("_", "")},
                {"market", toCurrency.ToString()}
            };

            return await cryptoClient.RequestApiAsync(parser, function, query);
        }
    }
}