using System.Collections.Generic;
using InstaSharper.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class InstaRecentActivityResponse : BaseLoadableResponse
    {
        public bool IsOwnActivity { get; set; } = false;

        [JsonProperty("stories")]
        public List<InstaRecentActivityFeedResponse> Stories { get; set; }
            = new List<InstaRecentActivityFeedResponse>();
    }
}