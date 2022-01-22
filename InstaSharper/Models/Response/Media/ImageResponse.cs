using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Media;

public class ImageResponse
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}