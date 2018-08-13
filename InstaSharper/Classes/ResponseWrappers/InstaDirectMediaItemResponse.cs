using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaDirectMediaItemResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("media")]
        public InstaMediaItemResponse Media { get; set; }
    }
}
