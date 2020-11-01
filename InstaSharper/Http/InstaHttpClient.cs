using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Serialization;
using InstaSharper.Models.Response.System;
using InstaSharper.Models.Status;
using InstaSharper.Utils;
using LanguageExt;

namespace InstaSharper.Http
{
    internal class InstaHttpClient : IInstaHttpClient
    {
        private readonly IDevice _device;
        private readonly HttpClientHandler _httpClientHandler;
        private readonly HttpClient _innerClient;
        private readonly ILogger _logger;
        private readonly ISerializer _serializer;

        public InstaHttpClient(HttpClient innerClient,
            HttpClientHandler httpClientHandler,
            ILogger logger,
            ISerializer serializer,
            IDevice device)
        {
            _innerClient = innerClient;
            _httpClientHandler = httpClientHandler;
            _logger = logger;
            _serializer = serializer;
            _device = device;
        }


        public async Task<Either<ResponseStatusBase, T>> SendAsync<T>(HttpRequestMessage requestMessage)
        {
            try
            {
                var responseMessage = await _innerClient.SendAsync(requestMessage);
                var json = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                    return _serializer.Deserialize<T>(json);
                return ResponseStatus.FromResponse(_serializer.Deserialize<BadStatusErrorsResponse>(json));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception);
                return ExceptionalResponseStatus.FromException(exception);
            }
        }

        public async Task<Either<ResponseStatusBase, T>> PostAsync<T, R>(Uri uri, R requestData)
        {
            try
            {
                var requestMessage = GetSignedRequest(HttpMethod.Post, uri, requestData);
                return await SendAsync<T>(requestMessage);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception);
                return ExceptionalResponseStatus.FromException(exception);
            }
        }

        public async Task<Either<ResponseStatusBase, HttpResponseMessage>> PostAsync<T>(Uri uri, T requestData)
        {
            try
            {
                var requestMessage = GetSignedRequest(HttpMethod.Post, uri, requestData);
                return await SendAsync(requestMessage);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception);
                return ExceptionalResponseStatus.FromException(exception);
            }
        }

        public async Task<Either<ResponseStatusBase, HttpResponseMessage>> SendAsync(HttpRequestMessage requestMessage)
        {
            _innerClient.DefaultRequestHeaders.ConnectionClose = false;
            try
            {
                var responseMessage = await _innerClient.SendAsync(requestMessage);
                var json = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                    return responseMessage;
                return ResponseStatus.FromResponse(_serializer.Deserialize<BadStatusErrorsResponse>(json));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception);
                return ExceptionalResponseStatus.FromException(exception);
            }
        }


        private HttpRequestMessage GetSignedRequest<T>(HttpMethod method,
            Uri uri,
            T data)
        {
            var body = new Dictionary<string, string>
            {
                [Constants.SIGNED_BODY] = $"SIGNATURE.{_serializer.Serialize(data)}",
                ["ig_sig_key_version"] = "5"
            };

            var defaultRequest = GetDefaultRequest(method, uri);
            defaultRequest.Content = new FormUrlEncodedContent(body);
            defaultRequest.Properties.Add(Constants.SIGNED_BODY, body);
            defaultRequest.Properties.Add("ig_sig_key_version", "4");
            return defaultRequest;
        }

        private HttpRequestMessage  GetDefaultRequest(
            HttpMethod method,
            Uri uri)
        {
            var httpRequestMessage = new HttpRequestMessage(method, uri);
            var cookies =
                _httpClientHandler.CookieContainer.GetCookies(_innerClient.BaseAddress);
            var x_mid = cookies["mid"]?.Value ?? string.Empty;
            var str2 = cookies["ds_user_id"]?.Value ?? string.Empty;
            var str3 = cookies["sessionid"]?.Value ?? string.Empty;
            var str4 = cookies["shbid"]?.Value ?? string.Empty;
            var str5 = cookies["shbts"]?.Value ?? string.Empty;
            var str6 = cookies["rur"]?.Value ?? string.Empty;
            var str7 = cookies["ig_direct_region_hint"]?.Value ?? string.Empty;
            httpRequestMessage.Headers.Add("X-IG-App-Locale", "en_US");
            httpRequestMessage.Headers.Add("X-IG-Device-Locale", "en_US");
            httpRequestMessage.Headers.Add("X-IG-Mapped-Locale", "en_US");
            httpRequestMessage.Headers.Add("X-IG-Connection-Speed", "-1kbps");
            httpRequestMessage.Headers.Add("X-IG-Bandwidth-Speed-KBPS", "-1.000");
            httpRequestMessage.Headers.Add("X-IG-Bandwidth-TotalBytes-B", "0");
            httpRequestMessage.Headers.Add("X-IG-Bandwidth-TotalTime-MS", "0");
            httpRequestMessage.Headers.Add("X-Bloks-Is-Layout-RTL", "false");
            httpRequestMessage.Headers.Add("X-Bloks-Enable-RenderCore", "false");
            httpRequestMessage.Headers.Add("X-IG-Device-ID", _device.DeviceId.ToString());
            httpRequestMessage.Headers.Add("X-IG-Android-ID", _device.AndroidId);
            httpRequestMessage.Headers.Add("X-IG-Connection-Type", "WIFI");
            httpRequestMessage.Headers.Add("X-IG-Capabilities", "3brTvx8=");
            httpRequestMessage.Headers.Add("X-IG-App-ID", "567067343352427");
            httpRequestMessage.Headers.Add("User-Agent", _device.UserAgent);
            httpRequestMessage.Headers.Add("Accept-Language", "en_US");
            if (!string.IsNullOrEmpty(x_mid))
                httpRequestMessage.Headers.Add("X-MID", x_mid);
            httpRequestMessage.Headers.Add("IG-U-RUR", str6);
            httpRequestMessage.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            httpRequestMessage.Headers.Add("Host", "i.instagram.com");
            httpRequestMessage.Headers.Add("X-FB-HTTP-Engine", "Liger");
            return httpRequestMessage;
        }
    }
}