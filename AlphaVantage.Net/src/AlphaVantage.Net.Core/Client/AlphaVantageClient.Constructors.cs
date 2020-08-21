using System;
using System.Net.Http;
using AlphaVantage.Net.Core.HttpClientWrapper;

namespace AlphaVantage.Net.Core.Client
{
    public partial class AlphaVantageClient
    {
        /// <summary>
        /// Create new instance of Alpha Vantage client with
        /// default <see cref="HttpClient"/> wrapped in <see cref="DefaultHttpClientWrapper"/>
        /// </summary>
        public AlphaVantageClient(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new DefaultHttpClientWrapper(new HttpClient());
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with
        /// <see cref="HttpClient"/> with timeout = <paramref name="timeout"/>
        /// wrapped in <see cref="DefaultHttpClientWrapper"/> 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="timeout"></param>
        public AlphaVantageClient(string apiKey, TimeSpan timeout)
        {
            _apiKey = apiKey;

            _httpClient = new DefaultHttpClientWrapper(new HttpClient());
            _httpClient.SetTimeOut(timeout);
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with <paramref name="httpClient"/> 
        /// wrapped in <see cref="DefaultHttpClientWrapper"/>
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="httpClient"></param>
        public AlphaVantageClient(string apiKey, HttpClient httpClient)
        {
            _apiKey = apiKey;
            _httpClient = new DefaultHttpClientWrapper(httpClient);
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with <paramref name="httpClientWrapper"/> 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="httpClientWrapper"></param>
        public AlphaVantageClient(string apiKey, IHttpClientWrapper httpClientWrapper)
        {
            _apiKey = apiKey;
            _httpClient = httpClientWrapper;
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with HttpClientWrapper
        /// returned by <paramref name="httpClientWrapperFactory"/> 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="httpClientWrapperFactory"></param>
        public AlphaVantageClient(string apiKey, Func<IHttpClientWrapper> httpClientWrapperFactory)
        {
            _apiKey = apiKey;
            _httpClient = httpClientWrapperFactory.Invoke();
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with
        /// <see cref="HttpClient"/> returned by <paramref name="httpClientFactory"/> 
        /// and wrapped in <see cref="DefaultHttpClientWrapper"/>
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="httpClientFactory"></param>
        public AlphaVantageClient(string apiKey, Func<HttpClient> httpClientFactory)
        {
            _apiKey = apiKey;
            _httpClient = new DefaultHttpClientWrapper(httpClientFactory.Invoke());
        }
    }
}