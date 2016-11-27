using System;
using System.Collections.Generic;
using InstagramAPI.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaFeedResponse : BaseLoadableResponse
    {
        [JsonProperty("is_direct_v2_enabled")]
        public bool IsDirectV2Enabled { get; set; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        public List<InstaMediaItemResponse> Items { get; set; } = new List<InstaMediaItemResponse>();
    }
}