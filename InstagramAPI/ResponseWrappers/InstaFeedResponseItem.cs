using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaFeedResponseItem
    {
        [JsonProperty("media")]
        public InstaFeedResponseMediaItem Media { get; set; }
    }
}