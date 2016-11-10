using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaResponseLoginAndroid
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("logged_in_user")]
        public InstaUserResponse User { get; set; }
    }
}