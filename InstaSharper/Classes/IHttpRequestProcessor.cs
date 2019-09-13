﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Classes.Android.DeviceInfo;

namespace InstaSharper.Classes
{
    public interface IHttpRequestProcessor
    {
        HttpClientHandler HttpHandler { get; }
        ApiRequestMessage RequestMessage { get; }
        HttpClient Client { get; }
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage);
        Task<HttpResponseMessage> GetAsync(Uri requestUri);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption);
        Task<string> SendAndGetJsonAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption);
        Task<string> GeJsonAsync(Uri requestUri);
    }
}