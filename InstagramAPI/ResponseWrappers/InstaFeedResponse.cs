using System.Collections.Generic;
using InstagramApi.ResponseWrappers.Android;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers.Web
{
    public class InstaFeedResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("num_results")]
        public int ResultsCount { get; set; }

        [JsonProperty("is_direct_v2_enabled")]
        public bool IsDirectV2Enabled { get; set; }

        [JsonProperty("auto_load_more_enabled")]
        public bool AutoLoadMoreEnabled { get; set; }

        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }

        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }

        [JsonProperty("items")]
        public List<InstaUserFeedItemResponse> Items { get; set; }
    }
}