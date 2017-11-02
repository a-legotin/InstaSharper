using System;
using System.Net.Http;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Logger;

namespace InstaSharper.API.Builder
{
    public class InstaApiBuilder : IInstaApiBuilder
    {
        private static readonly Lazy<InstaApiBuilder> LazyInstance =
            new Lazy<InstaApiBuilder>(() => new InstaApiBuilder());

        private TimeSpan _delay;
        private AndroidDevice _device;
        private HttpClient _httpClient;
        private HttpClientHandler _httpHandler = new HttpClientHandler();
        private IHttpRequestProcessor _httpRequestProcessor;
        private IInstaLogger _logger;
        private ApiRequestMessage _requestMessage;
        private UserSessionData _user;

        private InstaApiBuilder()
        {
        }

        public static InstaApiBuilder Instance => LazyInstance.Value;

        public IInstaApi Build()
        {
            if (_httpClient == null)
                _httpClient = new HttpClient(_httpHandler) {BaseAddress = new Uri(InstaApiConstants.INSTAGRAM_URL)};

            if (_requestMessage == null)
            {
                _device = AndroidDeviceGenerator.GetRandomAndroidDevice();
                _requestMessage = new ApiRequestMessage
                {
                    phone_id = _device.PhoneGuid.ToString(),
                    guid = _device.DeviceGuid,
                    password = _user?.Password,
                    username = _user?.UserName,
                    device_id = ApiRequestMessage.GenerateDeviceId()
                };
            }


            if (string.IsNullOrEmpty(_requestMessage.password)) _requestMessage.password = _user?.Password;
            if (string.IsNullOrEmpty(_requestMessage.username)) _requestMessage.username = _user?.UserName;
            if (_device == null && !string.IsNullOrEmpty(_requestMessage.device_id))
                _device = AndroidDeviceGenerator.GetById(_requestMessage.device_id);
            if (_device == null) AndroidDeviceGenerator.GetRandomAndroidDevice();

            if (_httpRequestProcessor == null)
                _httpRequestProcessor =
                    new HttpRequestProcessor(_delay, _httpClient, _httpHandler, _requestMessage, _logger);

            var instaApi = new InstaApi(_user, _logger, _device, _httpRequestProcessor);
            return instaApi;
        }

        public IInstaApiBuilder UseLogger(IInstaLogger logger)
        {
            _logger = logger;
            return this;
        }

        public IInstaApiBuilder UseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            return this;
        }

        public IInstaApiBuilder UseHttpClientHandler(HttpClientHandler handler)
        {
            _httpHandler = handler;
            return this;
        }

        public IInstaApiBuilder SetUserName(string username)
        {
            _user = new UserSessionData {UserName = username};
            return this;
        }

        public IInstaApiBuilder SetUser(UserSessionData user)
        {
            _user = user;
            return this;
        }

        public IInstaApiBuilder SetApiRequestMessage(ApiRequestMessage requestMessage)
        {
            _requestMessage = requestMessage;
            return this;
        }

        public IInstaApiBuilder SetRequestDelay(TimeSpan delay)
        {
            _delay = delay;
            return this;
        }

        public static IInstaApiBuilder CreateBuilder()
        {
            return new InstaApiBuilder();
        }
    }
}