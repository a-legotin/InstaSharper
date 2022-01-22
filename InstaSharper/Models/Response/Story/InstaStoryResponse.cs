using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryResponse
{
    [JsonPropertyName("taken_at")]
    public long TakenAtUnixLike { get; set; }

    [JsonPropertyName("can_reply")]
    public bool CanReply { get; set; }

    [JsonPropertyName("expiring_at")]
    public long ExpiringAt { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortFriendshipFullResponse User { get; set; }

    [JsonPropertyName("owner")]
    public InstaUserShortResponse Owner { get; set; }

    [JsonPropertyName("source_token")]
    public string SourceToken { get; set; }

    [JsonPropertyName("seen")]
    public long? Seen { get; set; }

    [JsonPropertyName("latest_reel_media")]
    public string LatestReelMedia { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("ranked_position")]
    public int RankedPosition { get; set; }

    [JsonPropertyName("muted")]
    public bool Muted { get; set; }

    [JsonPropertyName("seen_ranked_position")]
    public int SeenRankedPosition { get; set; }

    [JsonPropertyName("items")]
    public List<InstaStoryItemResponse> Items { get; set; }

    [JsonPropertyName("prefetch_count")]
    public int PrefetchCount { get; set; }

    [JsonPropertyName("social_context")]
    public string SocialContext { get; set; }


    [JsonPropertyName("client_cache_key")]
    public string ClientCacheKey { get; set; }

    [JsonPropertyName("caption_position")]
    public double? CaptionPosition { get; set; }

    [JsonPropertyName("is_reel_media")]
    public bool IsReelMedia { get; set; }

    [JsonPropertyName("video_duration")]
    public double? VideoDuration { get; set; }

    [JsonPropertyName("caption_is_edited")]
    public bool CaptionIsEdited { get; set; }

    [JsonPropertyName("photo_of_you")]
    public bool PhotoOfYou { get; set; }

    [JsonPropertyName("can_viewer_save")]
    public bool CanViewerSave { get; set; }

    [JsonPropertyName("imported_taken_at")]
    public long ImportedTakenAt { get; set; }

    [JsonPropertyName("can_reshare")]
    public bool CanReshare { get; set; }

    [JsonPropertyName("supports_reel_reactions")]
    public bool SupportsReelReactions { get; set; }

    [JsonPropertyName("has_shared_to_fb")]
    public bool HasSharedToFb { get; set; }

    [JsonPropertyName("story_hashtags")]
    public List<InstaReelMentionResponse> StoryHashtags { get; set; }

    [JsonPropertyName("story_locations")]
    public List<InstaStoryLocationResponse> StoryLocation { get; set; }

    [JsonPropertyName("show_one_tap_fb_share_tooltip")]
    public bool ShowOneTapFbShareTooltip { get; set; }
}