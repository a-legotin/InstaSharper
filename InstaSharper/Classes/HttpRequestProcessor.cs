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
            var response = await  Client.SendAsync(requestMessage);
            LogHttpResponse(response);
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            LogHttpRequest(requestUri);
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            var response = await  Client.GetAsync(requestUri);
            LogHttpResponse(response);
            return response;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage,   HttpCompletionOption completionOption)
        {
            LogHttpRequest(requestMessage);  
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            var response = await Client.SendAsync(requestMessage, completionOption);
            LogHttpResponse(response);
            return response;
        }

        public async Task<string> SendAndGetJsonAsync(HttpRequestMessage requestMessage,  HttpCompletionOption completionOption)
        {     
            LogHttpRequest(requestMessage); 
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            var response = await Client.SendAsync(requestMessage, completionOption); 
            LogHttpResponse(response);   
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GeJsonAsync(Uri requestUri)
        {
            LogHttpRequest(requestUri);   
            if (_delay > TimeSpan.Zero)
                await Task.Delay(_delay);
            var response = await Client.GetAsync(requestUri);
            LogHttpResponse(response);     
            return await response.Content.ReadAsStringAsync();
        }

        private void LogHttpRequest(object request)
        {
          _logger?.OnRequest(request);
        }
        
        private void LogHttpResponse(object request)
        {
            _logger?.OnResponse(request);
        }
        
    }
}