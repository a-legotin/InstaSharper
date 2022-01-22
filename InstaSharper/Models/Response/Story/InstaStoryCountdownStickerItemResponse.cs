using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryCountdownStickerItemResponse
{
    [JsonPropertyName("countdown_id")]
    public long CountdownId { get; set; }

    [JsonPropertyName("end_ts")]
    public long? EndTime { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("text_color")]
    public string TextColor { get; set; }

    [JsonPropertyName("start_background_color")]
    public string StartBackgroundColor { get; set; }

    [JsonPropertyName("end_background_color")]
    public string EndBackgroundColor { get; set; }

    [JsonPropertyName("digit_color")]
    public string DigitColor { get; set; }

    [JsonPropertyName("digit_card_color")]
    public string DigitCardColor { get; set; }

    [JsonPropertyName("following_enabled")]
    public bool? FollowingEnabled { get; set; }

    [JsonPropertyName("is_owner")]
    public bool? IsOwner { get; set; }

    [JsonPropertyName("viewer_is_following")]
    public bool? ViewerIsFollowing { get; set; }
}