using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class InstaFeedResponseItem
    {
        [JsonProperty("media")]
        public InstaFeedResponseMediaItem Media { get; set; }
    }
}