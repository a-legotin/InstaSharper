using System;
using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaStory
    {
        public bool CanReply { get; set; }

        public DateTime ExpiringAt { get; set; }

        public InstaUser User { get; set; }

        public string SourceToken { get; set; }

        public double Seen { get; set; }

        public string LatestReelMedia { get; set; }

        public string Id { get; set; }

        public int RankedPosition { get; set; }

        public bool Muted { get; set; }

        public int SeenRankedPosition { get; set; }

        public List<InstaStoryItem> Items { get; set; } = new List<InstaStoryItem>();

        public int PrefetchCount { get; set; }

        public string SocialContext { get; set; }
    }
}