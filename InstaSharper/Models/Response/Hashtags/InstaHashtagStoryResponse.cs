using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Story;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaHashtagStoryResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("latest_reel_media")]
    public int LatestReelMedia { get; set; }

    [JsonPropertyName("expiring_at")]
    public long ExpiringAt { get; set; }

    [JsonPropertyName("can_reply")]
    public bool CanReply { get; set; }

    [JsonPropertyName("can_reshare")]
    public bool CanReshare { get; set; }

    [JsonPropertyName("reel_type")]
    public string ReelType { get; set; }

    [JsonPropertyName("owner")]
    public InstaHashtagOwnerResponse Owner { get; set; }

    [JsonPropertyName("items")]
    public List<InstaStoryItemResponse> Items { get; set; }

    [JsonPropertyName("prefetch_count")]
    public int PrefetchCount { get; set; }

    [JsonPropertyName("unique_integer_reel_id")]
    public long UniqueIntegerReelId { get; set; }

    [JsonPropertyName("muted")]
    public bool Muted { get; set; }
}