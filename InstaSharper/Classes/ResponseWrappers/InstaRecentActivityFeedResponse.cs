using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaRecentActivityFeedResponse
    {
        [JsonProperty("args")] public InstaRecentActivityStoryItemResponse Args { get; set; }

        [JsonProperty("story_type")] public int Type { get; set; }

        [JsonProperty("pk")] public string Pk { get; set; }
    }
}