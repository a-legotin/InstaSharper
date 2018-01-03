using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Converters;
using InstaSharper.Converters.Json;
using InstaSharper.Helpers;
using InstaSharper.Logger;
using Newtonsoft.Json;

namespace InstaSharper.API.Processors
{
    public class UserProcessor : IUserProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;

        public UserProcessor(AndroidDevice deviceInfo, UserSessionData user, IHttpRequestProcessor httpRequestProcessor,
            IInstaLogger logger)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
        }


        public async Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(int maxPages = 0)
        {
            var feed = new InstaFeed();
            try
            {
                var userFeedUri = UriCreator.GetUserFeedUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFeed>(response, json);
                var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json,
                    new InstaFeedResponseDataConverter());
                var converter = ConvertersFabric.Instance.GetFeedConverter(feedResponse);
                feed = converter.Convert();
                var nextId = feedResponse.NextMaxId;
                var moreAvailable = feedResponse.MoreAvailable;
                while (moreAvailable && feed.Medias.Pages < maxPages)
                {
                    if (string.IsNullOrEmpty(nextId)) break;
                    var nextFeed = await GetUserFeedWithMaxIdAsync(nextId);
                    if (!nextFeed.Succeeded)
                        Result.Success($"Not all pages was downloaded: {nextFeed.Info.Message}", feed);
                    nextId = nextFeed.Value.NextMaxId;
                    moreAvailable = nextFeed.Value.MoreAvailable;
                    feed.Medias.AddRange(
                        nextFeed.Value.Items.Select(ConvertersFabric.Instance.GetSingleMediaConverter)
                            .Select(conv => conv.Convert()));
                    feed.Medias.Pages++;
                }
                return Result.Success(feed);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, feed);
            }
        }

        public async Task<IResult<InstaExploreFeed>> GetExploreFeedAsync(int maxPages = 0)
        {
            var exploreFeed = new InstaExploreFeed();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var exploreUri = UriCreator.GetExploreUri();
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, exploreUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaExploreFeed) null);
                var feedResponse = JsonConvert.DeserializeObject<InstaExploreFeedResponse>(json,
                    new InstaExploreFeedDataConverter());
                exploreFeed = ConvertersFabric.Instance.GetExploreFeedConverter(feedResponse).Convert();
                var nextId = feedResponse.Items.Medias.LastOrDefault(media => !string.IsNullOrEmpty(media.NextMaxId))
                    ?.NextMaxId;
                while (!string.IsNullOrEmpty(nextId) && exploreFeed.Medias.Pages < maxPages)
                {
                    var nextFeed = await GetExploreFeedAsync(nextId);
                    if (!nextFeed.Succeeded)
                        Result.Success($"Not all pages were downloaded: {nextFeed.Info.Message}", exploreFeed);
                    nextId = feedResponse.Items.Medias.LastOrDefault(media => !string.IsNullOrEmpty(media.NextMaxId))
                        ?.NextMaxId;
                    exploreFeed.Medias.AddRange(
                        nextFeed.Value.Items.Medias.Select(ConvertersFabric.Instance.GetSingleMediaConverter)
                            .Select(conv => conv.Convert()));
                    exploreFeed.Medias.Pages++;
                }
                return Result.Success(exploreFeed);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, exploreFeed);
            }
        }

        public async Task<IResult<InstaMediaList>> GetUserMediaAsync(string username, int maxPages = 0)
        {
            var mediaList = new InstaMediaList();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var user = await GetUserAsync(username);
                if (!user.Succeeded) return Result.Fail<InstaMediaList>("Unable to get current user");
                var instaUri = UriCreator.GetUserMediaListUri(user.Value.Pk);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaMediaList>(response, json);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                var moreAvailable = mediaResponse.MoreAvailable;
                var converter = ConvertersFabric.Instance.GetMediaListConverter(mediaResponse);
                mediaList = converter.Convert();
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
                    converter = ConvertersFabric.Instance.GetMediaListConverter(nextMedia.Value);
                    mediaList.AddRange(converter.Convert());
                }
                return Result.Success(mediaList);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, mediaList);
            }
        }

        public async Task<IResult<InstaUser>> GetUserAsync(string username)
        {
            try
            {
                var userUri = UriCreator.GetUserUri(username);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE,
                    InstaApiConstants.TIMEZONE_OFFSET.ToString()));
                request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_COUNT, "1"));
                request.Properties.Add(
                    new KeyValuePair<string, object>(InstaApiConstants.HEADER_RANK_TOKEN, _user.RankToken));
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaUser>(response, json);
                var userInfo = JsonConvert.DeserializeObject<InstaSearchUserResponse>(json);
                var user = userInfo.Users?.FirstOrDefault(u => u.UserName == username);
                if (user == null)
                {
                    var errorMessage = $"Can't find this user: {username}";
                    _logger?.LogInfo(errorMessage);
                    return Result.Fail<InstaUser>(errorMessage);
                }
                if (string.IsNullOrEmpty(user.Pk))
                    Result.Fail<InstaUser>("Pk is null");
                var converter = ConvertersFabric.Instance.GetUserConverter(user);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaUser>(exception.Message);
            }
        }

        public async Task<IResult<InstaCurrentUser>> GetCurrentUserAsync()
        {
            try
            {
                var instaUri = UriCreator.GetCurrentUserUri();
                var fields = new Dictionary<string, string>
                {
                    {"_uuid", _deviceInfo.DeviceGuid.ToString()},
                    {"_uid", _user.LoggedInUder.Pk},
                    {"_csrftoken", _user.CsrfToken}
                };
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo);
                request.Content = new FormUrlEncodedContent(fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaCurrentUser>(response, json);
                var user = JsonConvert.DeserializeObject<InstaCurrentUserResponse>(json,
                    new InstaCurrentUserDataConverter());
                if (string.IsNullOrEmpty(user.Pk))
                    Result.Fail<InstaCurrentUser>("Pk is null");
                var converter = ConvertersFabric.Instance.GetCurrentUserConverter(user);
                var userConverted = converter.Convert();
                return Result.Success(userConverted);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaCurrentUser>(exception.Message);
            }
        }

        public async Task<IResult<InstaUserShortList>> GetUserFollowersAsync(string username,
            PaginationParameters paginationParameters)
        {
            var followers = new InstaUserShortList();
            try
            {
                var user = await GetUserAsync(username);
                var userFollowersUri =
                    UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken, paginationParameters.NextId);
                var followersResponse = await GetUserListByURIAsync(userFollowersUri);
                if (!followersResponse.Succeeded)
                    Result.Fail(followersResponse.Info, (InstaUserList) null);
                followers.AddRange(
                    followersResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                followers.NextId = followersResponse.Value.NextMaxId;
                var pagesLoaded = 1;
                while (!string.IsNullOrEmpty(followersResponse.Value.NextMaxId)
                       && pagesLoaded < paginationParameters.MaximumPagesToLoad)
                {
                    var nextFollowersUri =
                        UriCreator.GetUserFollowersUri(user.Value.Pk, _user.RankToken,
                            followersResponse.Value.NextMaxId);
                    followersResponse = await GetUserListByURIAsync(nextFollowersUri);
                    if (!followersResponse.Succeeded)
                        return Result.Success($"Not all pages were downloaded: {followersResponse.Info.Message}",
                            followers);
                    followers.AddRange(
                        followersResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                            .Select(converter => converter.Convert()));
                    pagesLoaded++;
                    followers.NextId = followersResponse.Value.NextMaxId;
                }
                return Result.Success(followers);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, followers);
            }
        }

        public async Task<IResult<InstaUserShortList>> GetUserFollowingAsync(string username,
            PaginationParameters paginationParameters)
        {
            var following = new InstaUserShortList();
            try
            {
                var user = await GetUserAsync(username);
                var uri = UriCreator.GetUserFollowingUri(user.Value.Pk, _user.RankToken, paginationParameters.NextId);
                var userListResponse = await GetUserListByURIAsync(uri);
                if (!userListResponse.Succeeded)
                    Result.Fail(userListResponse.Info, following);
                following.AddRange(
                    userListResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                        .Select(converter => converter.Convert()));
                following.NextId = userListResponse.Value.NextMaxId;
                var pages = 1;
                while (!string.IsNullOrEmpty(following.NextId)
                       && pages < paginationParameters.MaximumPagesToLoad)
                {
                    var nextUri =
                        UriCreator.GetUserFollowingUri(user.Value.Pk, _user.RankToken,
                            userListResponse.Value.NextMaxId);
                    userListResponse = await GetUserListByURIAsync(nextUri);
                    if (!userListResponse.Succeeded)
                        return Result.Success($"Not all pages were downloaded: {userListResponse.Info.Message}",
                            following);
                    following.AddRange(
                        userListResponse.Value.Items.Select(ConvertersFabric.Instance.GetUserShortConverter)
                            .Select(converter => converter.Convert()));
                    pages++;
                    following.NextId = userListResponse.Value.NextMaxId;
                }
                return Result.Success(following);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, following);
            }
        }

        public async Task<IResult<InstaUserShortList>> GetCurrentUserFollowersAsync(
            PaginationParameters paginationParameters)
        {
            return await GetUserFollowersAsync(_user.UserName, paginationParameters);
        }

        public async Task<IResult<InstaMediaList>> GetUserTagsAsync(string username, int maxPages = 0)
        {
            var userTags = new InstaMediaList();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var user = await GetUserAsync(username);
                if (!user.Succeeded || string.IsNullOrEmpty(user.Value.Pk))
                    return Result.Fail($"Unable to get user {username}", (InstaMediaList) null);
                var uri = UriCreator.GetUserTagsUri(user.Value?.Pk, _user.RankToken);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaMediaList) null);
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                var nextId = mediaResponse.NextMaxId;
                userTags.AddRange(
                    mediaResponse.Medias.Select(ConvertersFabric.Instance.GetSingleMediaConverter)
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
                        mediaResponse.Medias.Select(ConvertersFabric.Instance.GetSingleMediaConverter)
                            .Select(converter => converter.Convert()));
                    pages++;
                }
                return Result.Success(userTags);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, userTags);
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

        public async Task<IResult<InstaFriendshipStatus>> BlockUserAsync(long userId)
        {
            return await BlockUnblockUserInternal(userId, UriCreator.GetBlockUserUri(userId));
        }

        public async Task<IResult<InstaFriendshipStatus>> UnBlockUserAsync(long userId)
        {
            return await BlockUnblockUserInternal(userId, UriCreator.GetUnBlockUserUri(userId));
        }

        public async Task<IResult<InstaFriendshipStatus>> GetFriendshipStatusAsync(long userId)
        {
            try
            {
                var userUri = UriCreator.GetUserFriendshipUri(userId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatusResponse = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json);
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatusResponse);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail<InstaFriendshipStatus>(exception.Message);
            }
        }

        private async Task<IResult<InstaFriendshipStatus>> FollowUnfollowUserInternal(long userId, Uri instaUri)
        {
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
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(json))
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json,
                    new InstaFriendShipDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (InstaFriendshipStatus) null);
            }
        }

        private async Task<IResult<InstaFriendshipStatus>> BlockUnblockUserInternal(long userId, Uri instaUri)
        {
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
                var request =
                    HttpHelper.GetSignedRequest(HttpMethod.Post, instaUri, _deviceInfo, fields);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(json))
                    return Result.UnExpectedResponse<InstaFriendshipStatus>(response, json);
                var friendshipStatus = JsonConvert.DeserializeObject<InstaFriendshipStatusResponse>(json,
                    new InstaFriendShipDataConverter());
                var converter = ConvertersFabric.Instance.GetFriendShipStatusConverter(friendshipStatus);
                return Result.Success(converter.Convert());
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (InstaFriendshipStatus) null);
            }
        }

        private async Task<IResult<InstaExploreFeedResponse>> GetExploreFeedAsync(string maxId)
        {
            try
            {
                var exploreUri = UriCreator.GetExploreUri(maxId);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, exploreUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK) return Result.Fail("", (InstaExploreFeedResponse) null);
                return Result.Success(
                    JsonConvert.DeserializeObject<InstaExploreFeedResponse>(json, new InstaExploreFeedDataConverter()));
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception.Message, (InstaExploreFeedResponse) null);
            }
        }

        private async Task<IResult<InstaMediaListResponse>> GetUserMediaListWithMaxIdAsync(Uri instaUri)
        {
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                return Result.Success(mediaResponse);
            }
            return Result.Fail("", (InstaMediaListResponse) null);
        }

        private async Task<IResult<InstaUserListShortResponse>> GetUserListByURIAsync(Uri uri)
        {
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.UnExpectedResponse<InstaUserListShortResponse>(response, json);
            var instaUserListResponse = JsonConvert.DeserializeObject<InstaUserListShortResponse>(json);
            if (!instaUserListResponse.IsOk())
            {
                var status = ErrorHandlingHelper.GetBadStatusFromJsonString(json);
                Result.Fail(new ResultInfo(status.Message), (InstaUserListShortResponse) null);
            }
            return Result.Success(instaUserListResponse);
        }

        private async Task<IResult<InstaFeedResponse>> GetUserFeedWithMaxIdAsync(string maxId)
        {
            if (!Uri.TryCreate(new Uri(InstaApiConstants.INSTAGRAM_URL), InstaApiConstants.TIMELINEFEED,
                out var instaUri))
                throw new Exception("Cant create search user URI");
            var userUriBuilder = new UriBuilder(instaUri) {Query = $"max_id={maxId}"};
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userUriBuilder.Uri, _deviceInfo);
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_PHONE_ID,
                _httpRequestProcessor.RequestMessage.phone_id));
            request.Properties.Add(new KeyValuePair<string, object>(InstaApiConstants.HEADER_TIMEZONE,
                InstaApiConstants.TIMEZONE_OFFSET.ToString()));
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json,
                    new InstaFeedResponseDataConverter());
                return Result.Success(feedResponse);
            }
            return Result.UnExpectedResponse<InstaFeedResponse>(response, json);
        }
    }
}