using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.Media;
using InstaSharper.Models.Response.Story;

namespace InstaSharper.Models.Response.Location;

internal class InstaLocationFeedResponse : BaseLoadableResponse
{
    [JsonPropertyName("ranked_items")]
    public List<InstaMediaItemResponse> RankedItems { get; set; } = new();

    [JsonPropertyName("items")]
    public List<InstaMediaItemResponse> Items { get; set; } = new();

    [JsonPropertyName("story")]
    public InstaStoryResponse Story { get; set; }

    [JsonPropertyName("media_count")]
    public long MediaCount { get; set; }

    [JsonPropertyName("location")]
    public InstaLocationResponse Location { get; set; }
}