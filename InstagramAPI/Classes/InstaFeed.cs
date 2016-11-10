using System.Collections.Generic;

namespace InstagramAPI.Classes
{
    public class InstaFeed
    {
        public int FeedItemsCount => Items.Count;
        public List<InstaFeedItem> Items { get; set; } = new List<InstaFeedItem>();
        public int Pages { get; set; } = 0;
    }
}