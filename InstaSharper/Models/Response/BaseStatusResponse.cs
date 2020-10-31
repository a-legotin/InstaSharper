using Newtonsoft.Json;

namespace InstaSharper.Models.Response
{
    internal abstract class BaseStatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        public bool IsOk() => !string.IsNullOrEmpty(Status) && Status.ToLower() == "ok";
    }
}