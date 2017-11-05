using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class FollowedByResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}