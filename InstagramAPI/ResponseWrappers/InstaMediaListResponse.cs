using System.Collections.Generic;
using InstagramAPI.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaMediaListResponse : BaseLoadableResponse
    {
        [JsonProperty("items")]
        public List<InstaMediaItemResponse> Items { get; set; }
    }
}