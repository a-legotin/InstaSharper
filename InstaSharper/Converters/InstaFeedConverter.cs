using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaFeedConverter : IObjectConverter<InstaFeed, InstaFeedResponse>
    {
        public InstaFeedResponse SourceObject { get; set; }

        public InstaFeed Convert()
        {
            if (SourceObject?.Items == null)
                throw new ArgumentNullException("InstaFeedResponse or its Items");
            var feed = new InstaFeed();
            foreach (var instaUserFeedItemResponse in SourceObject.Items)
            {
                if (instaUserFeedItemResponse?.Type != 0) continue;
                var feedItem = ConvertersFabric.Instance.GetSingleMediaConverter(instaUserFeedItemResponse).Convert();
                feed.Medias.Add(feedItem);
            }

            feed.NextId = SourceObject.NextMaxId;
            return feed;
        }
    }
}