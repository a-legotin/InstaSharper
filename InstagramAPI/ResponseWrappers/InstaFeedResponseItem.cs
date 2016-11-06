using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers.Web
{
    public class InstaFeedResponseItem
    {
        [JsonProperty("media")]
        public InstaFeedResponseMediaItem Media { get; set; }
    }
}