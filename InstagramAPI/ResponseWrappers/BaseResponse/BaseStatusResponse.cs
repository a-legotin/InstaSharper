using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers.BaseResponse
{
    public class BaseStatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}