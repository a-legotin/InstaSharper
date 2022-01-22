using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Location;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryLocationResponse
{
    [JsonPropertyName("rotation")]
    public double Rotation { get; set; }

    [JsonPropertyName("is_pinned")]
    public double IsPinned { get; set; }

    [JsonPropertyName("height")]
    public double Height { get; set; }

    [JsonPropertyName("location")]
    public InstaPlaceShortResponse Location { get; set; }

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("width")]
    public double Width { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    [JsonPropertyName("z")]
    public double Z { get; set; }

    [JsonPropertyName("is_hidden")]
    public double IsHidden { get; set; }
}