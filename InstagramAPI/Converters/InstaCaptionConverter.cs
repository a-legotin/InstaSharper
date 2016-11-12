using InstagramAPI.Classes.Models;
using InstagramAPI.Helpers;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    public class InstaCaptionConverter : IObjectConverter<InstaCaption, InstaCaptionResponse>
    {
        public InstaCaptionResponse SourceObject { get; set; }

        public InstaCaption Convert()
        {
            var caption = new InstaCaption
            {
                Pk = SourceObject.Pk,
                CreatedAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUnixLike),
                CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUtcUnixLike),
                MediaId = SourceObject.MediaId,
                Text = SourceObject.Text,
                User = ConvertersFabric.GetUserConverter(SourceObject.User).Convert(),
                UserId = SourceObject.UserId
            };
            return caption;
        }
    }
}