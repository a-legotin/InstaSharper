using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Helpers;

namespace InstaSharper.Converters
{
    internal class InstaStoryItemConverter : IObjectConverter<InstaStoryItem, InstaStoryItemResponse>
    {
        public InstaStoryItemResponse SourceObject { get; set; }

        public InstaStoryItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var instaStory = new InstaStoryItem
            {
                CanViewerSave = SourceObject.CanViewerSave,
                CaptionIsEdited = SourceObject.CaptionIsEdited,
                CaptionPosition = SourceObject.CaptionPosition,
                ClientCacheKey = SourceObject.ClientCacheKey,
                Code = SourceObject.Code,
                CommentCount = SourceObject.CommentCount,
                CommentsDisabled = SourceObject.CommentsDisabled,
                ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.ExpiringAt),
                FilterType = SourceObject.FilterType,
                HasAudio = SourceObject.HasAudio,
                HasLiked = SourceObject.HasLiked,
                HasMoreComments = SourceObject.HasMoreComments,
                Id = SourceObject.Id,
                IsReelMedia = SourceObject.IsReelMedia,
                LikeCount = SourceObject.LikeCount,
                MaxNumVisiblePreviewComments = SourceObject.MaxNumVisiblePreviewComments,
                MediaType = SourceObject.MediaType,
                OriginalHeight = SourceObject.OriginalHeight,
                OriginalWidth = SourceObject.OriginalWidth,
                PhotoOfYou = SourceObject.PhotoOfYou,
                Pk = SourceObject.Pk,
                TakenAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.TakenAt),
                TrackingToken = SourceObject.TrackingToken,
                VideoDuration = SourceObject.VideoDuration,
                VideoVersions = SourceObject.VideoVersions
            };

            if (SourceObject.User != null)
                instaStory.User = ConvertersFabric.GetUserConverter(SourceObject.User).Convert();

            if (SourceObject.Caption != null)
                instaStory.Caption = ConvertersFabric.GetCaptionConverter(SourceObject.Caption).Convert();

            if (SourceObject.Likers?.Count > 0)
                foreach (var liker in SourceObject.Likers)
                    instaStory.Likers.Add(ConvertersFabric.GetUserConverter(liker).Convert());

            if (SourceObject.CarouselMedia != null)
                instaStory.CarouselMedia = ConvertersFabric.GetCarouselConverter(SourceObject.CarouselMedia).Convert();

            if (SourceObject.UserTags?.In?.Count > 0)
                foreach (var tag in SourceObject.UserTags.In)
                    instaStory.UserTags.Add(ConvertersFabric.GetUserTagConverter(tag).Convert());

            if (SourceObject.ImageVersions?.Candidates != null)
                foreach (var image in SourceObject.ImageVersions.Candidates)
                    instaStory.Images.Add(new MediaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));

            return instaStory;
        }
    }
}