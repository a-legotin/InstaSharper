using System;
using InstagramAPI.Classes;
using InstagramAPI.Helpers;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    public class InstaMediaConverter : IObjectConverter<InstaMedia, InstaResponseMedia>
    {
        public InstaResponseMedia SourceObject { get; set; }

        public InstaMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var media = new InstaMedia
            {
                CaptionIsEdited = SourceObject.CaptionIsEdited,
                Code = SourceObject.Code,
                Date = DateTimeHelper.UnixTimestampToDateTime(double.Parse(SourceObject.Date)),
                Dimensions =
                    new Dimensions {Height = SourceObject.Dimensions.Height, Width = SourceObject.Dimensions.Width},
                ImageSourceLink = SourceObject.ImageSourceLink,
                InstaIdentifier = SourceObject.InstaIdentifier,
                IsAdvertisement = SourceObject.IsAdvertisement,
                IsVideo = SourceObject.IsVideo,
                Location = SourceObject.Location
            };

            var userConverter = ConvertersFabric.GetUserConverter(SourceObject.Owner);
            media.Owner = userConverter.Convert();
            return media;
        }
    }
}