using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.Comment;
using InstaSharper.Models.Response.Location;
using InstaSharper.Models.Response.Media;
using InstaSharper.Models.Response.User;
using InstaSharper.Utils;

namespace InstaSharper.Infrastructure.Converters.Media;

internal class MediaConverter : IObjectConverter<InstaMedia, InstaMediaItemResponse>
{
    private readonly IObjectConverter<InstaCaption, InstaCaptionResponse> _captionConverter;
    private readonly IObjectConverter<InstaCarousel, InstaCarouselResponse> _carouselConverter;
    private readonly IObjectConverter<InstaComment, InstaCommentResponse> _commentConverter;
    private readonly IObjectConverter<InstaLocation, InstaLocationResponse> _locationConverter;
    private readonly IObjectConverter<InstaUser, InstaUserResponse> _userConverter;
    private readonly IObjectConverter<InstaUserShort, InstaUserShortResponse> _userPreviewConverter;
    private readonly IObjectConverter<InstaUserTag, InstaUserTagResponse> _userTagConverter;

    public MediaConverter(IObjectConverter<InstaUser, InstaUserResponse> userConverter,
                          IObjectConverter<InstaCaption, InstaCaptionResponse> captionConverter,
                          IObjectConverter<InstaLocation, InstaLocationResponse> locationConverter,
                          IObjectConverter<InstaUserShort, InstaUserShortResponse> userPreviewConverter,
                          IObjectConverter<InstaComment, InstaCommentResponse> commentConverter,
                          IObjectConverter<InstaCarousel, InstaCarouselResponse> carouselConverter,
                          IObjectConverter<InstaUserTag, InstaUserTagResponse> userTagConverter)
    {
        _userConverter = userConverter;
        _captionConverter = captionConverter;
        _locationConverter = locationConverter;
        _userPreviewConverter = userPreviewConverter;
        _commentConverter = commentConverter;
        _carouselConverter = carouselConverter;
        _userTagConverter = userTagConverter;
    }

    public InstaMedia Convert(InstaMediaItemResponse source)
    {
        var media = new InstaMedia
        {
            Code = source.Code,
            Pk = source.Pk,
            Title = source.Title,
            Height = source.Height,
            Width = source.Width,
            CommentsCount = source.CommentsCount,
            HasAudio = source.HasAudio,
            HasLiked = source.HasLiked,
            InstaIdentifier = source.InstaIdentifier,
            LikesCount = source.LikesCount
        };
        if (source.TakenAtUnixLike > 0)
            media.TakenAt = DateTimeHelper.UnixTimestampToDateTime(source.TakenAtUnixLike);

        if (source.DeviceTimeStampUnixLike > 0)
            media.DeviceTimeStamp = DateTimeHelper.UnixTimestampMicrosecondsToDateTime(source.DeviceTimeStampUnixLike);

        if (source.User != null)
            media.User = _userConverter.Convert(source.User);

        if (source.Caption != null)
            media.Caption = _captionConverter.Convert(source.Caption);

        if (source.Location != null)
            media.Location = _locationConverter.Convert(source.Location);

        if (source.Likers?.Count > 0)
            foreach (var liker in source.Likers)
                media.Likers.Add(_userPreviewConverter.Convert(liker));

        if (source.PreviewComments?.Count > 0)
            foreach (var comment in source.PreviewComments)
                media.PreviewComments.Add(_commentConverter.Convert(comment));

        if (source.CarouselMedia != null)
            media.Carousel = _carouselConverter.Convert(source.CarouselMedia);

        if (source.UserTagList?.In?.Count > 0)
            foreach (var tag in source.UserTagList.In)
                media.UserTags.Add(_userTagConverter.Convert(tag));

        if (source.Images?.Candidates == null) return media;

        foreach (var image in source.Images.Candidates)
            media.Images.Add(new InstaImage(image.Url, image.Width, image.Height));

        if (source.Videos == null) return media;

        foreach (var video in source.Videos)
            media.Videos.Add(new InstaVideo(video.Url, video.Width, video.Height, video.Type));

        return media;

        return media;
    }
}