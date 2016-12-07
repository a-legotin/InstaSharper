using System;

namespace InstaSharper.Classes.Models
{
    public class InstaStory
    {
        public bool CanReply { get; set; }

        public DateTime ExpiringAt { get; set; }

        public InstaUser User { get; set; }

        public string SourceToken { get; set; }

        public bool Seen { get; set; }

        public string LatestReelMedia { get; set; }

        public string Id { get; set; }

        public int RankedPosition { get; set; }

        public int SeenRankedPosition { get; set; }
    }
}