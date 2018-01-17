using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaLocationFeedResponse : BaseLoadableResponse
    {
        [JsonProperty("ranked_items")]
        public List<InstaMediaItemResponse> RankedItems { get; set; } = new List<InstaMediaItemResponse>();

        [JsonProperty("items")]
        public List<InstaMediaItemResponse> Items { get; set; } = new List<InstaMediaItemResponse>();

        [JsonProperty("story")] public InstaStoryResponse Story { get; set; }

        [JsonProperty("media_count")] public long MediaCount { get; set; }

        [JsonProperty("location")] public InstaLocationResponse Location { get; set; }
    }
}