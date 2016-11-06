using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers.Web
{
    public class FollowedByResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}