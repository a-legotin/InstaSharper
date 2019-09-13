using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaPermalinkResponse : BaseStatusResponse
    {
        [JsonProperty("permalink")] public string Permalink { get; set; }
    }
}