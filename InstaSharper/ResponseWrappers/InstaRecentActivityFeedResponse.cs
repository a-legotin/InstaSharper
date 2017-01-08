using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class InstaRecentActivityFeedResponse
    {
        [JsonProperty("args")]
        public InstaRecentActivityStoryItemResponse Args { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("pk")]
        public string Pk { get; set; }
    }
}