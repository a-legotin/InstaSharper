using InstagramAPI.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaCurrentUserResponse : BaseStatusResponse
    {
        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }
    }
}