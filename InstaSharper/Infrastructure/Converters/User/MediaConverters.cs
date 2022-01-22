using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Story;
using InstaSharper.Models.Response.Feed;
using InstaSharper.Models.Response.Media;
using InstaSharper.Models.Response.Story;

namespace InstaSharper.Infrastructure.Converters.User;

internal class MediaConverters : IMediaConverters
{
    public MediaConverters(IObjectConverter<InstaMediaList, InstaMediaListResponse> mediaListConverter,
                           IObjectConverter<InstaTimelineFeed, InstaTimelineFeedResponse> feedConverter,
                           IObjectConverter<InstaStoryFeed, InstaStoryFeedResponse> storyFeed)
    {
        List = mediaListConverter;
        TimelineFeed = feedConverter;
        StoryFeed = storyFeed;
    }

    public IObjectConverter<InstaMediaList, InstaMediaListResponse> List { get; }
    public IObjectConverter<InstaTimelineFeed, InstaTimelineFeedResponse> TimelineFeed { get; }
    public IObjectConverter<InstaStoryFeed, InstaStoryFeedResponse> StoryFeed { get; }
}