using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    public class FollowedByResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}