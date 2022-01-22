using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryCTAContainerResponse
{
    [JsonPropertyName("links")]
    public InstaStoryCTAResponse[] Links { get; set; }
}