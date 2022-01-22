using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Http;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Models.Response.Media;
using LanguageExt;

namespace InstaSharper.API.Services;

internal class FeedService : IFeedService
{
    private readonly IFeedUriProvider _feedUriProvider;
    private readonly IInstaHttpClient _httpClient;
    private readonly IMediaConverters _mediaConverters;

    public FeedService(IFeedUriProvider feedUriProvider,
                       IInstaHttpClient httpClient,
                       IMediaConverters mediaConverters)
    {
        _feedUriProvider = feedUriProvider;
        _httpClient = httpClient;
        _mediaConverters = mediaConverters;
    }

    public async Task<Either<ResponseStatusBase, InstaMediaList>> GetUserFeedAsync(long userPk,
        PaginationParameters paginationParameters)
    {
        return await (await _httpClient.GetAsync<InstaMediaListResponse>(_feedUriProvider.GetUserMediaFeedUri(userPk)))
            .MapAsync(async r
                    =>
                {
                    paginationParameters.PagesLoaded++;

                    var mediaList = _mediaConverters.List.Convert(r);

                    while (r.MoreAvailable
                           && paginationParameters.PagesLoaded <= paginationParameters.MaximumPagesToLoad
                           && !string.IsNullOrEmpty(r.NextMaxId))
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
}