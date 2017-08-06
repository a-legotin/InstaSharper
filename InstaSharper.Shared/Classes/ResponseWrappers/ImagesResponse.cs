using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class ImagesResponse
    {
        [JsonProperty("low_resolution")]
        public ImageResponse LowResolution { get; set; }

        [JsonProperty("thumbnail")]
        public ImageResponse Thumbnail { get; set; }

        [JsonProperty("standard_resolution")]
        public ImageResponse StandartResolution { get; set; }
    }
}