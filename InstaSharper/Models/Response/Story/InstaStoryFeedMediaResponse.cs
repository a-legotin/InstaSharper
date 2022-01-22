using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryFeedMediaResponse
{
    [JsonPropertyName("rotation")]
    public long Rotation { get; set; }

    [JsonPropertyName("is_pinned")]
    public long IsPinned { get; set; }

    [JsonPropertyName("height")]
    public double Height { get; set; }

    [JsonPropertyName("media_id")]
    public long MediaId { get; set; }

    [JsonPropertyName("product_type")]
    public string ProductType { get; set; }

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("width")]
    public double Width { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    [JsonPropertyName("z")]
    public double Z { get; set; }
}