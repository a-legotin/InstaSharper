using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Logging;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Abstractions.Serialization;
using InstaSharper.Abstractions.Utils;
using InstaSharper.Infrastructure;
using InstaSharper.Models.Request.User;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.System;
using InstaSharper.Models.Status;
using InstaSharper.Utils;
using LanguageExt;

namespace InstaSharper.Http;

internal class InstaHttpClient : IInstaHttpClient, IHttpClientState
{
    private readonly IAuthorizationHeaderProvider _authorizationHeaderProvider;
    private readonly IDevice _device;
    private readonly HttpClientHandler _httpClientHandler;
    private readonly HttpClient _innerClient;
    private readonly ILogger _logger;
    private readonly IRequestDelay _requestDelay;
    private readonly IJsonSerializer _serializer;

    public InstaHttpClient(HttpClient innerClient,
                           HttpClientHandler httpClientHandler,
                           ILogger logger,
                           IJsonSerializer serializer,
                           IDevice device,
                           IAuthorizationHeaderProvider authorizationHeaderProvider,
                           IRequestDelay requestDelay)
    {
        _innerClient = innerClient;
        _httpClientHandler = httpClientHandler;
        _logger = logger;
        _serializer = serializer;
        _device = device;
        _authorizationHeaderProvider = authorizationHeaderProvider;
        _requestDelay = requestDelay;
    }

    public CookieContainer GetCookieContainer()
    {
        return _httpClientHandler.CookieContainer;
    }

    public void SetCookies(CookieCollection cookies)
    {
        foreach (Cookie cookie in cookies) _httpClientHandler.CookieContainer.Add(new Uri(Constants.BASE_URI), cookie);
    }


    public async Task<Either<ResponseStatusBase, T>> SendAsync<T>(HttpRequestMessage requestMessage)
    {
        try
        {
            if (_requestDelay?.Delay > TimeSpan.Zero)
                await Task.Delay(_requestDelay.Delay);

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

    public async Task<Either<ResponseStatusBase, T>> PostSignedAsync<T, R>(Uri uri,
                                                                           R requestData)
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
    {
        return await GetAsync<T>(uri, default);
    }

    public async Task<Either<ResponseStatusBase, T>> GetAsync<T>(Uri uri,
                                                                 GetRequestBase requestData)
    {
        try
        {
            var requestMessage = GetDefaultRequest(HttpMethod.Get, uri);
            if (requestData?.Headers?.Count > 0)
                foreach (var header in requestData.Headers)
                    requestMessage.Options.Set(new HttpRequestOptionsKey<string>(header.Key),
                        header.Value.ToString());

            return await SendAsync<T>(requestMessage);
        }
        catch (Exception exception)
        {
            _logger.LogException(exception);
            return ExceptionalResponseStatus.FromException(exception);
        }
    }

    public async Task<Either<ResponseStatusBase, HttpResponseMessage>> PostSignedAsync<T>(Uri uri,
        T requestData)
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

    public async Task<Either<ResponseStatusBase, T>> PostUnsignedAsync<T>(Uri uri,
                                                                          GetRequestBase request)
    {
        try
        {
            var requestMessage = GetDefaultRequest(HttpMethod.Post, uri);
            requestMessage.Content = new FormUrlEncodedContent(request.RequestData);
            return await SendAsync<T>(requestMessage);
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
        foreach (var item in body) defaultRequest.Options.Set(new HttpRequestOptionsKey<string>(item.Key), item.Value);

        return defaultRequest;
    }

    private HttpRequestMessage GetDefaultRequest(
        HttpMethod method,
        Uri uri)
    {
        var httpRequestMessage = new HttpRequestMessage(method, uri);

        _innerClient.DefaultRequestHeaders.AcceptCharset.Clear();
        _innerClient.DefaultRequestHeaders.AcceptEncoding.Clear();
        _innerClient.DefaultRequestHeaders.Connection.Clear();

        httpRequestMessage.Headers.Add("X-IG-App-Locale", "en_US");
        httpRequestMessage.Headers.Add("X-IG-Device-Locale", "en_US");
        httpRequestMessage.Headers.Add("X-IG-Mapped-Locale", "en_US");
        httpRequestMessage.Headers.Add("X-Pigeon-Session-Id", _device.PigeonSessionId);
        httpRequestMessage.Headers.Add("X-Pigeon-Rawclienttime",
            $"{DateTime.UtcNow.ToUnixTime()}.0{new Random(DateTime.Now.Millisecond).Next(10, 99)}");
        httpRequestMessage.Headers.Add("X-IG-Bandwidth-Speed-KBPS", "125.000");
        httpRequestMessage.Headers.Add("X-IG-Bandwidth-TotalBytes-B", "0");
        httpRequestMessage.Headers.Add("X-IG-Bandwidth-TotalTime-MS", "0");
        httpRequestMessage.Headers.Add("X-Ig-App-Startup-Country", "RU");

        if (!string.IsNullOrEmpty(_authorizationHeaderProvider.WwwClaimHeader))
            httpRequestMessage.Headers.Add(Constants.Headers.WWW_CLAIM, _authorizationHeaderProvider.WwwClaimHeader);
        else
            httpRequestMessage.Headers.Add(Constants.Headers.WWW_CLAIM, "0");

        httpRequestMessage.Headers.Add("X-Bloks-Is-Layout-RTL", "false");
        httpRequestMessage.Headers.Add("X-Bloks-Is-Panorama-Enabled", "true");
        httpRequestMessage.Headers.Add("X-IG-Device-ID", _device.DeviceId.ToString());
        httpRequestMessage.Headers.Add("X-Ig-Family-Device-Id", "a8b8320c-2c9a-49b6-aacb-4d51cbf1f045");
        httpRequestMessage.Headers.Add("X-IG-Android-ID", _device.AndroidId);
        httpRequestMessage.Headers.Add("X-Ig-Timezone-Offset", "10800");
        httpRequestMessage.Headers.Add("X-Ig-Salt-Ids", "1061163349");
        httpRequestMessage.Headers.Add("X-IG-Connection-Type", "WIFI");
        httpRequestMessage.Headers.Add("X-IG-Capabilities", "3brTvx8=");
        httpRequestMessage.Headers.Add("X-IG-App-ID", "567067343352427");
        httpRequestMessage.Headers.Add("User-Agent", _device.UserAgent);
        httpRequestMessage.Headers.Add("Accept-Language", "en_US");

        if (!string.IsNullOrEmpty(_authorizationHeaderProvider.AuthorizationHeader))
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer",
                _authorizationHeaderProvider.AuthorizationHeader.Replace("Bearer", "").Trim(' '));

        if (!string.IsNullOrEmpty(_authorizationHeaderProvider.XMidHeader))
            httpRequestMessage.Headers.Add("X-MID", _authorizationHeaderProvider.XMidHeader);

        if (!string.IsNullOrEmpty(_authorizationHeaderProvider.ShbId))
            httpRequestMessage.Headers.Add(Constants.Headers.IG_U_SHBID, _authorizationHeaderProvider.ShbId);
        if (!string.IsNullOrEmpty(_authorizationHeaderProvider.ShbTs))
            httpRequestMessage.Headers.Add(Constants.Headers.IG_U_SHBTS, _authorizationHeaderProvider.ShbTs);
        if (_authorizationHeaderProvider.CurrentUserIdHeader > 0)
            httpRequestMessage.Headers.Add(Constants.Headers.DS_USER_ID,
                _authorizationHeaderProvider.CurrentUserIdHeader.ToString());
        if (!string.IsNullOrEmpty(_authorizationHeaderProvider.Rur))
            httpRequestMessage.Headers.Add(Constants.Headers.IG_U_RUR, _authorizationHeaderProvider.Rur);
        httpRequestMessage.Headers.Add(Constants.Headers.INTENDED_USER_ID,
            _authorizationHeaderProvider.CurrentUserIdHeader.ToString());

        if (!string.IsNullOrEmpty(_authorizationHeaderProvider.FbTripHeader))
            httpRequestMessage.Headers.Add(Constants.Headers.HEADER_X_FB_TRIP_ID,
                _authorizationHeaderProvider.FbTripHeader);

        httpRequestMessage.Headers.Add("Host", "i.instagram.com");
        httpRequestMessage.Headers.Add("X-FB-HTTP-Engine", "Liger");
        httpRequestMessage.Headers.Add("X-FB-CLIENT-IP", "True");
        httpRequestMessage.Headers.Add("X-FB-SERVER-CLUSTER", "True");
        return httpRequestMessage;
    }
}