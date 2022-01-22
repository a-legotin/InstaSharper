using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Hashtags;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Story;

internal class InstaReelMentionResponse
{
    [JsonPropertyName("rotation")]
    public double Rotation { get; set; }

    [JsonPropertyName("height")]
    public double Height { get; set; }

    [JsonPropertyName("hashtag")]
    public InstaHashtagResponse Hashtag { get; set; }

    [JsonPropertyName("is_pinned")]
    public int IsPinned { get; set; }

    [JsonPropertyName("is_hidden")]
    public int IsHidden { get; set; }

    [JsonPropertyName("width")]
    public double Width { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortResponse User { get; set; }

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    [JsonPropertyName("z")]
    public double Z { get; set; }
}