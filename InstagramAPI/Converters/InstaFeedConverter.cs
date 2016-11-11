using InstagramAPI.Classes.Models;
using InstagramAPI.ResponseWrappers;

namespace InstagramAPI.Converters
{
    internal class InstaFeedConverter : IObjectConverter<InstaFeed, InstaFeedResponse>
    {
        public InstaFeedResponse SourceObject { get; set; }

        public InstaFeed Convert()
        {
            var feed = new InstaFeed();

            foreach (var instaUserFeedItemResponse in SourceObject.Items)
            {
                if (instaUserFeedItemResponse.Type != 0) continue;

                var feedItem = new InstaFeedItem
                {
                    InstaIdentifier = instaUserFeedItemResponse.InstaIdentifier,
                    Caption = instaUserFeedItemResponse.Caption.Text,
                    Code = instaUserFeedItemResponse.Code
                };
                feed.Items.Add(feedItem);
            }
            return feed;
        }
    }
}