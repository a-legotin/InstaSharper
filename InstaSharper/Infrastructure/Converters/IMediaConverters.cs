using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Infrastructure.Converters;

internal interface IMediaConverters
{
    IObjectConverter<InstaMediaList, InstaMediaListResponse> List { get; }
}