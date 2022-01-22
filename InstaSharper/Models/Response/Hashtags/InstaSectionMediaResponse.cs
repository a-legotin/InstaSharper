using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaSectionMediaResponse
{
    [JsonPropertyName("layout_type")]
    public string LayoutType { get; set; }

    [JsonPropertyName("layout_content")]
    public InstaSectionMediaLayoutContentResponse LayoutContent { get; set; }

    [JsonPropertyName("feed_type")]
    public string FeedType { get; set; }

    [JsonPropertyName("explore_item_info")]
    public InstaSectionMediaExploreItemInfoResponse ExploreItemInfo { get; set; }
}