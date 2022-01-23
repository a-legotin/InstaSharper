using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Base;

internal class BaseStatusResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    public HttpResponseHeaders ResponseHeaders { get; set; }

    public bool IsOk()
    {
        return !string.IsNullOrEmpty(Status) && Status.ToLower() == "ok";
    }
}