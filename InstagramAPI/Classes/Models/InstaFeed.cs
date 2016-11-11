using System.Collections.Generic;

namespace InstagramAPI.Classes.Models
{
    public class InstaFeed
    {
        public int FeedItemsCount => Items.Count;
        public List<InstaFeedItem> Items { get; set; } = new List<InstaFeedItem>();
        public int Pages { get; set; } = 1;
    }
}