using System;
using AlphaVantage.Net.Core.HttpClientWrapper;

namespace AlphaVantage.Net.Core
{
    public partial class AlphaVantageClient : IDisposable
    {
        private readonly IHttpClientWrapper _httpClient;

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}