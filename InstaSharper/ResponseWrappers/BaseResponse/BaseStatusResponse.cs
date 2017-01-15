using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers.BaseResponse
{
    public class BaseStatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}