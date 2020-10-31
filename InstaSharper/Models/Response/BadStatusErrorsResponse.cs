using Newtonsoft.Json;

namespace InstaSharper.Models.Response
{
    internal class BadStatusErrorsResponse : BaseStatusResponse
    {
        [JsonProperty("message")]
        public MessageErrorsResponse Message { get; set; }
    }
}