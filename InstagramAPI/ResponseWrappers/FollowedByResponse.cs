using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class FollowedByResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}