using System.Linq;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Story;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.Media;
using InstaSharper.Models.Response.Story;
using InstaSharper.Models.Response.User;
using InstaSharper.Utils;

namespace InstaSharper.Infrastructure.Converters.Feed;

internal class StoryItemConverter : IObjectConverter<InstaStoryItem, InstaStoryItemResponse>
{
    private readonly IObjectConverter<InstaCaption, InstaCaptionResponse> _captionConverter;
    private readonly IObjectConverter<InstaUserShort, InstaUserShortResponse> _userShortConverter;

    public StoryItemConverter(IObjectConverter<InstaCaption, InstaCaptionResponse> captionConverter,
                              IObjectConverter<InstaUserShort, InstaUserShortResponse> userShortConverter)
    {
        _captionConverter = captionConverter;
        _userShortConverter = userShortConverter;
    }

    public InstaStoryItem Convert(InstaStoryItemResponse source)
    {
        var storyItem = new InstaStoryItem
        {
            Code = source.Code,
            Id = source.Id,
            Pk = source.Pk,
            AdAction = source.AdAction,
            CanReshare = source.CanReshare,
            CaptionPosition = source.CaptionPosition,
            CommentCount = source.CommentCount,
            DeviceTimestamp = DateTimeHelper.UnixTimestampMicrosecondsToDateTime(source.DeviceTimestamp),
            ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(source.ExpiringAt),
            FilterType = source.FilterType,
            HasAudio = source.HasAudio,
            HasLiked = source.HasLiked,
            LikeCount = source.LikeCount,
            LinkText = source.LinkText,
            MediaType = (InstaMediaType)source.MediaType,
            OriginalHeight = source.OriginalHeight,
            OriginalWidth = source.OriginalWidth,
            TimezoneOffset = source.TimezoneOffset,
            TakenAt = DateTimeHelper.UnixTimestampToDateTime(source.TakenAt),
            ImportedTakenAt = DateTimeHelper.UnixTimestampToDateTime(source.ImportedTakenAt),
            VideoDuration = source.VideoDuration,
            ViewerCount = source.ViewerCount,
            ViewerCursor = source.ViewerCursor,
            CanViewerSave = source.CanViewerSave,
            CaptionIsEdited = source.CaptionIsEdited,
            CommentLikesEnabled = source.CommentLikesEnabled,
            CommentThreadingEnabled = source.CommentThreadingEnabled,
            HasMoreComments = source.HasMoreComments,
            IsReelMedia = source.IsReelMedia,
            NumberOfQualities = source.NumberOfQualities,
            OrganicTrackingToken = source.OrganicTrackingToken,
            StoryStickerIds = source.StoryStickerIds,
            PhotoOfYou = source.PhotoOfYou,
            SupportsReelReactions = source.SupportsReelReactions,
            TotalViewerCount = source.TotalViewerCount,
            VideoDashManifest = source.VideoDashManifest,
            HasSharedToFb = source.HasSharedToFb,
            ShowOneTapTooltip = source.ShowOneTapTooltip,
            StoryIsSavedToArchive = source.StoryIsSavedToArchive,
            UserPk = source.User.Pk
        };
        if (source.Caption != null)
            storyItem.Caption = _captionConverter.Convert(source.Caption);
        if (source.Images != null)
            source.Images.Candidates.ForEach(candidate =>
                storyItem.ImageList.Add(new InstaImage(candidate.Url, candidate.Width, candidate.Height)));

        if (source.VideoVersions != null)
            source.VideoVersions.ForEach(videoResponse =>
                storyItem.VideoList.Add(new InstaVideo(videoResponse.Url, videoResponse.Width, videoResponse.Height)));

        if (source.Likers?.Count > 0) storyItem.Likers.AddRange(source.Likers.Select(_userShortConverter.Convert));

        if (source.Viewers?.Count > 0) storyItem.Viewers.AddRange(source.Viewers.Select(_userShortConverter.Convert));

        return storyItem;
    }
}