using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class InstaFeedResponseMediaItem
    {
        [JsonProperty("page_info")]
        public InstaFeedPageInfoResponse PageInfo { get; set; }

        [JsonProperty("nodes")]
        public List<InstaResponseMedia> Nodes { get; set; }
    }
}