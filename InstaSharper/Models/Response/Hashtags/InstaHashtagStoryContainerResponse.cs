using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaHashtagStoryContainerResponse
{
    [JsonPropertyName("story")]
    public InstaHashtagStoryResponse Story { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}