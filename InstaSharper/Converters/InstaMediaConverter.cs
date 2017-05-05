using System;
using System.Globalization;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Helpers;

namespace InstaSharper.Converters
{
    internal class InstaMediaConverter : IObjectConverter<InstaMedia, InstaMediaItemResponse>
    {
        public InstaMediaItemResponse SourceObject { get; set; }

        public InstaMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var media = new InstaMedia
            {
                InstaIdentifier = SourceObject.InstaIdentifier,
                Code = SourceObject.Code,
                Pk = SourceObject.Pk,
                ClientCacheKey = SourceObject.ClientCacheKey,
                CommentsCount = SourceObject.CommentsCount,
                DeviceTimeStap = DateTimeHelper.UnixTimestampToDateTime(SourceObject.DeviceTimeStapUnixLike),
                HasLiked = SourceObject.HasLiked,
                PhotoOfYou = SourceObject.PhotoOfYou,
                TrakingToken = SourceObject.TrakingToken,
                TakenAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.TakenAtUnixLike),
                Height = SourceObject.Height,
                LikesCount = SourceObject.LikesCount,
                MediaType = SourceObject.MediaType,
                FilterType = SourceObject.FilterType,
                Width = SourceObject.Width,
                HasAudio = SourceObject.HasAudio,
                ViewCount = int.Parse(SourceObject.ViewCount.ToString(CultureInfo.InvariantCulture))
            };
            if (SourceObject.CarouselMedia != null)
                media.Carousel = ConvertersFabric.GetCarouselConverter(SourceObject.CarouselMedia).Convert();
            if (SourceObject.User != null) media.User = ConvertersFabric.GetUserConverter(SourceObject.User).Convert();
            if (SourceObject.Caption != null)
                media.Caption = ConvertersFabric.GetCaptionConverter(SourceObject.Caption).Convert();
            if (SourceObject.NextMaxId != null) media.NextMaxId = SourceObject.NextMaxId;
            if (SourceObject.Likers?.Count > 0)
                foreach (var liker in SourceObject.Likers)
                    media.Likers.Add(ConvertersFabric.GetUserConverter(liker).Convert());
            if (SourceObject.UserTagList?.In?.Count > 0)
                foreach (var tag in SourceObject.UserTagList.In)
                    media.Tags.Add(ConvertersFabric.GetUserTagConverter(tag).Convert());
            if (SourceObject.Images?.Candidates == null) return media;
            foreach (var image in SourceObject.Images.Candidates)
                media.Images.Add(new MediaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
            return media;
        }
    }
}