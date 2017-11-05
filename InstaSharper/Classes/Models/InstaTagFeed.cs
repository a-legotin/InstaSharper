using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaTagFeed : InstaFeed
    {
        public List<InstaMedia> RankedMedias { get; set; } = new List<InstaMedia>();
    }
}