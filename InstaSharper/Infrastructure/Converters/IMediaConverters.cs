using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Story;
using InstaSharper.Models.Response.Feed;
using InstaSharper.Models.Response.Media;
using InstaSharper.Models.Response.Story;

namespace InstaSharper.Infrastructure.Converters;

internal interface IMediaConverters
{
    IObjectConverter<InstaMediaList, InstaMediaListResponse> List { get; }
    IObjectConverter<InstaTimelineFeed, InstaTimelineFeedResponse> TimelineFeed { get; }
    IObjectConverter<InstaStoryFeed, InstaStoryFeedResponse> StoryFeed { get; }
}