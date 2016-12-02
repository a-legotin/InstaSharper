using System.Collections.Generic;
using InstaSharper.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    public class InstaMediaListResponse : BaseLoadableResponse
    {
        [JsonProperty("items")]
        public List<InstaMediaItemResponse> Items { get; set; }
    }
}