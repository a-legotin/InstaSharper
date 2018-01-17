using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaUserInfoContainerResponse : BaseStatusResponse
    {
        [JsonProperty("user")] public InstaUserInfoResponse User { get; set; }
    }
}