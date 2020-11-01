using Newtonsoft.Json;

namespace InstaSharper.Models.Response
{
    internal class BadStatusErrorsResponse : BaseStatusResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}