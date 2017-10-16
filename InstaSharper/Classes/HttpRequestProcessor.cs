using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Logger;

namespace InstaSharper.Classes
{
    internal class HttpRequestProcessor : IHttpRequestProcessor
    {
        private readonly TimeSpan _delay;
        private readonly ILogger _logger;

        public HttpRequestProcessor(TimeSpan delay, HttpClient httpClient, HttpClientHandler httpHandler,
            ApiRequestMessage requestMessage, ILogger logger)
        {
            _delay = delay;
            Client = httpClient;
            HttpHandler = httpHandler;
            RequestMessage = requestMessage;
            _logger = logger;
        }

        public HttpClientHandler HttpHandler { get; }
        public ApiRequestMessage RequestMessage { get; }
        public HttpClient Client { get; }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            LogHttpRequest(requestMessage);
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            return await Client.SendAsync(requestMessage);
        }

        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            LogHttpGetRequest(requestUri);
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            return await Client.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage,
            HttpCompletionOption completionOption)
        {
            LogHttpRequest(requestMessage);
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            return await Client.SendAsync(requestMessage, completionOption);
        }

        public async Task<string> SendAndGetJsonAsync(HttpRequestMessage requestMessage,
            HttpCompletionOption completionOption)
        {
            LogHttpRequest(requestMessage);
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            var response = await Client.SendAsync(requestMessage, completionOption);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GeJsonAsync(Uri requestUri)
        {
            LogHttpGetRequest(requestUri);
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            var response = await Client.GetAsync(requestUri);
            return await response.Content.ReadAsStringAsync();
        }

        private async void LogHttpRequest(HttpRequestMessage requestMessage)
        {
            if (requestMessage == null || _logger == null) return;
            await _logger.WriteAsync(
                $"{requestMessage.Method}; URI: {requestMessage.RequestUri}; Delay: {_delay.TotalMilliseconds} ms; Data: {requestMessage} {Environment.NewLine}");
        }

        private async void LogHttpGetRequest(Uri requestUri)
        {
            if (requestUri == null || _logger == null) return;
            await _logger.WriteAsync(
                $"GET; URI: {requestUri}; Delay: {_delay.TotalMilliseconds} ms;{Environment.NewLine}");
        }
    }
}