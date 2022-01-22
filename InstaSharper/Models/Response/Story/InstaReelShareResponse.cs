using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaReelShareResponse
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("reel_owner_id")]
    public long ReelOwnerId { get; set; }

    [JsonPropertyName("is_reel_persisted")]
    public bool? IsReelPersisted { get; set; }

    [JsonPropertyName("reel_type")]
    public string ReelType { get; set; }

    [JsonPropertyName("media")]
    public InstaStoryItemResponse Media { get; set; }
}