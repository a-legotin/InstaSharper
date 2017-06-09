using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaStoryMediaResponse
    {
        [JsonProperty("media")]
        public InstaStoryItemResponse Media { get; set; }
    }
}