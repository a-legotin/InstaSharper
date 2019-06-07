using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class FbPlace
    {
        [JsonProperty("location")]
        public FbLocation Location { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("media_bundles")]
        public IList<object> MediaBundles { get; set; }
    }
}