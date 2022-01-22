using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaUserPkResponse
{
    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("is_private")]
    public bool IsPrivate { get; set; }
}