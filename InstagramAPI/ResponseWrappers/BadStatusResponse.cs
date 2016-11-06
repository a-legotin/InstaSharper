using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers.Android
{
    public class BadStatusResponse
    {
        [JsonProperty("ststus")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
    }
}