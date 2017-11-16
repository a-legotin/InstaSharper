using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaTagFeedResponse : InstaMediaListResponse
    {
        [JsonProperty("ranked_items")]
        public List<InstaMediaItemResponse> RankedItems { get; set; } = new List<InstaMediaItemResponse>();
    }
}