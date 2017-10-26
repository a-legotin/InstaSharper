using System;
using System.Collections.Generic;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaExploreFeedConverter : IObjectConverter<InstaExploreFeed, InstaExploreFeedResponse>
    {
        public InstaExploreFeedResponse SourceObject { get; set; }

        public InstaExploreFeed Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException("SourceObject");

            List<InstaMedia> ConvertMedia(List<InstaMediaItemResponse> mediasResponse)
            {
                var medias = new List<InstaMedia>();
                foreach (var instaUserFeedItemResponse in mediasResponse)
                {
                    if (instaUserFeedItemResponse?.Type != 0) continue;
                    var feedItem = ConvertersFabric.GetSingleMediaConverter(instaUserFeedItemResponse).Convert();
                    medias.Add(feedItem);
                }
                return medias;
            }

            var feed = new InstaExploreFeed
            {
                StoryTray = ConvertersFabric.GetStoryTrayConverter(SourceObject.Items.StoryTray).Convert(),
                Channel = ConvertersFabric.GetChannelConverter(SourceObject.Items.Channel).Convert()
            };
            feed.Medias.AddRange(ConvertMedia(SourceObject.Items.Medias));
            feed.Medias.PageSize = feed.Medias.Count;

            return feed;
        }
    }
}