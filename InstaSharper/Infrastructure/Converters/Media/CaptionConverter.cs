using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.Media;
using InstaSharper.Models.Response.User;
using InstaSharper.Utils;

namespace InstaSharper.Infrastructure.Converters.Media;

internal class CaptionConverter : IObjectConverter<InstaCaption, InstaCaptionResponse>
{
    private readonly IObjectConverter<InstaUserShort, InstaUserShortResponse> _userPreviewConverter;

    public CaptionConverter(IObjectConverter<InstaUserShort, InstaUserShortResponse> userPreviewConverter)
    {
        _userPreviewConverter = userPreviewConverter;
    }

    public InstaCaption Convert(InstaCaptionResponse source)
    {
        return new InstaCaption
        {
            Pk = source.Pk,
            Text = source.Text,
            CreatedAt = DateTimeHelper.UnixTimestampToDateTime(source.CreatedAtUnixLike),
            CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(source.CreatedAtUtcUnixLike),
            MediaId = source.MediaId,
            UserId = source.UserId,
            User = _userPreviewConverter.Convert(source.User)
        };
    }
}