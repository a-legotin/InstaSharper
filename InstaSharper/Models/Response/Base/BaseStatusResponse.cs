
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Base
{
    internal abstract class BaseStatusResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        public HttpResponseHeaders ResponseHeaders { get; set; }

        public bool IsOk() => !string.IsNullOrEmpty(Status) && Status.ToLower() == "ok";
    }
}