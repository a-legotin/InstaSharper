using InstaSharper.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    public class InstaCurrentUserResponse : BaseStatusResponse
    {
        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }
    }
}