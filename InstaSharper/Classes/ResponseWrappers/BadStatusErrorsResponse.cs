using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class BadStatusErrorsResponse : BaseStatusResponse
    {
        [JsonProperty("message")] public MessageErrorsResponse Message { get; set; }
    }
}