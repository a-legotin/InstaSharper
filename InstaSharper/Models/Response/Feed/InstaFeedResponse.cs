using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Models.Response.Feed;

internal class InstaFeedResponse : BaseLoadableResponse
{
    [JsonPropertyName("is_direct_v2_enabled")]
    public bool IsDirectV2Enabled { get; set; }

    [JsonPropertyName("session_id")]
    public string SessionId { get; set; }

    [JsonPropertyName("feed_items")]
    public List<InstaMediaItemResponseContainer> Items { get; set; } = new();
}