using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaPersistentSectionResponse
{
    [JsonPropertyName("layout_type")]
    public string LayoutType { get; set; }

    [JsonPropertyName("layout_content")]
    public InstaPersistentSectionLayoutContentResponse LayoutContent { get; set; }
}