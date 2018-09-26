using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes
{
    public class CreationResponse : BaseStatusResponse
    {
        [JsonProperty("account_created")]
        public bool AccountCreated { get; set; }

        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
    }
}