using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryFeedResponse : BaseStatusResponse
{
    [JsonPropertyName("face_filter_nux_version")]
    public int FaceFilterNuxVersion { get; set; }

    [JsonPropertyName("has_new_nux_story")]
    public bool HasNewNuxStory { get; set; }

    [JsonPropertyName("story_ranking_token")]
    public string StoryRankingToken { get; set; }

    [JsonPropertyName("sticker_version")]
    public int StickerVersion { get; set; }

    [JsonPropertyName("tray")]
    public List<InstaReelFeedResponse> Tray { get; set; }

    //todo add live items
    //[JsonPropertyName("broadcasts")] public List<InstaBroadcastResponse> Broadcasts { get; set; }

    //[JsonPropertyName("post_live")] public InstaBroadcastAddToPostLiveContainerResponse PostLives { get; set; }
}