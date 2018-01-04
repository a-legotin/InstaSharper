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
using InstaRecentActivityConverter = InstaSharper.Converters.Json.InstaRecentActivityConverter;

namespace InstaSharper.API.Processors
{
    public class FeedProcessor : IFeedProcessor
    {
        private readonly AndroidDevice _deviceInfo;
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private readonly IInstaLogger _logger;
        private readonly UserSessionData _user;

        public FeedProcessor(AndroidDevice deviceInfo, UserSessionData user, IHttpRequestProcessor httpRequestProcessor,
            IInstaLogger logger)
        {
            _deviceInfo = deviceInfo;
            _user = user;
            _httpRequestProcessor = httpRequestProcessor;
            _logger = logger;
        }

        public async Task<IResult<InstaTagFeed>> GetTagFeedAsync(string tag, int maxPages = 0)
        {
            var tagFeed = new InstaTagFeed();
            try
            {
                if (maxPages == 0) maxPages = int.MaxValue;
                var userFeedUri = UriCreator.GetTagFeedUri(tag);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, userFeedUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<InstaTagFeed>(response, json);
                var feedResponse = JsonConvert.DeserializeObject<InstaTagFeedResponse>(json,
                    new InstaTagFeedDataConverter());
                var converter = ConvertersFabric.Instance.GetTagFeedConverter(feedResponse);
                tagFeed = converter.Convert();
                tagFeed.Medias.Pages++;
                var nextId = feedResponse.NextMaxId;
                var moreAvailable = feedResponse.MoreAvailable;
                while (moreAvailable && tagFeed.Medias.Pages < maxPages)
                {
                    var nextMedia = await GetTagFeedWithMaxIdAsync(tag, nextId);
                    tagFeed.Medias.Pages++;
                    if (!nextMedia.Succeeded)
                        return Result.Success($"Not all pages was downloaded: {nextMedia.Info.Message}", tagFeed);
                    nextId = nextMedia.Value.NextMaxId;
                    moreAvailable = nextMedia.Value.MoreAvailable;
                    tagFeed.Medias.AddRange(ConvertersFabric.Instance.GetMediaListConverter(nextMedia.Value).Convert());
                }
                return Result.Success(tagFeed);
            }
            catch (Exception exception)
            {
                _logger?.LogException(exception);
                return Result.Fail(exception, tagFeed);
            }
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

        public async Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityFeedAsync(int maxPages = 0)
        {
            var uri = UriCreator.GetFollowingRecentActivityUri();
            return await GetRecentActivityInternalAsync(uri, maxPages);
        }

        public async Task<IResult<InstaActivityFeed>> GetRecentActivityFeedAsync(int maxPages = 0)
        {
            var uri = UriCreator.GetRecentActivityUri();
            return await GetRecentActivityInternalAsync(uri, maxPages);
        }

        public async Task<IResult<InstaMediaList>> GetLikeFeedAsync(int maxPages = 0)
        {
            if (maxPages == 0) maxPages = int.MaxValue;
            var instaUri = UriCreator.GetUserLikeFeedUri();
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                    new InstaMediaListDataConverter());
                var moreAvailable = mediaResponse.MoreAvailable;
                var converter = ConvertersFabric.Instance.GetMediaListConverter(mediaResponse);
                var mediaList = converter.Convert();
                mediaList.Pages++;
                var nextId = mediaResponse.NextMaxId;
                while (moreAvailable && mediaList.Pages < maxPages)
                {
                    var result = await GetLikeFeedInternal(nextId);
                    if (!result.Succeeded)
                        return Result.Fail(result.Info, mediaList);
                    converter = ConvertersFabric.Instance.GetMediaListConverter(result.Value);
                    mediaList.AddRange(converter.Convert());
                    mediaList.Pages++;
                    nextId = mediaResponse.NextMaxId;
                    moreAvailable = result.Value.MoreAvailable;
                }
                return Result.Success(mediaList);
            }
            return Result.UnExpectedResponse<InstaMediaList>(response, json);
        }

        public async Task<IResult<InstaMediaListResponse>> GetLikeFeedInternal(string maxId = "")
        {
            var instaUri = UriCreator.GetUserLikeFeedUri(maxId);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.UnExpectedResponse<InstaMediaListResponse>(response, json);
            var mediaResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                new InstaMediaListDataConverter());
            return Result.Success(mediaResponse);
        }

        private async Task<IResult<InstaRecentActivityResponse>> GetFollowingActivityWithMaxIdAsync(string maxId)
        {
            var uri = UriCreator.GetFollowingRecentActivityUri(maxId);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var followingActivity = JsonConvert.DeserializeObject<InstaRecentActivityResponse>(json,
                    new InstaRecentActivityConverter());
                return Result.Success(followingActivity);
            }
            return Result.UnExpectedResponse<InstaRecentActivityResponse>(response, json);
        }

        private async Task<IResult<InstaActivityFeed>> GetRecentActivityInternalAsync(Uri uri, int maxPages = 0)
        {
            if (maxPages == 0) maxPages = int.MaxValue;

            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri, _deviceInfo);
            var response = await _httpRequestProcessor.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            var activityFeed = new InstaActivityFeed();
            var json = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return Result.UnExpectedResponse<InstaActivityFeed>(response, json);
            var feedPage = JsonConvert.DeserializeObject<InstaRecentActivityResponse>(json,
                new InstaRecentActivityConverter());
            activityFeed.IsOwnActivity = feedPage.IsOwnActivity;
            var nextId = feedPage.NextMaxId;
            activityFeed.Items.AddRange(
                feedPage.Stories.Select(ConvertersFabric.Instance.GetSingleRecentActivityConverter)
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
                    feedPage.Stories.Select(ConvertersFabric.Instance.GetSingleRecentActivityConverter)
                        .Select(converter => converter.Convert()));
                pages++;
            }
            return Result.Success(activityFeed);
        }

        private async Task<IResult<InstaMediaListResponse>> GetTagFeedWithMaxIdAsync(string tag, string nextId)
        {
            try
            {
                var instaUri = UriCreator.GetTagFeedUri(tag);
                instaUri = new UriBuilder(instaUri) {Query = $"max_id={nextId}"}.Uri;
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, instaUri, _deviceInfo);
                var response = await _httpRequestProcessor.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var feedResponse = JsonConvert.DeserializeObject<InstaMediaListResponse>(json,
                        new InstaMediaListDataConverter());
                    return Result.Success(feedResponse);
                }
                return Result.UnExpectedResponse<InstaMediaListResponse>(response, json);
            }
            catch (Exception exception)
            {
                return Result.Fail(exception.Message, (InstaMediaListResponse) null);
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