using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaHashtagResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}