using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using InstaSharper.Converters;
using InstaSharper.Converters.Json;
using InstaSharper.Helpers;
using InstaSharper.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InstaRecentActivityConverter = InstaSharper.Converters.Json.InstaRecentActivityConverter;

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

        #region async part

        public async Task<IResult<bool>> LoginAsync()
        {
            ValidateUser();
            ValidateRequestMessage();
            try
            {
                var csrftoken = string.Empty;
                var firstResponse = await _httpClient.GetAsync(_httpClient.BaseAddress);
                var cookies = _httpHandler.CookieContainer.GetCookies(_httpClient.BaseAddress);

                foreach (Cookie cookie in cookies)
                    if (cookie.Name == InstaApiConstants.CSRFTOKEN) csrftoken = cookie.Value;
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
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                    InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var loginInfo =
                        JsonConvert.DeserializeObject<InstaLoginResponse>(json);
                    IsUserAuthenticated = loginInfo.User != null && loginInfo.User.UserName == _user.UserName;
                    var converter = ConvertersFabric.GetUserConverter(loginInfo.User);
                    _user.LoggedInUder = converter.Convert();
                    _user.RankToken = $"{_user.LoggedInUder.Pk}_{_requestMessage.phone_id}";
                    return Result.Success(true);
                }
                else
                {
                    var loginInfo = GetBadStatusFromJsonString(json);
                    if (loginInfo.ErrorType == "checkpoint_logged_out")
                        return Result.Fail("Please go to instagram.com and confirm checkpoint",
                            ResponseType.CheckPointRequired, false);
                    if (loginInfo.ErrorType == "login_required")
                        return Result.Fail("Please go to instagram.com and check if you account marked as unsafe",
                            ResponseType.LoginRequired, false);
                    if (loginInfo.ErrorType == "Sorry, too many requests.Please try again later")
                        return Result.Fail("Please try again later, maximum amount of requests reached",
                            ResponseType.LoginRequired, false);
                    if (loginInfo.ErrorType == "sentry_block")
                        return Result.Fail("Sentry block. You got blocked by Instagram.", ResponseType.SentryBlock, false);
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

        public async Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            var userFeedUri = UriCreator.GetUserFeedUri();
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var feed = new InstaFeed();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaFeed)null);
            var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json,
                new InstaFeedResponseDataConverter());
            var converter = ConvertersFabric.GetFeedConverter(feedResponse);
            var feedConverted = converter.Convert();
            feed.Medias.AddRange(feedConverted.Medias);
            var nextId = feedResponse.NextMaxId;
            var moreAvailable = feedResponse.MoreAvailable;
            while (moreAvailable && feed.Pages < maxPages)
            {
                if (string.IsNullOrEmpty(nextId)) break;
                var nextFeed = await GetUserFeedWithMaxIdAsync(nextId);
                if (!nextFeed.Succeeded) Result.Success($"Not all pages was downloaded: {nextFeed.Info.Message}", feed);
                nextId = nextFeed.Value.NextMaxId;
                moreAvailable = nextFeed.Value.MoreAvailable;
                feed.Medias.AddRange(
                    nextFeed.Value.Items.Select(ConvertersFabric.GetSingleMediaConverter)
                        .Select(conv => conv.Convert()));
                feed.Pages++;
            }
            return Result.Success(feed);
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
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                exploreFeed.Medias.AddRange(
                    mediaResponse.Medias.Select(ConvertersFabric.GetSingleMediaConverter)
                        .Select(converter => converter.Convert()));
                exploreFeed.Stories.AddRange(
                    mediaResponse.Stories.Select(ConvertersFabric.GetSingleStoryConverter)
                        .Select(converter => converter.Convert()));
                var pages = 1;
                var nextId = mediaResponse.NextMaxId;
                while (!string.IsNullOrEmpty(nextId) && pages < maxPages)
                    if (string.IsNullOrEmpty(nextId) || nextId == "0") break;
                return Result.Success(exploreFeed);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaFeed)null);
            }
        }

        public async Task<IResult<InstaMediaList>> GetUserMediaAsync(string username, int maxPages = 0)
        {
            ValidateUser();
            if (maxPages == 0) maxPages = int.MaxValue;
            var user = await GetUserAsync(username);
            if (!user.Succeeded) return Result.Fail<InstaMediaList>("Unable to get current user");
            var instaUri = UriCreator.GetUserMediaListUri(user.Value.Pk);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                var moreAvailable = mediaResponse.MoreAvailable;
                var converter = ConvertersFabric.GetMediaListConverter(mediaResponse);
                var mediaList = converter.Convert();
                mediaList.Pages++;
                var nextId = mediaResponse.NextMaxId;
                while (moreAvailable && mediaList.Pages < maxPages)
                {
                    instaUri = UriCreator.GetMediaListWithMaxIdUri(user.Value.Pk, nextId);
                    var nextMedia = await GetUserMediaListWithMaxIdAsync(instaUri);
                    mediaList.Pages++;
                    if (!nextMedia.Succeeded)
                        Result.Success($"Not all pages were downloaded: {nextMedia.Info.Message}", mediaList);
                    nextId = nextMedia.Value.NextMaxId;
                    moreAvailable = nextMedia.Value.MoreAvailable;
                    converter = ConvertersFabric.GetMediaListConverter(nextMedia.Value);
                    mediaList.AddRange(converter.Convert());
                }
                return Result.Success(mediaList);
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaMediaList)null);
        }

        public async Task<IResult<InstaMedia>> GetMediaByIdAsync(string mediaId)
        {
            ValidateUser();
            var mediaUri = UriCreator.GetMediaUri(mediaId);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, mediaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                if (mediaResponse.Medias?.Count != 1)
                {
                    var errorMessage = $"Got wrong media count for request with media id={mediaId}";
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
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE,
                InstaApiConstants.TIMEZONE_OFFSET.ToString()));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_COUNT, "1"));
            request.Properties.Add(
                new KeyValuePair<string, object>(InstaApiConstants.HEADER_RANK_TOKEN, _user.RankToken));
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userInfo = JsonConvert.DeserializeObject<InstaSearchUserResponse>(json);
                var user = userInfo.Users?.FirstOrDefault(u => u.UserName == username);
                if (user == null)
                {
                    var errorMessage = $"Can't find this user: {username}";
                    _logger.Write(errorMessage);
                    return Result.Fail<InstaUser>(errorMessage);
                }
                var converter = ConvertersFabric.GetUserConverter(user);
                return Result.Success(converter.Convert());
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaUser)null);
        }


        public async Task<IResult<InstaUser>> GetCurrentUserAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            var instaUri = UriCreator.GetCurrentUserUri();
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

        public async Task<IResult<InstaFeed>> GetTagFeedAsync(string tag, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            if (maxPages == 0) maxPages = int.MaxValue;
            var userFeedUri = UriCreator.GetTagFeedUri(tag);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feedResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                var converter = ConvertersFabric.GetMediaListConverter(feedResponse);
                var tagFeed = new InstaFeed();
                tagFeed.Medias.AddRange(converter.Convert());
                tagFeed.Pages++;
                var nextId = feedResponse.NextMaxId;
                var moreAvailable = feedResponse.MoreAvailable;
                while (moreAvailable && tagFeed.Pages < maxPages)
                {
                    var nextMedia = await GetTagFeedWithMaxIdAsync(tag, nextId);
                    tagFeed.Pages++;
                    if (!nextMedia.Succeeded)
                        return Result.Success($"Not all pages was downloaded: {nextMedia.Info.Message}", tagFeed);
                    nextId = nextMedia.Value.NextMaxId;
                    moreAvailable = nextMedia.Value.MoreAvailable;
                    converter = ConvertersFabric.GetMediaListConverter(nextMedia.Value);
                    tagFeed.Medias.AddRange(converter.Convert());
                }
                return Result.Success(tagFeed);
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaFeed)null);
        }

        public async Task<IResult<InstaUserList>> GetUserFollowersAsync(string username, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var user = await GetUserAsync(username);
                var userFollowersUri = UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken);
                var followers = new InstaUserList();
                var followersResponse = await GetUserListByURIAsync(userFollowersUri);
                if (!followersResponse.Succeeded)
                    Result.Fail(followersResponse.Info, (InstaUserList)null);
                followers.AddRange(
                    followersResponse.Value.Items.Select(ConvertersFabric.GetUserConverter)
                        .Select(converter => converter.Convert()));
                if (!followersResponse.Value.IsBigList) return Result.Success(followers);
                var pages = 1;
                while (!string.IsNullOrEmpty(followersResponse.Value.NextMaxId) && pages < maxPages)
                {
                    var nextFollowersUri = UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken, followersResponse.Value.NextMaxId);
                    followersResponse = await GetUserListByURIAsync(nextFollowersUri);
                    if (!followersResponse.Succeeded)
                        return Result.Success($"Not all pages was downloaded: {followersResponse.Info.Message}", followers);
                    followers.AddRange(
                        followersResponse.Value.Items.Select(ConvertersFabric.GetUserConverter)
                            .Select(converter => converter.Convert()));
                    pages++;
                }
                return Result.Success(followers);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaUserList)null);
            }
        }

        public async Task<IResult<InstaUserList>> GetUserFollowingAsync(string username, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var user = await GetUserAsync(username);
                var userFeedUri = UriCreator.GetUserFollowingUri(user.Value.Pk, _user.RankToken);
                var following = new InstaUserList();
                var userListResponse = await GetUserListByURIAsync(userFeedUri);
                if (!userListResponse.Succeeded)
                    Result.Fail(userListResponse.Info, following);
                following.AddRange(
                    userListResponse.Value.Items.Select(ConvertersFabric.GetUserConverter)
                        .Select(converter => converter.Convert()));
                if (!userListResponse.Value.IsBigList) return Result.Success(following);
                var pages = 1;
                while (!string.IsNullOrEmpty(userListResponse.Value.NextMaxId) && pages < maxPages)
                {
                    var nextUri = UriCreator.GetUserFollowingUri(user.Value.Pk, _user.RankToken, userListResponse.Value.NextMaxId);
                    userListResponse = await GetUserListByURIAsync(nextUri);
                    if (!userListResponse.Succeeded)
                        return Result.Success($"Not all pages was downloaded: {userListResponse.Info.Message}", following);
                    following.AddRange(
                        userListResponse.Value.Items.Select(ConvertersFabric.GetUserConverter)
                            .Select(converter => converter.Convert()));
                    pages++;
                }
                return Result.Success(following);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaUserList)null);
            }
        }


        public async Task<IResult<InstaUserList>> GetCurrentUserFollowersAsync(int maxPages = 0)
        {
            ValidateUser();
            return await GetUserFollowersAsync(_user.UserName, maxPages);
        }

        public async Task<IResult<InstaMediaList>> GetUserTagsAsync(string username, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var user = await GetUserAsync(username);
                if (!user.Succeeded || string.IsNullOrEmpty(user.Value.Pk))
                    return Result.Fail($"Unable to get user {username}", (InstaMediaList)null);
                var uri = UriCreator.GetUserTagsUri(user.Value?.Pk, _user.RankToken);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var userTags = new InstaMediaList();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaMediaList)null);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                var nextId = mediaResponse.NextMaxId;
                userTags.AddRange(
                    mediaResponse.Medias.Select(ConvertersFabric.GetSingleMediaConverter)
                        .Select(converter => converter.Convert()));
                var pages = 1;
                while (!string.IsNullOrEmpty(nextId) && pages < maxPages)
                {
                    uri = UriCreator.GetUserTagsUri(user.Value?.Pk, _user.RankToken, nextId);
                    var nextMedia = await GetUserMediaListWithMaxIdAsync(uri);
                    if (!nextMedia.Succeeded)
                        Result.Success($"Not all pages was downloaded: {nextMedia.Info.Message}", userTags);
                    nextId = nextMedia.Value.NextMaxId;
                    userTags.AddRange(
                        mediaResponse.Medias.Select(ConvertersFabric.GetSingleMediaConverter)
                            .Select(converter => converter.Convert()));
                    pages++;
                }
                return Result.Success(userTags);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaMediaList)null);
            }
        }


        public async Task<IResult<InstaDirectInboxContainer>> GetDirectInboxAsync()
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
                var threadResponse = JsonConvert.DeserializeObject<InstaDirectInboxThreadResponse>(json,
                    new InstaThreadDataConverter());
                var converter = ConvertersFabric.GetDirectThreadConverter(threadResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaDirectInboxThread>(exception);
            }
        }


        public async Task<IResult<InstaRecipients>> GetRecentRecipientsAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            var userUri = UriCreator.GetRecentRecipientsUri();
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseRecipients = JsonConvert.DeserializeObject<InstaRecipientsResponse>(json,
                    new InstaRecipientsDataConverter("recent_recipients"));
                var converter = ConvertersFabric.GetRecipientsConverter(responseRecipients);
                return Result.Success(converter.Convert());
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaRecipients)null);
        }

        public async Task<IResult<InstaRecipients>> GetRankedRecipientsAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            var userUri = UriCreator.GetRankedRecipientsUri();
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseRecipients = JsonConvert.DeserializeObject<InstaRecipientsResponse>(json,
                    new InstaRecipientsDataConverter("ranked_recipients"));
                var converter = ConvertersFabric.GetRecipientsConverter(responseRecipients);
                return Result.Success(converter.Convert());
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaRecipients)null);
        }

        public async Task<IResult<InstaActivityFeed>> GetRecentActivityAsync(int maxPages = 0)
        {
            var uri = UriCreator.GetRecentActivityUri();
            return await GetRecentActivityInternalAsync(uri, maxPages);
        }

        public async Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityAsync(int maxPages = 0)
        {
            var uri = UriCreator.GetFollowingRecentActivityUri();
            return await GetRecentActivityInternalAsync(uri, maxPages);
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


        public async Task<IResult<bool>> LikeMediaAsync(string mediaId)
        {
            return await LikeUnlikeMediaInternal(mediaId, UriCreator.GetLikeMediaUri(mediaId));
        }

        public async Task<IResult<bool>> UnLikeMediaAsync(string mediaId)
        {
            return await LikeUnlikeMediaInternal(mediaId, UriCreator.GetUnLikeMediaUri(mediaId));
        }


        public async Task<IResult<bool>> LikeUnlikeMediaInternal(string mediaId, Uri instaUri)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"media_id", mediaId}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    return Result.Success(true);
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, false);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, false);
            }
        }

        public async Task<IResult<InstaCommentList>> GetMediaCommentsAsync(string mediaId, int maxPages = 0)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var commentsUri = UriCreator.GetMediaCommentsUri(mediaId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, commentsUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.Fail($"Unexpected response status: {response.StatusCode}", (InstaCommentList)null);
                var commentListResponse = JsonConvert.DeserializeObject<InstaCommentListResponse>(json);
                var converter = ConvertersFabric.GetCommentListConverter(commentListResponse);
                var instaComments = converter.Convert();
                instaComments.Pages++;
                var nextId = commentListResponse.NextMaxId;
                var moreAvailable = commentListResponse.MoreComentsAvailable;
                while (moreAvailable && instaComments.Pages < maxPages)
                {
                    if (string.IsNullOrEmpty(nextId)) break;
                    var nextComments = await GetCommentListWithMaxIdAsync(mediaId, nextId);
                    if (!nextComments.Succeeded)
                        Result.Success($"Not all pages was downloaded: {nextComments.Info.Message}", instaComments);
                    nextId = nextComments.Value.NextMaxId;
                    moreAvailable = nextComments.Value.MoreComentsAvailable;
                    converter = ConvertersFabric.GetCommentListConverter(nextComments.Value);
                    instaComments.Comments.AddRange(converter.Convert().Comments);
                    instaComments.Pages++;
                }
                return Result.Success(instaComments);
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaCommentList>(exception);
            }
        }

        public async Task<IResult<InstaUserList>> GetMediaLikersAsync(string mediaId)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var likersUri = UriCreator.GetMediaLikersUri(mediaId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, likersUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaUserList)null);
                var instaUsers = new InstaUserList();
                var mediaLikersResponse = JsonConvert.DeserializeObject<InstaMediaLikersResponse>(json);
                if (mediaLikersResponse.UsersCount < 1) return Result.Success(instaUsers);
                instaUsers.AddRange(
                    mediaLikersResponse.Users.Select(ConvertersFabric.GetUserConverter)
                        .Select(converter => converter.Convert()));
                return Result.Success(instaUsers);
            }
            catch (Exception exception)
            {
                return Result.Fail<InstaUserList>(exception);
            }
        }

        public async Task<IResult<InstaFriendshipStatus>> FollowUserAsync(long userId)
        {
            return await FollowUnfollowUserInternal(userId, UriCreator.GetFollowUserUri(userId));
        }

        public async Task<IResult<InstaFriendshipStatus>> UnFollowUserAsync(long userId)
        {
            return await FollowUnfollowUserInternal(userId, UriCreator.GetUnFollowUserUri(userId));
        }


        public async Task<IResult<InstaUser>> SetAccountPrivateAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetUriSetAccountPrivate();
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken}
                };
                var hash = CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                    JsonConvert.SerializeObject(fields));
                var payload = JsonConvert.SerializeObject(fields);
                var signature = $"{hash}.{Uri.EscapeDataString(payload)}";
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = new FormUrlEncodedContent(fields);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                    InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var userInfoUpdated = JsonConvert.DeserializeObject<InstaUserResponse>(json);
                    if (!userInfoUpdated.IsOk())
                        return Result.Fail<InstaUser>(GetBadStatusFromJsonString(json).Message);
                    var converter = ConvertersFabric.GetUserConverter(userInfoUpdated);
                    return Result.Success(converter.Convert());
                }
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, (InstaUser)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaUser)null);
            }
        }

        public async Task<IResult<InstaUser>> SetAccountPublicAsync()
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetUriSetAccountPublic();
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken}
                };
                var hash = CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                    JsonConvert.SerializeObject(fields));
                var payload = JsonConvert.SerializeObject(fields);
                var signature = $"{hash}.{Uri.EscapeDataString(payload)}";
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = new FormUrlEncodedContent(fields);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE, signature);
                request.Properties.Add(InstaApiConstants.HEADER_IG_SIGNATURE_KEY_VERSION,
                    InstaApiConstants.IG_SIGNATURE_KEY_VERSION);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var userInfoUpdated = JsonConvert.DeserializeObject<InstaUserResponse>(json);
                    if (!userInfoUpdated.IsOk())
                        return Result.Fail<InstaUser>(GetBadStatusFromJsonString(json).Message);
                    var converter = ConvertersFabric.GetUserConverter(userInfoUpdated);
                    return Result.Success(converter.Convert());
                }
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, (InstaUser)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaUser)null);
            }
        }


        public async Task<IResult<InstaComment>> CommentMediaAsync(string mediaId, string text)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetPostCommetUri(mediaId);
                var breadcrumb = CryptoHelper.GetCommentBreadCrumbEncoded(text);
                var fields = new Dictionary<string, string>
                {
                    {"user_breadcrumb", breadcrumb},
                    {"idempotence_token", Guid.NewGuid().ToString()},
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"comment_text", text},
                    {"containermodule", "comments_feed_timeline"},
                    {"radio_type", "wifi-none"}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var commentResponse = JsonConvert.DeserializeObject<InstaCommentResponse>(json,
                        new InstaCommentDataConverter());
                    var converter = ConvertersFabric.GetCommentConverter(commentResponse);
                    return Result.Success(converter.Convert());
                }
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, (InstaComment)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaComment)null);
            }
        }

        public async Task<IResult<bool>> DeleteCommentAsync(string mediaId, string commentId)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetDeleteCommetUri(mediaId, commentId);
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    return Result.Success(true);
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, false);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, false);
            }
        }

        public async Task<IResult<InstaMedia>> UploadPhotoAsync(MediaImage image, string caption)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetUploadPhotoUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(uploadId), "\"upload_id\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {
                        new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                        "\"image_compression\""
                    }
                };
                var imageContent = new ByteArrayContent(File.ReadAllBytes(image.URI));
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                requestContent.Add(imageContent, "photo", $"pending_media_{ApiRequestMessage.GenerateUploadId()}.jpg");
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return await ConfigurePhotoAsync(image, uploadId, caption);
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, (InstaMedia)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaMedia)null);
            }
        }

        public async Task<IResult<InstaMedia>> ConfigurePhotoAsync(MediaImage image, string uploadId, string caption)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetMediaConfigureUri();
                var androidVersion =
                    AndroidVersion.FromString(_deviceInfo.FirmwareFingerprint.Split('/')[2].Split(':')[1]);
                if (androidVersion == null)
                    return Result.Fail("Unsupported android version", (InstaMedia)null);
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"media_folder", "Camera"},
                    {"source_type", "4"},
                    {"caption", caption},
                    {"upload_id", uploadId},
                    {
                        "device", new JObject
                        {
                            {"manufacturer", _deviceInfo.HardwareManufacturer},
                            {"model", _deviceInfo.HardwareModel},
                            {"android_version", androidVersion.VersionNumber},
                            {"android_release", androidVersion.APILevel}
                        }
                    },
                    {
                        "edits", new JObject
                        {
                            {"crop_original_size", new JArray {image.Width, image.Height}},
                            {"crop_center", new JArray {0.0, -0.0}},
                            {"crop_zoom", 1}
                        }
                    },
                    {
                        "extra", new JObject
                        {
                            {"source_width", image.Width},
                            {"source_height", image.Height}
                        }
                    }
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var mediaResponse = JsonConvert.DeserializeObject<InstaMediaItemResponse>(json);
                    var converter = ConvertersFabric.GetSingleMediaConverter(mediaResponse);
                    return Result.Success(converter.Convert());
                }
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, (InstaMedia)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaMedia)null);
            }
        }


        public async Task<IResult<InstaStoryTray>> GetStoryTrayAsync()
        {
            ValidateUser();
            ValidateLoggedIn();

            try
            {
                var storyTrayUri = UriCreator.GetStoryTray();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, storyTrayUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaStoryTray)null);
                var instaStoryTray = new InstaStoryTray();
                var instaStoryTrayResponse = JsonConvert.DeserializeObject<InstaStoryTrayResponse>(json);

                instaStoryTray = ConvertersFabric.GetStoryTrayConverter(instaStoryTrayResponse).Convert();

                return Result.Success(instaStoryTray);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaStoryTray)null);
            }
        }

        public async Task<IResult<InstaStory>> GetUserStoryAsync(long userId)
        {
            ValidateUser();
            ValidateLoggedIn();

            try
            {
                var userStoryUri = UriCreator.GetUserStoryUri(userId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userStoryUri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaStory)null);
                var userStory = new InstaStory();
                var userStoryResponse = JsonConvert.DeserializeObject<InstaStoryResponse>(json);

                userStory = ConvertersFabric.GetStoryConverter(userStoryResponse).Convert();

                return Result.Success(userStory);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaStory)null);
            }
        }

        public async Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(MediaImage image, string caption)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetUploadPhotoUri();
                var uploadId = ApiRequestMessage.GenerateUploadId();
                var requestContent = new MultipartFormDataContent(uploadId)
                {
                    {new StringContent(uploadId), "\"upload_id\""},
                    {new StringContent(_deviceInfo.DeviceGuid.ToString()), "\"_uuid\""},
                    {new StringContent(_user.CsrfToken), "\"_csrftoken\""},
                    {
                        new StringContent("{\"lib_name\":\"jt\",\"lib_version\":\"1.3.0\",\"quality\":\"87\"}"),
                        "\"image_compression\""
                    }
                };
                var imageContent = new ByteArrayContent(File.ReadAllBytes(image.URI));
                imageContent.Headers.Add("Content-Transfer-Encoding", "binary");
                imageContent.Headers.Add("Content-Type", "application/octet-stream");
                requestContent.Add(imageContent, "photo", $"pending_media_{ApiRequestMessage.GenerateUploadId()}.jpg");
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = requestContent;
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return await ConfigureStoryPhotoAsync(image, uploadId, caption);
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, (InstaStoryMedia)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaStoryMedia)null);
            }
        }

        public async Task<IResult<InstaStoryMedia>> ConfigureStoryPhotoAsync(MediaImage image, string uploadId, string caption)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var instaUri = UriCreator.GetStoryConfigureUri();
                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"source_type", "1"},
                    {"caption", caption},
                    {"upload_id", uploadId},
                    {"edits", new JObject()},
                    {"disable_comments", false},
                    {"configure_mode", 1},
                    {"camera_position", "unknown"}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var mediaResponse = JsonConvert.DeserializeObject<InstaStoryMediaResponse>(json);
                    var converter = ConvertersFabric.GetStoryMediaConverter(mediaResponse);
                    return Result.Success(converter.Convert());
                }
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, (InstaStoryMedia)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaStoryMedia)null);
            }
        }

        public async Task<IResult<bool>> ChangePasswordAsync(string oldPassword, string newPassword)
        {
            ValidateUser();
            ValidateLoggedIn();

            if (oldPassword == newPassword)
                return Result.Fail("The old password should not the same of the new password", false);

            try
            {
                var changePasswordUri = UriCreator.GetChangePasswordUri();

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"old_password", oldPassword},
                    {"new_password1", newPassword},
                    {"new_password2", newPassword}
                };

                var request = HttpHelper.GetSignedRequest(HttpMethod.Get, changePasswordUri, _deviceInfo, data);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    return Result.Success(true); //If status code is OK, then the password is surely changed
                var error = JsonConvert.DeserializeObject<BadStatusErrorsResponse>(json);
                var errors = "";
                error.Message.Errors.ForEach(errorContent => errors += errorContent + "\n");
                return Result.Fail(errors, false);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, false);
            }
        }

        public async Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType)
        {
            ValidateUser();
            ValidateLoggedIn();

            try
            {
                var deleteMediaUri = UriCreator.GetDeleteMediaUri(mediaId, mediaType);

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"media_id", mediaId}
                };

                var request = HttpHelper.GetSignedRequest(HttpMethod.Get, deleteMediaUri, _deviceInfo, data);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var deletedResponse = JsonConvert.DeserializeObject<DeleteResponse>(json);
                    return Result.Success(deletedResponse.IsDeleted);
                }
                var error = JsonConvert.DeserializeObject<BadStatusErrorsResponse>(json);
                var errors = "";
                error.Message.Errors.ForEach(errorContent => errors += errorContent + "\n");
                return Result.Fail(errors, false);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, false);
            }
        }

        public async Task<IResult<bool>> EditMediaAsync(string mediaId, string caption)
        {
            ValidateUser();
            ValidateLoggedIn();

            try
            {
                var editMediaUri = UriCreator.GetEditMediaUri(mediaId);

                var data = new JObject
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"caption_text", caption}
                };

                var request = HttpHelper.GetSignedRequest(HttpMethod.Get, editMediaUri, _deviceInfo, data);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    return Result.Success(true); //Technically Instagram returns the InstaMediaItem, but it is useless in our case, at this time.
                var error = JsonConvert.DeserializeObject<BadStatusResponse>(json);
                return Result.Fail(error.Message, false);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, false);
            }
        }

        public async Task<IResult<InstaMediaList>> GetLikeFeedAsync(int maxPages = 0)
        {
            ValidateUser();
            if (maxPages == 0) maxPages = int.MaxValue;
            var instaUri = UriCreator.GetUserLikeFeedUri();
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                var moreAvailable = mediaResponse.MoreAvailable;
                var converter = ConvertersFabric.GetMediaListConverter(mediaResponse);
                var mediaList = converter.Convert();
                mediaList.Pages++;
                var nextId = mediaResponse.NextMaxId;
                while (moreAvailable && mediaList.Pages < maxPages)
                {

                }
                return Result.Success(mediaList);
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaMediaList)null);
        }

        #endregion

        #region private part

        private void ValidateUser()
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
        }

        private void ValidateLoggedIn()
        {
            if (!IsUserAuthenticated) throw new ArgumentException("user must be authenticated");
        }

        private void ValidateRequestMessage()
        {
            if (_requestMessage == null || _requestMessage.IsEmpty())
                throw new ArgumentException("API request message null or empty");
        }

        private BadStatusResponse GetBadStatusFromJsonString(string json)
        {
            var badStatus = new BadStatusResponse();
            try
            {
                if (json == "Oops, an error occurred\n")
                    badStatus.Message = json;
                else badStatus = JsonConvert.DeserializeObject<BadStatusResponse>(json);
            }
            catch (Exception ex)
            {
                badStatus.Message = ex.Message;
            }
            return badStatus;
        }

        private async Task<IResult<InstaFeedResponse>> GetUserFeedWithMaxIdAsync(string maxId)
        {
            Uri instaUri;
            if (!Uri.TryCreate(new Uri(InstaApiConstants.INSTAGRAM_URL), InstaApiConstants.TIMELINEFEED, out instaUri))
                throw new Exception("Cant create search user URI");
            var userUriBuilder = new UriBuilder(instaUri) { Query = $"max_id={maxId}" };
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUriBuilder.Uri, _deviceInfo);
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_PHONE_ID,
                _requestMessage.phone_id));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE,
                InstaApiConstants.TIMEZONE_OFFSET.ToString()));
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json,
                    new InstaFeedResponseDataConverter());
                return Result.Success(feedResponse);
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaFeedResponse)null);
        }

        private async Task<IResult<InstaRecentActivityResponse>> GetFollowingActivityWithMaxIdAsync(string maxId)
        {
            var uri = UriCreator.GetFollowingRecentActivityUri(maxId);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var followingActivity = JsonConvert.DeserializeObject<InstaRecentActivityResponse>(json,
                    new InstaRecentActivityConverter());
                return Result.Success(followingActivity);
            }
            return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaRecentActivityResponse)null);
        }

        private async Task<IResult<InstaMediaListResponse>> GetUserMediaListWithMaxIdAsync(Uri instaUri)
        {
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                return Result.Success(mediaResponse);
            }
            return Result.Fail("", (InstaMediaListResponse)null);
        }

        private async Task<IResult<InstaUserListResponse>> GetUserListByURIAsync(Uri uri)
        {
            ValidateUser();
            try
            {
                if (!IsUserAuthenticated) throw new ArgumentException("user must be authenticated");
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var instaUserListResponse = JsonConvert.DeserializeObject<InstaUserListResponse>(json);
                    if (!instaUserListResponse.IsOK()) Result.Fail("", (InstaUserListResponse)null);
                    return Result.Success(instaUserListResponse);
                }
                return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaUserListResponse)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaUserListResponse)null);
            }
        }

        private async Task<IResult<InstaActivityFeed>> GetRecentActivityInternalAsync(Uri uri, int maxPages = 0)
        {
            ValidateLoggedIn();
            if (maxPages == 0) maxPages = int.MaxValue;

            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            var activityFeed = new InstaActivityFeed();
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaActivityFeed)null);
            var feedPage = JsonConvert.DeserializeObject<InstaRecentActivityResponse>(json,
                new InstaRecentActivityConverter());
            activityFeed.IsOwnActivity = feedPage.IsOwnActivity;
            var nextId = feedPage.NextMaxId;
            activityFeed.Items.AddRange(
                feedPage.Stories.Select(ConvertersFabric.GetSingleRecentActivityConverter)
                    .Select(converter => converter.Convert()));
            var pages = 1;
            while (!string.IsNullOrEmpty(nextId) && pages < maxPages)
            {
                var nextFollowingFeed = await GetFollowingActivityWithMaxIdAsync(nextId);
                if (!nextFollowingFeed.Succeeded)
                    return Result.Success($"Not all pages was downloaded: {nextFollowingFeed.Info.Message}",
                        activityFeed);
                nextId = nextFollowingFeed.Value.NextMaxId;
                activityFeed.Items.AddRange(
                    feedPage.Stories.Select(ConvertersFabric.GetSingleRecentActivityConverter)
                        .Select(converter => converter.Convert()));
                pages++;
            }
            return Result.Success(activityFeed);
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
                    var feedResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                        new InstaMediaListDataConverter());
                    return Result.Success(feedResponse);
                }
                return Result.Fail(GetBadStatusFromJsonString(json).Message, (InstaMediaListResponse)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaMediaListResponse)null);
            }
        }

        private async Task<IResult<InstaCommentListResponse>> GetCommentListWithMaxIdAsync(string mediaId,
            string nextId)
        {
            var commentsUri = UriCreator.GetMediaCommentsUri(mediaId);
            var commentsUriMaxId = new UriBuilder(commentsUri) { Query = $"max_id={nextId}" }.Uri;
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, commentsUriMaxId, _deviceInfo);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var comments = JsonConvert.DeserializeObject<InstaCommentListResponse>(json);
                return Result.Success(comments);
            }
            return Result.Fail("", (InstaCommentListResponse)null);
        }

        private async Task<IResult<InstaFriendshipStatus>> FollowUnfollowUserInternal(long userId, Uri instaUri)
        {
            ValidateUser();
            ValidateLoggedIn();
            try
            {
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken},
                    {"user_id", userId.ToString()},
                    {"radio_type", "wifi-none"}
                };
                var request = HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(json))
                {
                    var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json,
                        new InstaFriendShipDataConverter());
                    var converter = ConvertersFabric.GetFriendShipStatusConverter(friendshipStatus);
                    return Result.Success(converter.Convert());
                }
                var status = GetBadStatusFromJsonString(json);
                return Result.Fail(status.Message, (InstaFriendshipStatus)null);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaFriendshipStatus)null);
            }
        }

        #endregion
    }
}