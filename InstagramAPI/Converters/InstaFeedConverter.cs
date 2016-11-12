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
                var feedItem = ConvertersFabric.GetSingleMediaConverter(instaUserFeedItemResponse).Convert();
                feed.Items.Add(feedItem);
            }
            return feed;
        }
    }
}