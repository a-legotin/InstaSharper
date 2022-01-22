using System.Linq;
using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Feed;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Infrastructure.Converters.Feed;

internal class TimelineFeedConverter : IObjectConverter<InstaFeed, InstaFeedResponse>
{
    private readonly IObjectConverter<InstaMedia, InstaMediaItemResponse> _mediaConverter;

    public TimelineFeedConverter(IObjectConverter<InstaMedia, InstaMediaItemResponse> mediaConverter)
    {
        _mediaConverter = mediaConverter;
    }

    public InstaFeed Convert(InstaFeedResponse source)
    {
        var feed = new InstaFeed
        {
            NextMaxId = source.NextMaxId,
            MoreAvailable = source.MoreAvailable
        };
        feed.Medias.AddRange(source.Items.Where(item => item.MediaOrAd != null)
                                   .Select(item => _mediaConverter.Convert(item.MediaOrAd)));
        return feed;
    }
}