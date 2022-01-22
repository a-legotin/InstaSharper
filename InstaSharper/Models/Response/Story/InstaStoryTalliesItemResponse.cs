using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

public class InstaStoryTalliesItemResponse
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("font_size")]
    public float FontSize { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}