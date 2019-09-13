﻿using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaInboxMedia
    {
        public List<InstaImage> Images { get; set; } = new List<InstaImage>();
        public long OriginalWidth { get; set; }
        public long OriginalHeight { get; set; }
        public InstaMediaType MediaType { get; set; }
    }
}