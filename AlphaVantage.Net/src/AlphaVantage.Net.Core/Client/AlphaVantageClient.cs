using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.HttpClientWrapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.WebUtilities;

namespace AlphaVantage.Net.Core.Client
{
    [UsedImplicitly]
    public partial class AlphaVantageClient : IDisposable
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly string _apiKey;

        public async Task<JsonDocument> RequestParsedJsonAsync(
            ApiFunction function,
            IDictionary<string, string>? query = null,
            bool cleanJsonFromSequenceNumbers = false)
        {
            var jsonString = await RequestPureJsonAsync(function, query, cleanJsonFromSequenceNumbers)
                .ConfigureAwait(false);
            
            var jsonDocument = JsonDocument.Parse(jsonString);

            return jsonDocument;
        }

        public async Task<string> RequestPureJsonAsync(
            ApiFunction function,
            IDictionary<string, string>? query = null,
            bool cleanJsonFromSequenceNumbers = false)
        {
            var jsonString = await RequestApiAsync(_apiKey, function, query)
                .ConfigureAwait(false);

            AssertNotBadRequest(jsonString);

            if (cleanJsonFromSequenceNumbers)
            {
                jsonString = CleanJsonFromSequenceNumbers(jsonString);
            }
            
            return jsonString;
        }

        private async Task<string> RequestApiAsync(string apiKey, ApiFunction function, 
            IDictionary<string, string>? query)
        {
            var request = ComposeHttpRequest(_apiKey, function, query);
            
            var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);
            var jsonString = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
            
            return jsonString;
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

        private string CleanJsonFromSequenceNumbers(string jsonString)
        {
            // "(\d+)[a-z]?\.\s
            return Regex.Replace(jsonString, "\"(\\d+)[a-z]?\\.\\s", "\"", RegexOptions.Multiline);
        }
        
        private void AssertNotBadRequest(string jsonString)
        {
            if (jsonString.Contains(ApiConstants.BadRequestToken))
            {
                throw new AlphaVantageException(jsonString);
            }
        }
        
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}