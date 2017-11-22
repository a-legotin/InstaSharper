using System;
using System.Collections.Generic;
using System.Text;
using InstaSharper.Classes.ResponseWrappers;
using Newtonsoft.Json;

namespace InstaSharper.Classes.Models
{
    public class InstaCoverMedia
    {
        public long Id { get; set; }

        public List<InstaImage> ImageVersions { get; set; }

        public int MediaType { get; set; }

        public int OriginalHeight { get; set; }

        public int OriginalWidth { get; set; }
    }
}
