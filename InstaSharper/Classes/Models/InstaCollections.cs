using System;
using System.Collections.Generic;
using System.Text;
using InstaSharper.Classes.ResponseWrappers;
using Newtonsoft.Json;

namespace InstaSharper.Classes.Models
{
    public class InstaCollections
    {
        public bool MoreCollectionsAvailable { get; set; }

        public int Pages { get; set; } = 0;

        public List<InstaCollectionItem> Items { get; set; }
    }
}
