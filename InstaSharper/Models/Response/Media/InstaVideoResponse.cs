using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Media;

public class InstaVideoResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }
}