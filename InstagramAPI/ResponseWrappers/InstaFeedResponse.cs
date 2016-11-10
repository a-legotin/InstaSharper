using System.Collections.Generic;
using InstagramAPI.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaFeedResponse : BaseLoadableResponse
    {
        [JsonProperty("is_direct_v2_enabled")]
        public bool IsDirectV2Enabled { get; set; }


        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }


        [JsonProperty("items")]
        public List<InstaUserFeedItemResponse> Items { get; set; }
    }
}