using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApi.API.Web;
using InstagramApi.Classes;
using InstagramApi.Classes.Android.DeviceInfo;
using InstagramApi.Classes.Web;
using InstagramApi.Logger;
using InstagramApi.ResponseWrappers.Android;
using Newtonsoft.Json;

namespace InstagramApi.API.Android
{
    public class InstaApi : IInstaApi
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;
        private readonly UserCredentials _user;
        private readonly ILogger _logger;
        private readonly ApiRequestMessage _requestMessage;


        public InstaApi(UserCredentials _user, ILogger _logger, HttpClient _httpClient,
            HttpClientHandler _httpHandler, ApiRequestMessage _requestMessage)
        {
            this._user = _user;
            this._logger = _logger;
            this._httpClient = _httpClient;
            this._httpHandler = _httpHandler;
            this._requestMessage = _requestMessage;
        }

        public bool IsUserAuthenticated { get; private set; }

        public InstaMedia GetMediaByCode(string postCode)
        {
            throw new NotImplementedException();
        }

        public Task<InstaMedia> GetMediaByCodeAsync(string postCode)
        {
            throw new NotImplementedException();
        }

        public InstaUser GetUser()
        {
            throw new NotImplementedException();
        }

        public Task<InstaUser> GetUserAsync()
        {
            throw new NotImplementedException();
        }

        public InstaUserFeed GetUserFeed(int pageCount)
        {
            return GetUserFeedAsync(1).Result;
        }

        public async Task<InstaUserFeed> GetUserFeedAsync(int pageCount)
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
            if (!IsUserAuthenticated) throw new ArgumentException("user must be authenticated");

            _httpClient.DefaultRequestHeaders.Add("User-Agent", InstaApiConstants.USER_AGENT);

            Uri instaUri;
            if (!Uri.TryCreate(_httpClient.BaseAddress, InstaApiConstants.TIMELINEFEED, out instaUri))
                _logger.Write("Unable to create uri");
            var request = new HttpRequestMessage(HttpMethod.Get, instaUri);
            request.Headers.Add(InstaApiConstants.HEADER_PHONE_ID, _requestMessage.phone_id);
            request.Headers.Add(InstaApiConstants.HEADER_TIMEZONE, InstaApiConstants.IG_CAPABILITIES);
            request.Headers.Add(InstaApiConstants.HEADER_XGOOGLE_AD_IDE,
                InstaApiConstants.IG_CONNECTION_TYPE);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
                return new InstaUserFeed();
            var loginInfo = JsonConvert.DeserializeObject<BadStatusResponse>(await response.Content.ReadAsStringAsync());
            return null;
        }

        public InstaPostList GetUserPosts()
        {
            throw new NotImplementedException();
        }

        public Task<InstaPostList> GetUserPostsAsync()
        {
            throw new NotImplementedException();
        }

        public bool Login()
        {
            return LoginAsync().Result;
        }

        public async Task<bool> LoginAsync()
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
            if ((_requestMessage == null) || _requestMessage.IsEmpty())
                throw new ArgumentException("API request message null or empty");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", InstaApiConstants.USER_AGENT);
            var firstResponse = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var csrftoken = string.Empty;
            var cookies = _httpHandler.CookieContainer.GetCookies(_httpClient.BaseAddress);
            foreach (Cookie cookie in cookies)
                if (cookie.Name == InstaApiConstants.CSRFTOKEN)
                {
                    csrftoken = cookie.Value;
                    break;
                }
            Uri instaUri;
            if (!Uri.TryCreate(_httpClient.BaseAddress, InstaApiConstants.ACCOUNTS_LOGIN, out instaUri))
                _logger.Write("Unable to create uri");
            var request = new HttpRequestMessage(HttpMethod.Post, instaUri);
            var signature = $"{_requestMessage.GenerateSignature()}.{_requestMessage.GetMessageString()}";
            var fields = new Dictionary<string, string>
            {
                {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                {
                    InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                    InstaApiConstants.IG_SIGNATURE_KEY_VERSION
                }
            };
            request.Content = new FormUrlEncodedContent(fields);
            request.Headers.Add(InstaApiConstants.HEADER_ACCEPT_LANGUAGE,
                InstaApiConstants.ACCEPT_LANGUAGE);
            request.Headers.Add(InstaApiConstants.HEADER_IG_CAPABILITIES,
                InstaApiConstants.IG_CAPABILITIES);
            request.Headers.Add(InstaApiConstants.HEADER_IG_CONNECTION_TYPE,
                InstaApiConstants.IG_CONNECTION_TYPE);
            request.Headers.Add(InstaApiConstants.HEADER_USER_AGENT, InstaApiConstants.USER_AGENT);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var loginInfo =
                    JsonConvert.DeserializeObject<InstaResponseLoginAndroid>(await response.Content.ReadAsStringAsync());
                IsUserAuthenticated = (loginInfo.User != null) && (loginInfo.User.UserName == _user.UserName);
                return true;
            }
            else
            {
                var loginInfo =
                    JsonConvert.DeserializeObject<BadStatusResponse>(await response.Content.ReadAsStringAsync());
                return IsUserAuthenticated;
            }
        }
    }
}