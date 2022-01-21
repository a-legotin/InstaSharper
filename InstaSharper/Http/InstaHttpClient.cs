using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Serialization;
using InstaSharper.API.Services;
using InstaSharper.Models.Request.User;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.System;
using InstaSharper.Models.Status;
using InstaSharper.Utils;
using LanguageExt;

namespace InstaSharper.Http
{
    internal class InstaHttpClient : IInstaHttpClient, IHttpClientState
    {
        private readonly IDevice _device;
        private readonly IAuthorizationHeaderProvider _authorizationHeaderProvider;
        private readonly HttpClientHandler _httpClientHandler;
        private readonly HttpClient _innerClient;
        private readonly ILogger _logger;
        private readonly IJsonSerializer _serializer;

        public InstaHttpClient(HttpClient innerClient,
            HttpClientHandler httpClientHandler,
            ILogger logger,
            IJsonSerializer serializer,
            IDevice device, 
            IAuthorizationHeaderProvider authorizationHeaderProvider)
        {
            _innerClient = innerClient;
            _httpClientHandler = httpClientHandler;
            _logger = logger;
            _serializer = serializer;
            _device = device;
            _authorizationHeaderProvider = authorizationHeaderProvider;
        }

        public CookieContainer GetCookieContainer() => _httpClientHandler.CookieContainer;

        public void SetCookies(CookieCollection cookies)
        {
            foreach (Cookie cookie in cookies)
            {
                _httpClientHandler.CookieContainer.Add(new Uri(Constants.BASE_URI), cookie);
            }
        }


        public async Task<Either<ResponseStatusBase, T>> SendAsync<T>(HttpRequestMessage requestMessage)
        {
            try
            {
                var responseMessage = await _innerClient.SendAsync(requestMessage);
                var json = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = _serializer.Deserialize<T>(json);
                    if (response is BaseStatusResponse statusResponse)
                        statusResponse.ResponseHeaders = responseMessage.Headers;
                    return response;
                }
                if (string.IsNullOrEmpty(json))
                    return ResponseStatus.FromStatusCode(responseMessage.StatusCode);
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

        public async Task<Either<ResponseStatusBase, T>> GetAsync<T>(Uri uri)
            => await GetAsync<T>(uri, default);

        public async Task<Either<ResponseStatusBase, T>> GetAsync<T>(Uri uri, GetRequestBase requestData)
        {
            try
            {
                var requestMessage = GetDefaultRequest(HttpMethod.Get, uri);
                if (requestData?.Headers?.Count > 0)
                {
                    foreach (var header in requestData.Headers)
                    {
                        requestMessage.Options.Set(new HttpRequestOptionsKey<string>(header.Key),
                            header.Value.ToString());
                    }
                }

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
            _innerClient.DefaultRequestHeaders.ConnectionClose = true;
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

        public void SetCredentials(ICredentials credentials)
        {
            _httpClientHandler.Credentials = credentials;
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
            foreach (var item in body)
            {
                defaultRequest.Options.Set(new HttpRequestOptionsKey<string>(item.Key), item.Value);
            }

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
            httpRequestMessage.Headers.Add("X-Pigeon-Session-Id", _device.PigeonSessionId.ToString());
            httpRequestMessage.Headers.Add("X-Pigeon-Rawclienttime", $"{DateTime.UtcNow.ToUnixTime()}.0{new Random(DateTime.Now.Millisecond).Next(10, 99)}");
            httpRequestMessage.Headers.Add("X-IG-Connection-Type", "WIFI");
            httpRequestMessage.Headers.Add("X-IG-Capabilities", "3brTvx8=");
            httpRequestMessage.Headers.Add("X-IG-App-ID", "567067343352427");
            httpRequestMessage.Headers.Add("User-Agent", _device.UserAgent);
            httpRequestMessage.Headers.Add("Accept-Language", "en_US");
            if (!string.IsNullOrEmpty(x_mid))
                httpRequestMessage.Headers.Add("X-MID", x_mid);
            httpRequestMessage.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            httpRequestMessage.Headers.Add("Host", "i.instagram.com");
            httpRequestMessage.Headers.Add("X-FB-HTTP-Engine", "Liger");
            httpRequestMessage.Headers.Add("X-FB-CLIENT-IP", "True");
            httpRequestMessage.Headers.Add("X-FB-SERVER-CLUSTER", "True");
            if(!string.IsNullOrEmpty(_authorizationHeaderProvider.AuthorizationHeader))
                httpRequestMessage.Headers.Add("Authorization", _authorizationHeaderProvider.AuthorizationHeader);
            if(!string.IsNullOrEmpty(_authorizationHeaderProvider.WwwClaimHeader))
                httpRequestMessage.Headers.Add(Constants.Headers.WWW_CLAIM, _authorizationHeaderProvider.WwwClaimHeader);
            else 
                httpRequestMessage.Headers.Add(Constants.Headers.WWW_CLAIM, "0");

            httpRequestMessage.Headers.Add(Constants.Headers.INTENDED_USER_ID, _authorizationHeaderProvider.CurrentUserIdHeader.ToString());
            if (_authorizationHeaderProvider.CurrentUserIdHeader > 0)
                httpRequestMessage.Headers.Add(Constants.Headers.DS_USER_ID, _authorizationHeaderProvider.CurrentUserIdHeader.ToString());
            
            if(!string.IsNullOrEmpty(_authorizationHeaderProvider.ShbId))
                httpRequestMessage.Headers.Add(Constants.Headers.IG_U_SHBID, _authorizationHeaderProvider.ShbId);
            if(!string.IsNullOrEmpty(_authorizationHeaderProvider.ShbTs))
                httpRequestMessage.Headers.Add(Constants.Headers.IG_U_SHBTS, _authorizationHeaderProvider.ShbTs);
            if(!string.IsNullOrEmpty(_authorizationHeaderProvider.Rur))
                httpRequestMessage.Headers.Add(Constants.Headers.IG_U_RUR, _authorizationHeaderProvider.Rur);
            if(!string.IsNullOrEmpty(_authorizationHeaderProvider.FbTripHeader))
                httpRequestMessage.Headers.Add(Constants.Headers.HEADER_X_FB_TRIP_ID, _authorizationHeaderProvider.FbTripHeader);
            return httpRequestMessage;
        }
    }
}