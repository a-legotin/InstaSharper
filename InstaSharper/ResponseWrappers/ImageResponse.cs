using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class ImageResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }
    }
}