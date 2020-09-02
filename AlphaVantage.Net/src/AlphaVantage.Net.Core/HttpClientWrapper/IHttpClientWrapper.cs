using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlphaVantage.Net.Core.HttpClientWrapper
{
    /// <summary>
    /// Implement this interface if you need to add custom logic or hooks in request sending.
    /// </summary>
    /// <remarks>
    /// Example of wrapper with custom logic: <see cref="HttpClientWithRateLimit"/>
    /// </remarks>
    public interface IHttpClientWrapper : IDisposable
    {
        void SetTimeOut(TimeSpan timeSpan);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
