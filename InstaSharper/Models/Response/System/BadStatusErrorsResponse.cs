using InstaSharper.Models.Response.Base;
using Newtonsoft.Json;

namespace InstaSharper.Models.Response.System
{
    internal class BadStatusErrorsResponse : BaseStatusResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}