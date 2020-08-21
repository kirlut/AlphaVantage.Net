using System;
using System.Net.Http;
using AlphaVantage.Net.Core.HttpClientWrapper;

namespace AlphaVantage.Net.Core
{
    public partial class AlphaVantageClient
    {
        /// <summary>
        /// Create new instance of Alpha Vantage client with
        /// default <see cref="HttpClient"/> wrapped in <see cref="DefaultHttpClientWrapper"/>
        /// </summary>
        public AlphaVantageClient()
        {
            _httpClient = new DefaultHttpClientWrapper(new HttpClient());
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with
        /// <see cref="HttpClient"/> with timeout = <paramref name="timeout"/>
        /// wrapped in <see cref="DefaultHttpClientWrapper"/> 
        /// </summary>
        /// <param name="timeout"></param>
        public AlphaVantageClient(TimeSpan timeout)
        {
            _httpClient = new DefaultHttpClientWrapper(new HttpClient());
            _httpClient.SetTimeOut(timeout);
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with <paramref name="httpClient"/> 
        /// wrapped in <see cref="DefaultHttpClientWrapper"/>
        /// </summary>
        /// <param name="httpClient"></param>
        public AlphaVantageClient(HttpClient httpClient)
        {
            _httpClient = new DefaultHttpClientWrapper(httpClient);
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with <paramref name="httpClientWrapper"/> 
        /// </summary>
        /// <param name="httpClientWrapper"></param>
        public AlphaVantageClient(IHttpClientWrapper httpClientWrapper)
        {
            _httpClient = httpClientWrapper;
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with HttpClientWrapper
        /// returned by <paramref name="httpClientWrapperFactory"/> 
        /// </summary>
        /// <param name="httpClientWrapperFactory"></param>
        public AlphaVantageClient(Func<IHttpClientWrapper> httpClientWrapperFactory)
        {
            _httpClient = httpClientWrapperFactory.Invoke();
        }

        /// <summary>
        /// Create new instance of Alpha Vantage client with
        /// <see cref="HttpClient"/> returned by <paramref name="httpClientFactory"/> 
        /// and wrapped in <see cref="DefaultHttpClientWrapper"/>
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public AlphaVantageClient(Func<HttpClient> httpClientFactory)
        {
            _httpClient = new DefaultHttpClientWrapper(httpClientFactory.Invoke());
        }
    }
}