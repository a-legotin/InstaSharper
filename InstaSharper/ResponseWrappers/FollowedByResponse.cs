using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class FollowedByResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}