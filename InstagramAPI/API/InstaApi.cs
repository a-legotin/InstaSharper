using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApi.Classes;
using InstagramApi.Classes.Android.DeviceInfo;
using InstagramApi.Converters;
using InstagramApi.Logger;
using InstagramApi.ResponseWrappers;
using InstagramAPI.Helpers;
using InstagramAPI.ResponseWrappers;
using Newtonsoft.Json;

namespace InstagramApi.API
{
    public class InstaApi : IInstaApi
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;
        private readonly ILogger _logger;
        private readonly ApiRequestMessage _requestMessage;
        private readonly UserCredentials _user;

        public InstaApi(UserCredentials user, ILogger logger, HttpClient httpClient,
            HttpClientHandler httpHandler, ApiRequestMessage requestMessage, AndroidDevice deviceInfo)
        {
            this._user = user;
            this._logger = logger;
            this._httpClient = httpClient;
            this._httpHandler = httpHandler;
            this._requestMessage = requestMessage;
            this._deviceInfo = deviceInfo;
        }

        public bool IsUserAuthenticated { get; private set; }

        public InstaMedia GetMediaByCode(string postCode)
        {
            return GetMediaByCodeAsync(postCode).Result;
        }

        public async Task<InstaMedia> GetMediaByCodeAsync(string postCode)
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
            if ((_requestMessage == null) || _requestMessage.IsEmpty())
                throw new ArgumentException("API request message null or empty");
            Uri instaUri;
            if (!Uri.TryCreate(_httpClient.BaseAddress, string.Format(InstaApiConstants.GET_MEDIA, postCode), out instaUri))
                _logger.Write("Unable to create uri");
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var postsResponse =
                    JsonConvert.DeserializeObject<InstaResponsePostList>(json);
                var converter = ConvertersFabric.GetPostsConverter(postsResponse);
                return new InstaMedia();
            }
            else
            {
                var badRequest =
                    JsonConvert.DeserializeObject<BadStatusResponse>(json);
                _logger.Write(badRequest.Message);
            }
            return null;
        }

        public InstaUser GetUser(string username)
        {
            return GetUserAsync(username).Result;
        }

        public async Task<InstaUser> GetUserAsync(string username)
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
            if ((_requestMessage == null) || _requestMessage.IsEmpty())
                throw new ArgumentException("API request message null or empty");
            Uri instaUri;
            if (!Uri.TryCreate(_httpClient.BaseAddress, InstaApiConstants.SEARCH_USERS, out instaUri))
                _logger.Write("Unable to create uri");
            UriBuilder baseUri = new UriBuilder(instaUri) { Query = $"q={username}" };
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, baseUri.Uri);
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE, InstaApiConstants.TIMEZONE_OFFSET.ToString()));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_COUNT, "1"));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_RANK_TOKEN, _user.RankToken));
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userInfo =
                    JsonConvert.DeserializeObject<InstaSearchUserResponse>(json);
                foreach (var instaUserResponse in userInfo.Users)
                {
                    var converter = ConvertersFabric.GetUserConverter(instaUserResponse);
                    return converter.Convert();
                }
            }
            else
            {
                var badRequest =
                    JsonConvert.DeserializeObject<BadStatusResponse>(json);
                _logger.Write(badRequest.Message);
                return null;
            }
            return null;
        }

        public InstaFeed GetUserFeed(int pageCount)
        {
            return GetUserFeedAsync(1).Result;
        }

        public async Task<InstaFeed> GetUserFeedAsync(int pageCount)
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
            if (!IsUserAuthenticated) throw new ArgumentException("user must be authenticated");

            _httpClient.DefaultRequestHeaders.Add("User-Agent", InstaApiConstants.USER_AGENT);

            Uri instaUri;
            if (!Uri.TryCreate(_httpClient.BaseAddress, InstaApiConstants.TIMELINEFEED, out instaUri))
                _logger.Write("Unable to create uri");
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri);
            request.Headers.Add(InstaApiConstants.HEADER_XGOOGLE_AD_IDE, _deviceInfo.GoogleAdId.ToString());
            var response = await _httpClient.SendAsync(request);
            var feed = new InstaFeed();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feedResponse =
                    JsonConvert.DeserializeObject<InstaFeedResponse>(await response.Content.ReadAsStringAsync());
                var converter = ConvertersFabric.GetFeedConverter(feedResponse);
                var feedConverted = converter.Convert();
                feed.Items.AddRange(feedConverted.Items);
                feed.Pages++;
                if (pageCount < 2) return feed;
                while (feedResponse.MoreAvailable && (feed.Pages <= pageCount))
                {
                    feedResponse = _getFeedResponseWithMaxId(feedResponse.NextMaxId);
                    converter = ConvertersFabric.GetFeedConverter(feedResponse);
                    feedConverted = converter.Convert();
                    feed.Items.AddRange(feedConverted.Items);
                    feed.Pages++;
                }
                return feed;
            }
            return null;
        }

        public InstaPostList GetUserPosts(string username)
        {
            return GetUserPostsAsync(username).Result;
        }

        public async Task<InstaPostList> GetUserPostsAsync(string username)
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
            if ((_requestMessage == null) || _requestMessage.IsEmpty())
                throw new ArgumentException("API request message null or empty");
            var user = GetUser(username);
            Uri instaUri;
            if (!Uri.TryCreate(_httpClient.BaseAddress, InstaApiConstants.USEREFEED + user.Pk + "/", out instaUri))
                _logger.Write("Unable to create uri");
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var postsResponse =
                    JsonConvert.DeserializeObject<InstaResponsePostList>(json);
                var converter = ConvertersFabric.GetPostsConverter(postsResponse);
                return converter.Convert();
            }
            else
            {
                var badRequest =
                    JsonConvert.DeserializeObject<BadStatusResponse>(json);
                _logger.Write(badRequest.Message);
            }
            var posts = new InstaPostList();
            return posts;
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
            var signature = $"{_requestMessage.GenerateSignature()}.{_requestMessage.GetMessageString()}";
            var fields = new Dictionary<string, string>
            {
                {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                {
                    InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                    InstaApiConstants.IG_SIGNATURE_KEY_VERSION
                }
            };
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri);
            request.Content = new FormUrlEncodedContent(fields);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
            request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var loginInfo =
                    JsonConvert.DeserializeObject<InstaResponseLoginAndroid>(await response.Content.ReadAsStringAsync());
                IsUserAuthenticated = (loginInfo.User != null) && (loginInfo.User.UserName == _user.UserName);
                var converter = ConvertersFabric.GetUserConverter(loginInfo.User);
                _user.LoggedInUder = converter.Convert();
                _user.RankToken = $"{_user.LoggedInUder.Pk}_{Guid.NewGuid()}";
                return true;
            }
            else
            {
                var loginInfo =
                    JsonConvert.DeserializeObject<BadStatusResponse>(await response.Content.ReadAsStringAsync());
                _logger.Write(loginInfo.Message);
                return IsUserAuthenticated;
            }
        }

        private InstaFeedResponse _getFeedResponseWithMaxId(string id)
        {
            Uri instaUri;
            if (!Uri.TryCreate(_httpClient.BaseAddress, InstaApiConstants.TIMELINEFEED, out instaUri))
                _logger.Write("Unable to create uri");
            var request = new HttpRequestMessage(HttpMethod.Get, instaUri);
            request.Headers.Clear();
            request.Headers.Add(InstaApiConstants.HEADER_PHONE_ID, _requestMessage.phone_id);
            request.Headers.Add(InstaApiConstants.HEADER_TIMEZONE, InstaApiConstants.TIMEZONE_OFFSET.ToString());
            request.Headers.Add(InstaApiConstants.HEADER_XGOOGLE_AD_IDE, _deviceInfo.GoogleAdId.ToString());
            request.Headers.Add(InstaApiConstants.HEADER_MAX_ID, id);

            var response = _httpClient.SendAsync(request);
            var json = response.Result.Content.ReadAsStringAsync().Result;
            if (response.Result.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<InstaFeedResponse>(json);
            return null;
        }
    }
}