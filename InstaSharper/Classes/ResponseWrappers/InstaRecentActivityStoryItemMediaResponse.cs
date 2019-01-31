using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaRecentActivityStoryItemMediaResponse
    {
        [JsonProperty("id")] public string MediaId { get; set; }
    }
}