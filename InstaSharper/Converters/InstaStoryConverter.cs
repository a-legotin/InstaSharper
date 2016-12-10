using System;
using InstaSharper.Classes.Models;
using InstaSharper.Helpers;
using InstaSharper.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaStoryConverter : IObjectConverter<InstaStory, InstaStoryResponse>
    {
        public InstaStoryResponse SourceObject { get; set; }

        public InstaStory Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var story = new InstaStory
            {
                CanReply = SourceObject.CanReply,
                ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.ExpiringAt),
                Id = SourceObject.Id,
                LatestReelMedia = SourceObject.LatestReelMedia,
                RankedPosition = SourceObject.RankedPosition,
                Seen = SourceObject.Seen,
                SeenRankedPosition = SourceObject.SeenRankedPosition,
                SourceToken = SourceObject.SourceToken
            };
            if (SourceObject.User != null) story.User = ConvertersFabric.GetUserConverter(SourceObject.User).Convert();
            return story;
        }
    }
}