using InstaSharper.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class InstaCurrentUserResponse : BaseStatusResponse
    {
        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }
    }
}