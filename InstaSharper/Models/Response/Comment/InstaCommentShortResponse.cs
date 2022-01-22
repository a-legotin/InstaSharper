using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Comment;

internal class InstaCommentShortResponse
{
    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortResponse User { get; set; }

    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("created_at")]
    public float CreatedAt { get; set; }

    [JsonPropertyName("created_at_utc")]
    public string CreatedAtUtc { get; set; }

    [JsonPropertyName("media_id")]
    public long MediaId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("parent_comment_id")]
    public long ParentCommentId { get; set; }

    [JsonPropertyName("has_liked_comment")]
    public bool HasLikedComment { get; set; }

    [JsonPropertyName("comment_like_count")]
    public int CommentLikeCount { get; set; }
}