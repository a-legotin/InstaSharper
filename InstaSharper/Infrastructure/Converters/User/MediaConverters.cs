using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Feed;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Infrastructure.Converters.User;

internal class MediaConverters : IMediaConverters
{
    public MediaConverters(IObjectConverter<InstaMediaList, InstaMediaListResponse> mediaListConverter,
                           IObjectConverter<InstaFeed, InstaFeedResponse> feedConverter)
    {
        List = mediaListConverter;
        Feed = feedConverter;
    }

    public IObjectConverter<InstaMediaList, InstaMediaListResponse> List { get; }
    public IObjectConverter<InstaFeed, InstaFeedResponse> Feed { get; }
}