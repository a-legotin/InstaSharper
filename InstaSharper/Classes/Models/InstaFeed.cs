using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaFeed
    {
        public int FeedItemsCount => Items.Count;
        public List<InstaMedia> Items { get; set; } = new List<InstaMedia>();
        public int Pages { get; set; } = 1;
    }
}