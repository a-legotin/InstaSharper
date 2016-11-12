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
            var caption = new InstaCaption();
            caption.Pk = SourceObject.Pk;
            caption.CreatedAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUnixLike);
            caption.CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(SourceObject.CreatedAtUtcUnixLike);
            caption.MediaId = SourceObject.MediaId;
            caption.Text = SourceObject.Text;
            caption.User = ConvertersFabric.GetUserConverter(SourceObject.User).Convert();
            caption.UserId = SourceObject.UserId;
            return caption;
        }
    }
}