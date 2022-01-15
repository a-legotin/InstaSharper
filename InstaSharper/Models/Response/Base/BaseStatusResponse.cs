
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Base
{
    internal abstract class BaseStatusResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        public bool IsOk() => !string.IsNullOrEmpty(Status) && Status.ToLower() == "ok";
    }
}