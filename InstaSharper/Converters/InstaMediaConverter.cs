﻿using System;
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
                DeviceTimeStamp = DateTimeHelper.UnixTimestampToDateTime(SourceObject.DeviceTimeStampUnixLike),
                HasLiked = SourceObject.HasLiked,
                PhotoOfYou = SourceObject.PhotoOfYou,
                TrackingToken = SourceObject.TrackingToken,
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
                media.Carousel = ConvertersFabric.Instance.GetCarouselConverter(SourceObject.CarouselMedia).Convert();
            if (SourceObject.User != null)
                media.User = ConvertersFabric.Instance.GetUserConverter(SourceObject.User).Convert();
            if (SourceObject.Caption != null)
                media.Caption = ConvertersFabric.Instance.GetCaptionConverter(SourceObject.Caption).Convert();
            if (SourceObject.NextMaxId != null) media.NextMaxId = SourceObject.NextMaxId;
            if (SourceObject.Likers != null && SourceObject.Likers?.Count > 0)
                foreach (var liker in SourceObject.Likers)
                    media.Likers.Add(ConvertersFabric.Instance.GetUserShortConverter(liker).Convert());
            if (SourceObject.UserTagList?.In != null && SourceObject.UserTagList?.In?.Count > 0)
                foreach (var tag in SourceObject.UserTagList.In)
                    media.Tags.Add(ConvertersFabric.Instance.GetUserTagConverter(tag).Convert());
            if (SourceObject.PreviewComments != null)
                foreach (var comment in SourceObject.PreviewComments)
                    media.PreviewComments.Add(ConvertersFabric.Instance.GetCommentConverter(comment).Convert());
            if (SourceObject.Location != null)
                media.Location = ConvertersFabric.Instance.GetLocationConverter(SourceObject.Location).Convert();
            if (SourceObject.Images?.Candidates == null) return media;
            foreach (var image in SourceObject.Images.Candidates)
                media.Images.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
            if (SourceObject.Videos == null) return media;
            foreach (var video in SourceObject.Videos)
                media.Videos.Add(new InstaVideo(video.Url, int.Parse(video.Width), int.Parse(video.Height),
                    video.Type));
            return media;
        }
    }
}