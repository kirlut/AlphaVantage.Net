using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.HttpClientWrapper;

namespace AlphaVantage.Net.Core.Client
{
    public class TypedAlphaVantageClient : IDisposable
    {
        private readonly AlphaVantageClient _alphaVantageClient;

        public TypedAlphaVantageClient(AlphaVantageClient alphaVantageClient)
        {
            _alphaVantageClient = alphaVantageClient;
        }

        public async Task<T> RequestApiAsync<T>(
            IAlphaVantageParser<T> parser,
            ApiFunction function,
            IDictionary<string, string>? query = null)
        {
            var jsonDocument = await _alphaVantageClient.RequestApiAsync(function, query);
            var result = parser.ParseApiResponse(jsonDocument);

            return result;
        }

        public void Dispose()
        {
            _alphaVantageClient.Dispose();
        }
    }
}