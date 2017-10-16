using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaFeedConverter : IObjectConverter<InstaFeed, InstaFeedResponse>
    {
        public InstaFeedResponse SourceObject { get; set; }

        public InstaFeed Convert()
        {
            var feed = new InstaFeed();

            foreach (var instaUserFeedItemResponse in SourceObject.Items)
            {
                if (instaUserFeedItemResponse?.Type != 0) continue;
                var feedItem = ConvertersFabric.GetSingleMediaConverter(instaUserFeedItemResponse).Convert();
                feed.Medias.Add(feedItem);
            }
            return feed;
        }
    }
}