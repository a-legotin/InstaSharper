using System;
using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Http;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Models.Request.Feed;
using InstaSharper.Models.Response.Feed;
using InstaSharper.Models.Response.Media;
using LanguageExt;

namespace InstaSharper.API.Services;

internal class FeedService : IFeedService
{
    private readonly IApiStateProvider _apiStateProvider;
    private readonly IFeedUriProvider _feedUriProvider;
    private readonly IInstaHttpClient _httpClient;
    private readonly IMediaConverters _mediaConverters;

    public FeedService(IFeedUriProvider feedUriProvider,
                       IInstaHttpClient httpClient,
                       IMediaConverters mediaConverters,
                       IApiStateProvider apiStateProvider)
    {
        _feedUriProvider = feedUriProvider;
        _httpClient = httpClient;
        _mediaConverters = mediaConverters;
        _apiStateProvider = apiStateProvider;
    }

    public async Task<Either<ResponseStatusBase, InstaMediaList>> GetUserFeedAsync(long userPk,
        PaginationParameters paginationParameters)
    {
        return await (await _httpClient.GetAsync<InstaMediaListResponse>(_feedUriProvider.GetUserMediaFeedUri(userPk)))
            .MapAsync(async r
                    =>
                {
                    paginationParameters.PagesLoaded++;
                    paginationParameters.NextMaxId = r.NextMaxId;
                    var mediaList = _mediaConverters.List.Convert(r);

                    while (r.MoreAvailable
                           && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad
                           && !string.IsNullOrEmpty(paginationParameters.NextMaxId))
                        (await _httpClient.GetAsync<InstaMediaListResponse>(
                                _feedUriProvider.GetUserMediaFeedUri(userPk, mediaList.NextMaxId)))
                            .Match(ok =>
                            {
                                paginationParameters.PagesLoaded++;
                                mediaList.NextMaxId = paginationParameters.NextMaxId = ok.NextMaxId;
                                mediaList.AddRange(_mediaConverters.List.Convert(ok));
                            }, fail => { });

                    return mediaList;
                }
            );
    }


    public async Task<Either<ResponseStatusBase, InstaFeed>> GetTimelineFeedAsync(
        PaginationParameters paginationParameters)
    {
        var sessionId = Guid.NewGuid().ToString();
        var clientSessionId = Guid.NewGuid().ToString();

        return await (await _httpClient.PostUnsignedAsync<InstaFeedResponse>(
                _feedUriProvider.GetTimelineFeedUri(paginationParameters.NextMaxId),
                TimelineFeedRequest.Build(_apiStateProvider, sessionId, clientSessionId,
                    paginationParameters.NextMaxId)))
            .MapAsync(async r
                    =>
                {
                    paginationParameters.PagesLoaded++;
                    paginationParameters.NextMaxId = r.NextMaxId;
                    var feed = _mediaConverters.Feed.Convert(r);

                    while (r.MoreAvailable
                           && paginationParameters.PagesLoaded < paginationParameters.MaximumPagesToLoad
                           && !string.IsNullOrEmpty(paginationParameters.NextMaxId))
                        (await _httpClient.PostUnsignedAsync<InstaFeedResponse>(
                                _feedUriProvider.GetTimelineFeedUri(paginationParameters.NextMaxId),
                                TimelineFeedRequest.Build(_apiStateProvider, sessionId, clientSessionId,
                                    paginationParameters.NextMaxId)))
                            .Match(ok =>
                            {
                                paginationParameters.PagesLoaded++;
                                feed.NextMaxId = paginationParameters.NextMaxId = ok.NextMaxId;
                                var nextFeed = _mediaConverters.Feed.Convert(ok);
                                feed.Medias.AddRange(nextFeed.Medias);
                            }, fail => { });

                    return feed;
                }
            );
    }
}