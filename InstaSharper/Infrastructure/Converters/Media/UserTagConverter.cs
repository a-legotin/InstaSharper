using System;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Infrastructure.Converters.Media;

internal class UserTagConverter : IObjectConverter<InstaUserTag, InstaUserTagResponse>
{
    private readonly IObjectConverter<InstaUserShort, InstaUserShortResponse> _userPreviewConverter;

    public UserTagConverter(IObjectConverter<InstaUserShort, InstaUserShortResponse> userPreviewConverter)
    {
        _userPreviewConverter = userPreviewConverter;
    }

    public InstaUserTag Convert(InstaUserTagResponse source)
    {
        if (source == null) throw new ArgumentNullException("Source object");
        var userTag = new InstaUserTag();
        if (source.Position?.Length == 2)
            userTag.Position = new InstaPosition(source.Position[0], source.Position[1]);
        userTag.TimeInVideo = source.TimeInVideo;
        if (source.User != null)
            userTag.User = _userPreviewConverter.Convert(source.User);
        return userTag;
    }
}