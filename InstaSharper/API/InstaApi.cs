using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Classes.Models;
using InstaSharper.Converters;
using InstaSharper.Converters.Json;
using InstaSharper.Helpers;
using InstaSharper.Logger;
using InstaSharper.ResponseWrappers;
using InstaSharper.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstaSharper.API
{
    public class InstaApi : IInstaApi
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;
        private readonly ILogger _logger;
        private readonly ApiRequestMessage _requestMessage;
        private readonly UserSessionData _user;

        public InstaApi(UserSessionData user,
            ILogger logger,
            HttpClient httpClient,
            HttpClientHandler httpHandler,
            ApiRequestMessage requestMessage,
            AndroidDevice deviceInfo)
        {
            _user = user;
            _logger = logger;
            _httpClient = httpClient;
            _httpHandler = httpHandler;
            _requestMessage = requestMessage;
            _deviceInfo = deviceInfo;
        }

        public bool IsUserAuthenticated { get; private set; }

        #region sync part

        public IResult<InstaMedia> GetMediaByCode(string postCode)
        {
            return GetMediaByCodeAsync(postCode).Result;
        }

        public IResult<InstaUser> GetUser(string username)
        {
            return GetUserAsync(username).Result;
        }

        public IResult<InstaFeed> GetUserFeed(int maxPages = 0)
        {
            return GetUserFeedAsync(maxPages).Result;
        }

        public IResult<InstaMediaList> GetUserMedia(string username, int maxPages = 0)
        {
            return GetUserMediaAsync(username, maxPages).Result;
        }

        public IResult<bool> Login()
        {
            return LoginAsync().Result;
        }

        public IResult<InstaMediaList> GetTagFeed(string tag, int maxPages = 0)
        {
            return GetTagFeedAsync(tag, maxPages).Result;
        }

        public IResult<InstaUserList> GetCurentUserFollowers(int maxPages = 0)
        {
            return GetCurrentUserFollowersAsync(maxPages).Result;
        }

        #endregion

        #region async part

        public async Task<IResult<InstaMedia>> GetMediaByCodeAsync(string postCode)
        {
            ValidateUser();
            var mediaUri = UriCreator.GetMediaUri(postCode);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, mediaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json);
                if (mediaResponse.Items?.Count != 1)
                {
                    string errorMessage = $"Got wrong media count for request with media id={postCode}";
                    _logger.Write(errorMessage);
                    return Result.Fail<InstaMedia>(errorMessage);
                }
                var converter = ConvertersFabric.GetSingleMediaConverter(mediaResponse.Items.FirstOrDefault());
                return Result.Success(converter.Convert());
            }
            var badRequest = JsonConvert.DeserializeObject<BadStatusResponse>(json);
            _logger.Write(badRequest.Message);
            return Result.Fail(badRequest.Message, (InstaMedia)null);
        }

        public async Task<IResult<InstaUser>> GetUserAsync(string username)
        {
            ValidateUser();
            var userUri = UriCreator.GetUserUri(username);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE, InstaApiConstants.TIMEZONE_OFFSET.ToString()));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_COUNT, "1"));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_RANK_TOKEN, _user.RankToken));
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userInfo = JsonConvert.DeserializeObject<InstaSearchUserResponse>(json);
                var user = userInfo.Users?.FirstOrDefault(u => u.UserName == username);
                if (user == null)
                {
                    string errorMessage = $"Can't find this user: {username}";
                    _logger.Write(errorMessage);
                    return Result.Fail<InstaUser>(errorMessage);
                }
                var converter = ConvertersFabric.GetUserConverter(user);
                return Result.Success(converter.Convert());
            }
            var badRequest = JsonConvert.DeserializeObject<BadStatusResponse>(json);
            _logger.Write(badRequest.Message);
            return Result.Fail(badRequest.Message, (InstaUser)null);
        }

        public IResult<InstaUser> GetCurrentUser()
        {
            return GetCurrentUserAsync().Result;
        }

        public async Task<IResult<InstaUser>> GetCurrentUserAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            var instaUri = UriCreator.GetCurrentUserUri();
            dynamic jsonObject = new JObject();
            jsonObject._uuid = _deviceInfo.DeviceGuid;
            jsonObject._uid = _user.LoggedInUder.Pk;
            jsonObject._csrftoken = _user.CsrfToken;
            var fields = new Dictionary<string, string>
            {
                {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                {"_uid", _user.LoggedInUder.Pk},
                {"_csrftoken", _user.CsrfToken}
            };
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
            request.Content = new FormUrlEncodedContent(fields);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var user = JsonConvert.DeserializeObject<InstaCurrentUserResponse>(json);
                var converter = ConvertersFabric.GetUserConverter(user.User);
                var userConverted = converter.Convert();

                return Result.Success(userConverted);
            }
            return Result.Fail("", (InstaUser)null);
        }

        public async Task<IResult<InstaFeed>> GetUserFeedAsync(int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            var userFeedUri = UriCreator.GetUserFeedUri();
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var feed = new InstaFeed();
            if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaFeed)null);
            var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json, new FeedResponseDataConverter());
            var converter = ConvertersFabric.GetFeedConverter(feedResponse);
            var feedConverted = converter.Convert();
            feed.Items.AddRange(feedConverted.Items);
            while (feedResponse.MoreAvailable && (feed.Pages < maxPages))
            {
                var nextFeed = await GetUserFeedWithMaxIdAsync(feedResponse.NextMaxId);
                if (!nextFeed.Succeeded) break;
                feed.Items.AddRange(nextFeed.Value.Items.Select(ConvertersFabric.GetSingleMediaConverter).Select(conv => conv.Convert()));
                feed.Pages++;
            }
            return Result.Success(feed);
        }

        public async Task<IResult<InstaUserList>> GetCurrentUserFollowersAsync(int maxPages = 0)
        {
            ValidateUser();
            return await GetUserFollowersAsync(_user.UserName, maxPages);
        }

        public async Task<IResult<InstaUserList>> GetUserFollowersAsync(string username, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var user = await GetUserAsync(username);
                var userFeedUri = UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var followers = new InstaUserList();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaUserList)null);
                var followersResponse = JsonConvert.DeserializeObject<InstaFollowersResponse>(json);
                if (!followersResponse.IsOK()) Result.Fail("", (InstaUserList)null);
                followers.AddRange(followersResponse.Items.Select(ConvertersFabric.GetUserConverter).Select(converter => converter.Convert()));
                if (!followersResponse.IsBigList) return Result.Success(followers);
                var pages = 1;
                while (!string.IsNullOrEmpty(followersResponse.NextMaxId) && (pages < maxPages))
                {
                    var nextFollowers = Result.Success(followersResponse);
                    nextFollowers = await GetUserFollowersWithMaxIdAsync(username, nextFollowers.Value.NextMaxId);
                    if (!nextFollowers.Succeeded) break;
                    followers.AddRange(nextFollowers.Value.Items.Select(ConvertersFabric.GetUserConverter).Select(converter => converter.Convert()));
                    pages++;
                }
                return Result.Success(followers);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaUserList)null);
            }
        }

        private async Task<IResult<InstaFollowersResponse>> GetUserFollowersWithMaxIdAsync(string username, string maxId)
        {
            ValidateUser();
            try
            {
                if (!IsUserAuthenticated) throw new ArgumentException("user must be authenticated");
                var user = await GetUserAsync(username);
                var userFeedUri = UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken, maxId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var followersResponse = JsonConvert.DeserializeObject<InstaFollowersResponse>(json);
                    if (!followersResponse.IsOK()) Result.Fail("", (InstaFollowersResponse)null);
                    return Result.Success(followersResponse);
                }
                return Result.Fail("", (InstaFollowersResponse)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaFollowersResponse)null);
            }
        }

        public async Task<IResult<InstaMediaList>> GetTagFeedAsync(string tag, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            var userFeedUri = UriCreator.GetTagFeedUri(tag);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feedResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json);
                var converter = ConvertersFabric.GetMediaListConverter(feedResponse);
                var mediaList = converter.Convert();
                while (feedResponse.MoreAvailable && (mediaList.Pages < maxPages))
                {
                    var nextMedia = await GetTagFeedWithMaxIdAsync(tag, feedResponse.NextMaxId);
                    if (!nextMedia.Succeeded) continue;
                    mediaList.AddRange(converter.Convert());
                    mediaList.Pages++;
                }
                return Result.Success(mediaList);
            }
            return Result.Fail("", (InstaMediaList)null);
        }

        public async Task<IResult<InstaMediaListResponse>> GetTagFeedWithMaxIdAsync(string tag, string nextId)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetTagFeedUri(tag);
                instaUri = new UriBuilder(instaUri) { Query = $"max_id={nextId}" }.Uri;
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var feedResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json);
                    return Result.Success(feedResponse);
                }
                return Result.Fail("", (InstaMediaListResponse)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaMediaListResponse)null);
            }
        }

        public async Task<IResult<InstaMediaList>> GetUserMediaAsync(string username, int maxPages = 0)
        {
            ValidateUser();
            if (maxPages == 0) maxPages = int.MaxValue;
            var user = GetUser(username).Value;
            var instaUri = UriCreator.GetUserMediaListUri(user.Pk);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json);
                var converter = ConvertersFabric.GetMediaListConverter(mediaResponse);
                var mediaList = converter.Convert();
                while (mediaResponse.MoreAvailable && (mediaList.Pages < maxPages))
                {
                    var nextMedia = await GetUserMediaListWithMaxIdAsync(user.Pk, mediaResponse.NextMaxId);
                    if (!nextMedia.Succeeded) continue;
                    mediaList.AddRange(converter.Convert());
                    mediaList.Pages++;
                }
                return Result.Success(mediaList);
            }
            var badRequest = JsonConvert.DeserializeObject<BadStatusResponse>(json);
            _logger.Write(badRequest.Message);
            return Result.Fail(badRequest.Message, (InstaMediaList)null);
        }

        public async Task<IResult<InstaFeedResponse>> GetUserFeedWithMaxIdAsync(string maxId)
        {
            Uri instaUri;
            if (!Uri.TryCreate(new Uri(InstaApiConstants.INSTAGRAM_URL), InstaApiConstants.TIMELINEFEED, out instaUri)) throw new Exception("Cant create search user URI");
            var userUriBuilder = new UriBuilder(instaUri) { Query = $"max_id={maxId}" };
            var request = new HttpRequestMessage(HttpMethod.Get, userUriBuilder.Uri);
            request.Headers.Clear();
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_PHONE_ID, _requestMessage.phone_id));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE, InstaApiConstants.TIMEZONE_OFFSET.ToString()));
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var feed = new InstaFeed();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json, new FeedResponseDataConverter());
                return Result.Success(feedResponse);
            }
            return Result.Fail("", (InstaFeedResponse)null);
        }

        private async Task<IResult<InstaMediaListResponse>> GetUserMediaListWithMaxIdAsync(string userPk, string nextId)
        {
            var instaUri = UriCreator.GetMediaListWithMaxIdUri(userPk, nextId);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json);
                return Result.Success(mediaResponse);
            }
            return Result.Fail("", (InstaMediaListResponse)null);
        }

        public async Task<IResult<bool>> LoginAsync()
        {
            ValidateUser();
            ValidateRequestMessage();
            try
            {
                var csrftoken = string.Empty;
                var firstResponse = await _httpClient.GetAsync(_httpClient.BaseAddress);
                var cookies = _httpHandler.CookieContainer.GetCookies(_httpClient.BaseAddress);
                foreach (Cookie cookie in cookies) if (cookie.Name == InstaApiConstants.CSRFTOKEN) csrftoken = cookie.Value;
                _user.CsrfToken = csrftoken;
                var instaUri = UriCreator.GetLoginUri();
                var signature = $"{_requestMessage.GenerateSignature()}.{_requestMessage.GetMessageString()}";
                var fields = new Dictionary<string, string>
                {
                    {InstaApiConstants.HEADER_IG_SIGNATURE, signature},
                    {InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION}
                };
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = new FormUrlEncodedContent(fields);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION, InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var loginInfo =
                        JsonConvert.DeserializeObject<InstaLoginResponse>(await response.Content.ReadAsStringAsync());
                    IsUserAuthenticated = (loginInfo.User != null) && (loginInfo.User.UserName == _user.UserName);
                    var converter = ConvertersFabric.GetUserConverter(loginInfo.User);
                    _user.LoggedInUder = converter.Convert();
                    _user.RankToken = $"{_user.LoggedInUder.Pk}_{_requestMessage.phone_id}";
                    return Result.Success(true);
                }
                else
                {
                    var loginInfo = JsonConvert.DeserializeObject<BadStatusResponse>(await response.Content.ReadAsStringAsync());
                    _logger.Write(loginInfo.Message);
                    return Result.Fail(loginInfo.Message, false);
                }
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, false);
            }
        }

        public async Task<IResult<bool>> LogoutAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetLogoutUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var loginInfo = JsonConvert.DeserializeObject<BaseStatusResponse>(await response.Content.ReadAsStringAsync());
                    IsUserAuthenticated = loginInfo.Status == "ok";
                    return Result.Success(true);
                }
                else
                {
                    var loginInfo = JsonConvert.DeserializeObject<BadStatusResponse>(await response.Content.ReadAsStringAsync());
                    _logger.Write(loginInfo.Message);
                    return Result.Fail(loginInfo.Message, false);
                }
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, false);
            }
        }

        #endregion

        #region private part

        private void ValidateUser()
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password)) throw new ArgumentException("user name and password must be specified");
        }

        private void ValidateLoggedIn()
        {
            if (!IsUserAuthenticated) throw new ArgumentException("user must be authenticated");
        }

        private void ValidateRequestMessage()
        {
            if ((_requestMessage == null) || _requestMessage.IsEmpty()) throw new ArgumentException("API request message null or empty");
        }

        #endregion
    }
}