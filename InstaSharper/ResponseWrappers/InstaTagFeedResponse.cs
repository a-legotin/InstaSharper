using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    public class InstaTagFeedResponse
    {
        [JsonProperty("ranked_items")]
        public List<InstaMediaItemResponse> Items { get; set; }
    }
}