using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlphaVantage.Net.Core.InternalHttpClient
{
    internal interface IHttpClient
    {
        void SetTimeOut(TimeSpan timeSpan);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
