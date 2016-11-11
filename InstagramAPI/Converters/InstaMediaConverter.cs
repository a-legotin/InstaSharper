using System;
using InstagramAPI.Classes;
using InstagramAPI.Helpers;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    public class InstaMediaConverter : IObjectConverter<InstaMedia, InstaMediaItemResponse>
    {
        public InstaMediaItemResponse SourceObject { get; set; }

        public InstaMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var media = new InstaMedia
            {
                Code = SourceObject.Code,
                Date = DateTimeHelper.UnixTimestampToDateTime(double.Parse(SourceObject.TakenAtUnixLike)),
                InstaIdentifier = SourceObject.InstaIdentifier
            };
            var userConverter = ConvertersFabric.GetUserConverter(SourceObject.User);
            media.Owner = userConverter.Convert();
            return media;
        }
    }
}