using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Infrastructure.Converters.User;

internal class MediaConverters : IMediaConverters
{
    public MediaConverters(IObjectConverter<InstaMediaList, InstaMediaListResponse> mediaListConverter)
    {
        List = mediaListConverter;
    }

    public IObjectConverter<InstaMediaList, InstaMediaListResponse> List { get; }
}