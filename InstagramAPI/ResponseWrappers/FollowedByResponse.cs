using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class FollowedByResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}