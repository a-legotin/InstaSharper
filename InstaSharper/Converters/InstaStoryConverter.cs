using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Helpers;

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
                ExpiringAt = SourceObject.ExpiringAt.FromUnixTimeSeconds(),
                Id = SourceObject.Id,
                LatestReelMedia = SourceObject.LatestReelMedia,
                Muted = SourceObject.Muted,
                PrefetchCount = SourceObject.PrefetchCount,
                RankedPosition = SourceObject.RankedPosition,
                Seen = (SourceObject.Seen ?? 0).FromUnixTimeSeconds(),
                SeenRankedPosition = SourceObject.SeenRankedPosition,
                SocialContext = SourceObject.SocialContext,
                SourceToken = SourceObject.SourceToken
            };
            if (SourceObject.Owner != null)
                story.Owner = ConvertersFabric.GetUserShortConverter(SourceObject.Owner).Convert();

            if (SourceObject.User != null)
                story.User = ConvertersFabric.GetUserShortConverter(SourceObject.User).Convert();

            if (SourceObject.Items != null)
                foreach (var item in SourceObject.Items)
                    story.Items.Add(ConvertersFabric.GetSingleMediaConverter(item).Convert());
            return story;
        }
    }
}