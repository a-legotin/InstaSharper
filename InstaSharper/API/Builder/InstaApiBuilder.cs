using System;
using System.Net.Http;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Logger;

namespace InstaSharper.API.Builder
{
    public class InstaApiBuilder : IInstaApiBuilder
    {
        private HttpClient _httpClient;
        private HttpClientHandler _httpHandler = new HttpClientHandler();
        private ILogger _logger;
        private ApiRequestMessage _requestMessage;
        private UserSessionData _user;

        public IInstaApi Build()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient(_httpHandler);
                _httpClient.BaseAddress = new Uri(InstaApiConstants.INSTAGRAM_URL);
            }
            AndroidDevice device = null;

            if (_requestMessage == null)
            {
                device = AndroidDeviceGenerator.GetRandomAndroidDevice();
                _requestMessage = new ApiRequestMessage
                {
                    phone_id = device.PhoneGuid.ToString(),
                    guid = device.DeviceGuid,
                    password = _user?.Password,
                    username = _user?.UserName,
                    device_id = ApiRequestMessage.GenerateDeviceId()
                };
            }
            var instaApi = new InstaApi(_user, _logger, _httpClient, _httpHandler, _requestMessage, device);
            return instaApi;
        }

        public IInstaApiBuilder UseLogger(ILogger logger)
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
    }
}