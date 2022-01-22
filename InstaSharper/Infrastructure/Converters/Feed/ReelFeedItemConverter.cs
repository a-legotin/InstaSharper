using System;
using System.Linq;
using InstaSharper.Abstractions.Models.Story;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.Story;
using InstaSharper.Models.Response.User;
using InstaSharper.Utils;

namespace InstaSharper.Infrastructure.Converters.Feed;

internal class ReelFeedItemConverter : IObjectConverter<InstaReelFeedItem, InstaReelFeedResponse>
{
    private readonly IObjectConverter<InstaStoryItem, InstaStoryItemResponse> _storyItemConverter;

    private readonly IObjectConverter<InstaUserShortFriendshipFull, InstaUserShortFriendshipFullResponse>
        _userFriendshipConverter;

    public ReelFeedItemConverter(IObjectConverter<InstaStoryItem, InstaStoryItemResponse> storyItemConverter,
                                 IObjectConverter<InstaUserShortFriendshipFull, InstaUserShortFriendshipFullResponse>
                                     userFriendshipConverter)
    {
        _storyItemConverter = storyItemConverter;
        _userFriendshipConverter = userFriendshipConverter;
    }

    public InstaReelFeedItem Convert(InstaReelFeedResponse source)
    {
        var feedItem = new InstaReelFeedItem
        {
            Id = source.Id,
            Seen = source.Seen,
            CanReply = source.CanReply,
            CanReshare = source.CanReshare,
            ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(source.ExpiringAt),
            MediaIds = source.MediaIds,
            HasBestiesMedia = source.HasBestiesMedia,
            LatestReelMedia = source.LatestReelMedia
        };
        feedItem.User = _userFriendshipConverter.Convert(source.User);
        feedItem.Items.AddRange(
            source.Items?.Where(item => item != null).Select(item => _storyItemConverter.Convert(item)) ??
            Array.Empty<InstaStoryItem>());
        return feedItem;
    }
}