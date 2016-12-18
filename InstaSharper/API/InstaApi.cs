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

        public IResult<bool> Logout()
        {
            return LogoutAsync().Result;
        }

        public IResult<InstaUserList> GetUserFollowers(string username, int maxPages = 0)
        {
            return GetUserFollowersAsync(username, maxPages).Result;
        }

        public IResult<InstaFeed> GetTagFeed(string tag, int maxPages = 0)
        {
            return GetTagFeedAsync(tag, maxPages).Result;
        }

        public IResult<InstaFeed> GetExploreFeed(int maxPages = 0)
        {
            return GetExploreFeedAsync(maxPages).Result;
        }

        public IResult<InstaMediaList> GetUserTags(string username, int maxPages = 0)
        {
            return GetUserTagsAsync(username, maxPages).Result;
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
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json, new InstaMediaListDataConverter());
                if (mediaResponse.Medias?.Count != 1)
                {
                    string errorMessage = $"Got wrong media count for request with media id={postCode}";
                    _logger.Write(errorMessage);
                    return Result.Fail<InstaMedia>(errorMessage);
                }
                var converter = ConvertersFabric.GetSingleMediaConverter(mediaResponse.Medias.FirstOrDefault());
                return Result.Success(converter.Convert());
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaMedia)null);
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
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaUser)null);
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
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaUser)null);
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
            if (response.StatusCode != HttpStatusCode.OK) return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaFeed)null);
            var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json, new InstaFeedResponseDataConverter());
            var converter = ConvertersFabric.GetFeedConverter(feedResponse);
            var feedConverted = converter.Convert();
            feed.Medias.AddRange(feedConverted.Medias);
            var nextId = feedResponse.NextMaxId;
            while (feedResponse.MoreAvailable && (feed.Pages < maxPages))
            {
                if (string.IsNullOrEmpty(nextId)) break;
                var nextFeed = await GetUserFeedWithMaxIdAsync(nextId);
                if (!nextFeed.Succeeded) Result.Success($"Not all pages was downloaded: {nextFeed.Message}", feed);
                nextId = nextFeed.Value.NextMaxId;
                feed.Medias.AddRange(nextFeed.Value.Items.Select(ConvertersFabric.GetSingleMediaConverter).Select(conv => conv.Convert()));
                feed.Pages++;
            }
            return Result.Success(feed);
        }

        public async Task<IResult<InstaUserList>> GetCurrentUserFollowersAsync(int maxPages = 0)
        {
            ValidateUser();
            return await GetUserFollowersAsync(_user.UserName, maxPages);
        }

        public async Task<IResult<bool>> SendDirectMessageAsync(string recipientName, string message)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                //    var instaUri = UriCreator.GetDirectSendMessageUri();

                //    var fields = new Dictionary<string, string>
                //{
                //    {"type", "form-data"},
                //    {"name", "recipient_users"},
                //    {"data", "[[\"alexandr_le\"]]"}
                //};
                //    var fields1 = new Dictionary<string, string>
                //{
                //    {"type", "form-data"},
                //    {"name", "client_context"},
                //    {"data", _deviceInfo.DeviceGuid.ToString()}
                //};
                //    var fields2 = new Dictionary<string, string>
                //{
                //    {"type", "form-data"},
                //    {"name", "thread_ids"},
                //    {"data", "[\"0\"]"}
                //};
                //    var fields3 = new Dictionary<string, string>
                //{
                //    {"type", "form-data"},
                //    {"name", "text"},
                //    {"data", "text test"}
                //};
                //    var recipientContent = new MultipartFormDataContent();
                //    recipientContent.Add(new StringContent("name"), "recipient_users");
                //    recipientContent.Add(new StringContent("data"), "alexandr_le");
                //    recipientContent.Add(new StringContent("type"), "form-data");

                //    var clientContextContent = new MultipartFormDataContent();
                //    clientContextContent.Add(new StringContent("name"), "client_context");
                //    clientContextContent.Add(new StringContent("data"), _deviceInfo.DeviceGuid.ToString());
                //    clientContextContent.Add(new StringContent("type"), "form-data");

                //    var threadsContent = new MultipartFormDataContent();
                //    threadsContent.Add(new StringContent("name"), "thread_ids");
                //    threadsContent.Add(new StringContent("data"), "0");
                //    threadsContent.Add(new StringContent("type"), "form-data");

                //    var itselfContent = new MultipartFormDataContent();
                //    threadsContent.Add(new StringContent("name"), "text");
                //    threadsContent.Add(new StringContent("data"), "text test");
                //    threadsContent.Add(new StringContent("type"), "form-data");
                //    var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                //    var request = new HttpRequestMessage(HttpMethod.Post, instaUri);
                //    request.Headers.Add("Proxy-Connection", $"keep-alive");
                //    request.Headers.Add("Connection", $"keep-alive");
                //    request.Headers.Add("Accept", $"*/*");
                //    request.Headers.Add("Content-Type", $"multipart/form-data; boundary={_deviceInfo.GoogleAdId}");
                //    request.Headers.Add("Accept-Language", $"en-en");
                //    var content = new MultipartContent { recipientContent, threadsContent, clientContextContent, itselfContent };
                //    content.Headers.Clear();
                //    content.Headers.Add("Content-Type", $"multipart/form-data; boundary={_deviceInfo.DeviceGuid.ToString()}");
                //    request.Content = content;
                //    var cont = await content.ReadAsStringAsync();
                //    var response = await _httpClient.SendAsync(request);
                //    var json = await response.Content.ReadAsStringAsync();
                //    if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", false);
                //    var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json, new InstaMediaListDataConverter());

                return Result.Success(true);
            }
            catch (Exception exception)
            {
                return Result.Fail<bool>(exception);
            }
        }

        public async Task<IResult<InstaDirectInboxContainer>> GetDirectInboxAsync(string recipientName, string message)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var directInboxUri = UriCreator.GetDirectInboxUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaDirectInboxContainer)null);
                var inboxResponse = JsonConvert.DeserializeObject<InstaDirectInboxContainerResponse>(json);
                var converter = ConvertersFabric.GetDirectInboxConverter(inboxResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaDirectInboxContainer>(exception);
            }
        }

        public async Task<IResult<InstaDirectInboxThread>> GetDirectInboxThreadAsync(string threadId)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var directInboxUri = UriCreator.GetDirectInboxThreadUri(threadId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, directInboxUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaDirectInboxThread)null);
                var threadResponse = JsonConvert.DeserializeObject<InstaDirectInboxThreadResponse>(json, new InstaThreadDataConverter());
                var converter = ConvertersFabric.GetDirectThreadConverter(threadResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaDirectInboxThread>(exception);
            }
        }

        public async Task<IResult<InstaFeed>> GetExploreFeedAsync(int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var exploreUri = UriCreator.GetExploreUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, exploreUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var exploreFeed = new InstaFeed();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaFeed)null);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json, new InstaMediaListDataConverter());
                exploreFeed.Medias.AddRange(mediaResponse.Medias.Select(ConvertersFabric.GetSingleMediaConverter).Select(converter => converter.Convert()));
                exploreFeed.Stories.AddRange(mediaResponse.Stories.Select(ConvertersFabric.GetSingleStoryConverter).Select(converter => converter.Convert()));
                var pages = 1;
                var nextId = mediaResponse.NextMaxId;
                while (!string.IsNullOrEmpty(nextId) && (pages < maxPages)) if (string.IsNullOrEmpty(nextId) || (nextId == "0")) break;
                return Result.Success(exploreFeed);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaFeed)null);
            }
        }

        public async Task<IResult<InstaMediaList>> GetUserTagsAsync(string username, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var user = await GetUserAsync(username);
                if (!user.Succeeded || string.IsNullOrEmpty(user.Value.Pk)) return Result.Fail($"Unable to get user {username}", (InstaMediaList)null);
                var uri = UriCreator.GetUserTagsUri(user.Value?.Pk, _user.RankToken);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var userTags = new InstaMediaList();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaMediaList)null);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json, new InstaMediaListDataConverter());
                var nextId = mediaResponse.NextMaxId;
                userTags.AddRange(mediaResponse.Medias.Select(ConvertersFabric.GetSingleMediaConverter).Select(converter => converter.Convert()));
                var pages = 1;
                while (!string.IsNullOrEmpty(nextId) && (pages < maxPages))
                {
                    uri = UriCreator.GetUserTagsUri(user.Value?.Pk, _user.RankToken, nextId);
                    var nextMedia = await GetUserMediaListWithMaxIdAsync(uri);
                    if (!nextMedia.Succeeded) Result.Success($"Not all pages was downloaded: {nextMedia.Message}", userTags);
                    nextId = nextMedia.Value.NextMaxId;
                    userTags.AddRange(mediaResponse.Medias.Select(ConvertersFabric.GetSingleMediaConverter).Select(converter => converter.Convert()));
                    pages++;
                }
                return Result.Success(userTags);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaMediaList)null);
            }
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
                if (!followersResponse.IsOK()) Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaUserList)null);
                followers.AddRange(followersResponse.Items.Select(ConvertersFabric.GetUserConverter).Select(converter => converter.Convert()));
                if (!followersResponse.IsBigList) return Result.Success(followers);
                var pages = 1;
                while (!string.IsNullOrEmpty(followersResponse.NextMaxId) && (pages < maxPages))
                {
                    var nextFollowers = Result.Success(followersResponse);
                    nextFollowers = await GetUserFollowersWithMaxIdAsync(username, nextFollowers.Value.NextMaxId);
                    if (!nextFollowers.Succeeded) Result.Success($"Not all pages was downloaded: {nextFollowers.Message}", followers);
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
                return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaFollowersResponse)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaFollowersResponse)null);
            }
        }

        public async Task<IResult<bool>> CheckpointAsync(string checkPointUrl)
        {
            if (string.IsNullOrEmpty(checkPointUrl)) return Result.Fail("Empty checkpoint URL", false);
            var instaUri = new Uri(checkPointUrl);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK) return Result.Success(true);
            return Result.Fail(GetBadStatusFromJsonString(json).Message, false);
        }

        public async Task<IResult<InstaFeed>> GetTagFeedAsync(string tag, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            var userFeedUri = UriCreator.GetTagFeedUri(tag);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feedResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json, new InstaMediaListDataConverter());
                var converter = ConvertersFabric.GetMediaListConverter(feedResponse);
                var tagFeed = new InstaFeed();
                tagFeed.Medias.AddRange(converter.Convert());
                var nextId = feedResponse.NextMaxId;
                while (feedResponse.MoreAvailable && (tagFeed.Pages < maxPages))
                {
                    var nextMedia = await GetTagFeedWithMaxIdAsync(tag, nextId);
                    if (!nextMedia.Succeeded) Result.Success($"Not all pages was downloaded: {nextMedia.Message}", tagFeed);
                    nextId = nextMedia.Value.NextMaxId;
                    converter = ConvertersFabric.GetMediaListConverter(nextMedia.Value);
                    tagFeed.Medias.AddRange(converter.Convert());
                    tagFeed.Pages++;
                }
                return Result.Success(tagFeed);
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaFeed)null);
        }

        private async Task<IResult<InstaMediaListResponse>> GetTagFeedWithMaxIdAsync(string tag, string nextId)
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
                    var feedResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json, new InstaMediaListDataConverter());
                    return Result.Success(feedResponse);
                }
                return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaMediaListResponse)null);
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
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json, new InstaMediaListDataConverter());
                var converter = ConvertersFabric.GetMediaListConverter(mediaResponse);
                var mediaList = converter.Convert();
                var nextId = mediaResponse.NextMaxId;
                while (mediaResponse.MoreAvailable && (mediaList.Pages < maxPages))
                {
                    instaUri = UriCreator.GetMediaListWithMaxIdUri(user.Pk, nextId);
                    var nextMedia = await GetUserMediaListWithMaxIdAsync(instaUri);
                    if (!nextMedia.Succeeded) Result.Success($"Not all pages was downloaded: {nextMedia.Message}", mediaList);
                    nextId = nextMedia.Value.NextMaxId;
                    mediaList.AddRange(converter.Convert());
                    mediaList.Pages++;
                }
                return Result.Success(mediaList);
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaMediaList)null);
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
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var loginInfo =
                        JsonConvert.DeserializeObject<InstaLoginResponse>(json);
                    IsUserAuthenticated = (loginInfo.User != null) && (loginInfo.User.UserName == _user.UserName);
                    var converter = ConvertersFabric.GetUserConverter(loginInfo.User);
                    _user.LoggedInUder = converter.Convert();
                    _user.RankToken = $"{_user.LoggedInUder.Pk}_{_requestMessage.phone_id}";
                    return Result.Success(true);
                }
                else
                {
                    var loginInfo = GetBadStatusFromJsonString(json);
                    if (loginInfo.ErrorType == "checkpoint_logged_out")
                    {
                        var checkPointResult = await CheckpointAsync(loginInfo.CheckPointUrl);
                        IsUserAuthenticated = checkPointResult.Succeeded;
                        return Result.Success(checkPointResult.Value);
                    }
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
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var logoutInfo = JsonConvert.DeserializeObject<BaseStatusResponse>(json);
                    IsUserAuthenticated = logoutInfo.Status == "ok";
                    return Result.Success(true);
                }
                else
                {
                    var logoutInfo = GetBadStatusFromJsonString(json);
                    return Result.Fail(logoutInfo.Message, false);
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

        private BadStatusResponse GetBadStatusFromJsonString(string json)
        {
            var badStatus = new BadStatusResponse();
            try { badStatus = JsonConvert.DeserializeObject<BadStatusResponse>(json); }
            catch (Exception ex)
            {
                badStatus.Message = ex.Message;
            }
            return badStatus;
        }

        private async Task<IResult<InstaFeedResponse>> GetUserFeedWithMaxIdAsync(string maxId)
        {
            Uri instaUri;
            if (!Uri.TryCreate(new Uri(InstaApiConstants.INSTAGRAM_URL), InstaApiConstants.TIMELINEFEED, out instaUri)) throw new Exception("Cant create search user URI");
            var userUriBuilder = new UriBuilder(instaUri) { Query = $"max_id={maxId}" };
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUriBuilder.Uri, _deviceInfo);
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_PHONE_ID, _requestMessage.phone_id));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE, InstaApiConstants.TIMEZONE_OFFSET.ToString()));
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json, new InstaFeedResponseDataConverter());
                return Result.Success(feedResponse);
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaFeedResponse)null);
        }

        private async Task<IResult<InstaMediaListResponse>> GetUserMediaListWithMaxIdAsync(Uri instaUri)
        {
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json, new InstaMediaListDataConverter());
                return Result.Success(mediaResponse);
            }
            return Result.Fail("", (InstaMediaListResponse)null);
        }

        #endregion
    }
}