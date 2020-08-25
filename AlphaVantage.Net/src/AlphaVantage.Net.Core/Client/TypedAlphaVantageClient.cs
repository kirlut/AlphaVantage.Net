using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.HttpClientWrapper;
using AlphaVantage.Net.Core.Parsing;

namespace AlphaVantage.Net.Core.Client
{
    public abstract class TypedAlphaVantageClient : IDisposable
    {
        private readonly AlphaVantageClient _alphaVantageClient;

        internal TypedAlphaVantageClient(AlphaVantageClient alphaVantageClient)
        {
            _alphaVantageClient = alphaVantageClient;
        }

        internal async Task<T> RequestApiAsync<T>(
            IAlphaVantageJsonDocumentParser<T> parser,
            ApiFunction function,
            IDictionary<string, string>? query = null)
        {
            var jsonDocument = await _alphaVantageClient.RequestParsedJsonAsync(function, query, true);
            var result = parser.ParseApiResponse(jsonDocument);

            return result;
        }

        internal async Task<T> RequestApiAsync<T>(
            IAlphaVantageJsonParser<T> parser,
            ApiFunction function,
            IDictionary<string, string>? query = null)
        {
            var jsonString = await _alphaVantageClient.RequestPureJsonAsync(function, query, true);
            var result = parser.ParseApiResponse(jsonString);

            return result;
        }

        public void Dispose()
        {
            _alphaVantageClient.Dispose();
        }
    }
}