using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers.BaseResponse
{
    public class BaseLoadableResponse : BaseStatusResponse
    {
        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }

        [JsonProperty("num_results")]
        public int ResultsCount { get; set; }

        [JsonProperty("auto_load_more_enabled")]
        public bool AutoLoadMoreEnabled { get; set; }
    }
}