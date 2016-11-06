using InstagramApi.Classes;
using InstagramApi.Classes.Web;
using InstagramApi.ResponseWrappers.Web;

namespace InstagramApi.Converters
{
    internal class InstaFeedConverter : IObjectConverter<InstaUserFeed, InstaFeedResponse>
    {
        public InstaFeedResponse SourceObject { get; set; }

        public InstaUserFeed Convert()
        {
            var feed = new InstaUserFeed();


            return feed;
        }
    }
}