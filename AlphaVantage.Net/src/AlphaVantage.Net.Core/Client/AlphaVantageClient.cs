using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.HttpClientWrapper;
using Microsoft.AspNetCore.WebUtilities;

namespace AlphaVantage.Net.Core.Client
{
    public partial class AlphaVantageClient : IDisposable
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly string _apiKey;

        public async Task<JsonDocument> RequestApiAsync(
            ApiFunction function,
            IDictionary<string, string>? query = null)
        {
            var request = ComposeHttpRequest(_apiKey, function, query);
            var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);

            var stream = await response.Content.ReadAsStreamAsync()
                .ConfigureAwait(false);
            var jsonDocument = await JsonDocument.ParseAsync(stream)
                .ConfigureAwait(false);

            AssertNotBadRequest(jsonDocument);
            
            return jsonDocument;
        }
        
        private HttpRequestMessage ComposeHttpRequest(string apiKey, ApiFunction function, IDictionary<string, string>? query)
        {
            var fullQueryDict = new Dictionary<string, string>(query ?? new Dictionary<string, string>(0))
            {
                {ApiConstants.ApiKeyQueryVar, apiKey}, {ApiConstants.FunctionQueryVar, function.ToString()}
            };

            var urlWithQueryString = QueryHelpers.AddQueryString(ApiConstants.AlfaVantageUrl, fullQueryDict);
            var urlWithQuery = new Uri(urlWithQueryString);

            var request = new HttpRequestMessage
            {
                RequestUri = urlWithQuery,
                Method = HttpMethod.Get
            };

            return request;
        }
        
        private void AssertNotBadRequest(JsonDocument jsonDocument)
        {
            if (jsonDocument.RootElement.TryGetProperty(ApiConstants.BadRequestToken, out var errorElement))
            {
                throw new AlphaVantageException(errorElement.GetString());
            }
        }
        
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}