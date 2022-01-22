using System.Linq;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Infrastructure.Converters.Media;

internal class MediaListConverter : IObjectConverter<InstaMediaList, InstaMediaListResponse>
{
    private readonly IObjectConverter<InstaMedia, InstaMediaItemResponse> _mediaItemConverter;

    public MediaListConverter(IObjectConverter<InstaMedia, InstaMediaItemResponse> mediaItemConverter)
    {
        _mediaItemConverter = mediaItemConverter;
    }

    public InstaMediaList Convert(InstaMediaListResponse source)
    {
        var mediaList = new InstaMediaList
        {
            NextMaxId = source.NextMaxId,
            MoreAvailable = source.MoreAvailable
        };
        mediaList.AddRange(source.Medias.Select(media => _mediaItemConverter.Convert(media)));
        return mediaList;
    }
}