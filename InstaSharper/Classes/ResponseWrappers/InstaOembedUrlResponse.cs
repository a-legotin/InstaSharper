using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaOembedUrlResponse
    {
        [JsonProperty("media_id")] //media_id is enough.
        public string MediaId { get; set; }
    }
}