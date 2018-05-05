using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.Validation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlphaVantage.Net.Core
{
    public class AlphaVantageCoreClient
    {
        [CanBeNull]
        private readonly IApiCallValidator _apiCallValidator;
        
        [CanBeNull]
        private readonly TimeSpan? _timeout;

        public AlphaVantageCoreClient(IApiCallValidator apiCallValidator = null, TimeSpan? timeout = null)
        {
            _apiCallValidator = apiCallValidator;
            _timeout = timeout;
        }

        public async Task<JObject> RequestApiAsync(string apiKey, ApiFunction function, IDictionary<string, string> query = null)
        {
            AssertValid(apiKey, function, query);

            var client = new HttpClient();
            if (_timeout.HasValue)
                client.Timeout = _timeout.Value;
            
            var request = ComposeHttpRequest(apiKey, function, query);
            var response = await client.SendAsync(request);

            var jsonString = await response.Content.ReadAsStringAsync();
            var jObject = (JObject)JsonConvert.DeserializeObject(jsonString);
            
            AssertNotBadRequest(jObject);
            
            return jObject;
        }

        private HttpRequestMessage ComposeHttpRequest(string apiKey, ApiFunction function, IDictionary<string, string> query)
        {
            var fullQueryDict = new Dictionary<string, string>(query);
            fullQueryDict.Add(ApiConstants.ApiKeyQueryVar, apiKey);
            fullQueryDict.Add(ApiConstants.FunctionQueryVar, function.ToString());
            
            var urlWithQueryString = QueryHelpers.AddQueryString(ApiConstants.AlfaVantageUrl, fullQueryDict);
            var urlWithQuery = new Uri(urlWithQueryString);

            var request = new HttpRequestMessage
            {
                RequestUri = urlWithQuery,
                Method = HttpMethod.Get
            };

            return request;
        }

        private void AssertValid(string apiKey, ApiFunction function, IDictionary<string, string> query = null)
        {
            if(_apiCallValidator == null) return;

            var validationResult = _apiCallValidator.Validate(apiKey, function, query);
            
            if(!validationResult.IsValid)
                throw new AlphaVantageException(validationResult.ErrorMsg);
        }
        
        private void AssertNotBadRequest(JObject jObject)
        {
            if(jObject.ContainsKey(ApiConstants.BadRequestToken))
                throw new AlphaVantageException(jObject[ApiConstants.BadRequestToken].ToString());
        }
    }
}