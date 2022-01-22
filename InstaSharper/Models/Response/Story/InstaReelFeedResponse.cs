using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Story;

internal class InstaReelFeedResponse
{
    [JsonPropertyName("has_besties_media")]
    public long HasBestiesMedia { get; set; }

    [JsonPropertyName("prefetch_count")]
    public long PrefetchCount { get; set; }

    [JsonPropertyName("can_reshare")]
    public string CanReshare { get; set; }

    [JsonPropertyName("can_reply")]
    public bool CanReply { get; set; }

    [JsonPropertyName("expiring_at")]
    public long ExpiringAt { get; set; }

    [JsonPropertyName("items")]
    public List<InstaStoryItemResponse> Items { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("latest_reel_media")]
    public long? LatestReelMedia { get; set; }

    [JsonPropertyName("seen")]
    public long? Seen { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortFriendshipFullResponse User { get; set; }
}