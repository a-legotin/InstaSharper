using System.Collections.Generic;
using InstagramAPI.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaResponsePostList : BaseLoadableResponse
    {
        [JsonProperty("items")]
        public List<InstaUserFeedItemResponse> Items { get; set; }
    }
}