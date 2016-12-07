using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers.BaseResponse
{
    internal class BaseStatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}