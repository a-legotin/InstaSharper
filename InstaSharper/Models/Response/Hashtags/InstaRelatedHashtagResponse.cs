using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaRelatedHashtagResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}