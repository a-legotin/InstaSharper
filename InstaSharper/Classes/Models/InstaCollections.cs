using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaCollections
    {
        public bool MoreCollectionsAvailable { get; set; }

        public int Pages { get; set; } = 0;

        public List<InstaCollectionItem> Items { get; set; }
    }
}