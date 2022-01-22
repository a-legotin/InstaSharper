using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Feed;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Infrastructure.Converters;

internal interface IMediaConverters
{
    IObjectConverter<InstaMediaList, InstaMediaListResponse> List { get; }
    IObjectConverter<InstaFeed, InstaFeedResponse> Feed { get; }
}