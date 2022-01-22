using System.Linq;
using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Feed;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Infrastructure.Converters.Feed;

internal class TimelineFeedConverter : IObjectConverter<InstaTimelineFeed, InstaTimelineFeedResponse>
{
    private readonly IObjectConverter<InstaMedia, InstaMediaItemResponse> _mediaConverter;

    public TimelineFeedConverter(IObjectConverter<InstaMedia, InstaMediaItemResponse> mediaConverter)
    {
        _mediaConverter = mediaConverter;
    }

    public InstaTimelineFeed Convert(InstaTimelineFeedResponse source)
    {
        var feed = new InstaTimelineFeed
        {
            NextMaxId = source.NextMaxId,
            MoreAvailable = source.MoreAvailable
        };
        feed.Medias.AddRange(source.Items.Where(item => item.MediaOrAd != null)
                                   .Select(item => _mediaConverter.Convert(item.MediaOrAd)));
        return feed;
    }
}