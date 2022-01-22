using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaDirectHashtagResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("media_count")]
    public long MediaCount { get; set; }

    [JsonPropertyName("media")]
    public InstaMediaItemResponse Media { get; set; }
}