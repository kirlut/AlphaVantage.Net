using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Exceptions;
using AlphaVantage.Net.Core.HttpClientWrapper;
using AlphaVantage.Net.Core.Validation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace AlphaVantage.Net.Core
{
    [Obsolete("This class is from the old version of AlfaVantage.Net library and will be removed in version 2.1. " +
              "Consider using classes from the latest version: https://github.com/LutsenkoKirill/AlphaVantage.Net")]
    public class AlphaVantageCoreClient
    {
        private readonly IApiCallValidator? _apiCallValidator;
        
        [CanBeNull]
        private readonly TimeSpan? _timeout;

        private static IHttpClientWrapper _clientWrapper = new HttpClientWithRateLimit(new HttpClient(), 20, 10);

        public AlphaVantageCoreClient(IApiCallValidator? apiCallValidator = null, TimeSpan? timeout = null)
        {
            _apiCallValidator = apiCallValidator;
            _timeout = timeout;
        }

        public virtual async Task<JObject> RequestApiAsync(string apiKey, ApiFunction function, IDictionary<string, string>? query = null)
        {
            AssertValid(function, query);

            if (_timeout.HasValue)
                _clientWrapper.SetTimeOut(_timeout.Value);
            
            var request = ComposeHttpRequest(apiKey, function, query);
            var response = await _clientWrapper.SendAsync(request);

            var jsonString = await response.Content.ReadAsStringAsync();
            var jObject = (JObject)JsonConvert.DeserializeObject(jsonString);
            
            AssertNotBadRequest(jObject);
            
            return jObject;
        }

        private HttpRequestMessage ComposeHttpRequest(string apiKey, ApiFunction function, IDictionary<string, string>? query)
        {
            var fullQueryDict = new Dictionary<string, string>(query ?? new Dictionary<string, string>(0));
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

        private void AssertValid(ApiFunction function, IDictionary<string, string>? query = null)
        {
            if(_apiCallValidator == null) return;

            var validationResult = _apiCallValidator.Validate(function, query);
            
            if(!validationResult.IsValid)
                throw new AlphaVantageException(validationResult.ErrorMsg ?? "");
        }
        
        private void AssertNotBadRequest(JObject jObject)
        {
            if(jObject.ContainsKey(ApiConstants.BadRequestToken))
                throw new AlphaVantageException(jObject[ApiConstants.BadRequestToken].ToString());
        }
    }
}