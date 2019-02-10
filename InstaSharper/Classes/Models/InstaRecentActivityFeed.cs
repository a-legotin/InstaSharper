using System;
using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaRecentActivityFeed
    {
        public string MediaId { get; set; }

        public long ProfileId { get; set; }

        public string ProfileName { get; set; }

        public string ProfileImage { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Text { get; set; }

        public List<InstaLink> Links { get; set; } = new List<InstaLink>();

        public InstaInlineFollow InlineFollow { get; set; }

        public InstaActivityFeedType Type { get; set; }

        public string Pk { get; set; }
    }
}