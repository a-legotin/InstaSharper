using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaFeedResponseMediaItem
    {
        [JsonProperty("page_info")]
        public InstaFeedPageInfoResponse PageInfo { get; set; }

        [JsonProperty("nodes")]
        public List<InstaResponseMedia> Nodes { get; set; }
    }
}