using InstagramApi.ResponseWrappers.Common;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers.Android
{
    public class InstaResponseLoginAndroid
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("logged_in_user")]
        public InstaUserResponse User { get; set; }
    }
}