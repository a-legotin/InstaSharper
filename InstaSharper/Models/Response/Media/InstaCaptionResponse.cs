using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Media;

internal class InstaCaptionResponse : BaseStatusResponse
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("created_at_utc")]
    public long CreatedAtUtcUnixLike { get; set; }

    [JsonPropertyName("created_at")]
    public long CreatedAtUnixLike { get; set; }

    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortResponse User { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("media_id")]
    public long MediaId { get; set; }

    [JsonPropertyName("share_enabled")]
    public bool IsShareEnabled { get; set; }

    [JsonPropertyName("pk")]
    public long Pk { get; set; }
}