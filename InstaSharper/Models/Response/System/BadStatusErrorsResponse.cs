using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;

namespace InstaSharper.Models.Response.System
{
    internal class BadStatusErrorsResponse : BaseStatusResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}