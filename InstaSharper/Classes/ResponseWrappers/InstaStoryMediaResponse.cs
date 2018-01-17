using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaStoryMediaResponse
    {
        [JsonProperty("media")] public InstaStoryItemResponse Media { get; set; }
    }
}