using System;
using System.Linq;
using InstaSharper.Abstractions.Models.Story;
using InstaSharper.Models.Response.Story;

namespace InstaSharper.Infrastructure.Converters.Feed;

internal class StoryFeedConverter : IObjectConverter<InstaStoryFeed, InstaStoryFeedResponse>
{
    private readonly IObjectConverter<InstaReelFeedItem, InstaReelFeedResponse> _feedItemConverter;

    public StoryFeedConverter(IObjectConverter<InstaReelFeedItem, InstaReelFeedResponse> feedItemConverter)
    {
        _feedItemConverter = feedItemConverter;
    }

    public InstaStoryFeed Convert(InstaStoryFeedResponse source)
    {
        var storyFeed = new InstaStoryFeed
        {
            StickerVersion = source.StickerVersion,
            HasNewNuxStory = source.HasNewNuxStory,
            FaceFilterNuxVersion = source.FaceFilterNuxVersion,
            StoryRankingToken = source.StoryRankingToken
        };
        storyFeed.Items.AddRange(
            source.Tray?.Where(item => item != null).Select(item => _feedItemConverter.Convert(item)) ??
            Array.Empty<InstaReelFeedItem>());
        return storyFeed;
    }
}