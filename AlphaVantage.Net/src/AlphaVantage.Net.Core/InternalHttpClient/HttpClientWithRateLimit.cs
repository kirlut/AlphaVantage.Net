using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaVantage.Net.Core.InternalHttpClient
{
    /// <summary>
    /// Only requests passing thru given instance of the client are throttled.
    /// Two different instances of the client may have totally different rate limites.
    /// </summary>
    internal class HttpClientWithRateLimit : IHttpClient, IDisposable
    {
        private HttpClient _client;
        private TimeSpan _minRequestInterval;//Calculated based on rpm limit in constructor
        private Semaphore _concurrentRequestsCounter;
        private DateTime _previousRequestStartTime;
        private Object _lockObject = new Object();
        
        internal HttpClientWithRateLimit(HttpClient client, int maxRequestPerMinutes, int maxConcurrentRequests)
        {
            _client = client;
            _minRequestInterval = new TimeSpan(0, 0, 0, 0, 60000 / maxRequestPerMinutes);
            _concurrentRequestsCounter = new Semaphore(maxConcurrentRequests, maxConcurrentRequests);
            _previousRequestStartTime = DateTime.MinValue;
        }
        public void Dispose()
        {
            _concurrentRequestsCounter.Dispose();
        }
        
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            HttpResponseMessage? response = null;
            _concurrentRequestsCounter.WaitOne();
            await WaitForRequestedMinimumInterval();
            try
            {
                response = await _client.SendAsync(request);
            }
            finally
            {
                _concurrentRequestsCounter.Release();
            }
            
            return response;
        }
        
        private async Task WaitForRequestedMinimumInterval()
        {
            TimeSpan? delayInterval = null;
            lock (_lockObject)
            {
                var timeSinceLastRequest = DateTime.Now - _previousRequestStartTime;
                if (timeSinceLastRequest < _minRequestInterval)
                {
                    delayInterval = _minRequestInterval - timeSinceLastRequest;
                }
                _previousRequestStartTime = DateTime.Now;
                if (delayInterval.HasValue)
                {
                    _previousRequestStartTime.AddMilliseconds(delayInterval.Value.Milliseconds);
                }
            }
            
            if (delayInterval.HasValue)
            {
                await Task.Delay(delayInterval.Value.Milliseconds);
            }
        }
        
        public void SetTimeOut(TimeSpan timeSpan)
        {
            _client.Timeout = timeSpan;
        }
    }
}
