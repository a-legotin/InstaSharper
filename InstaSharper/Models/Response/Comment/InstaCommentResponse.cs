using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Comment;

internal class InstaCommentResponse
{
    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("bit_flags")]
    public int BitFlags { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("created_at_utc")]
    public string CreatedAtUtc { get; set; }

    [JsonPropertyName("comment_like_count")]
    public int LikesCount { get; set; }

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }

    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortResponse User { get; set; }

    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("did_report_as_spam")]
    public bool DidReportAsSpam { get; set; }

    [JsonPropertyName("has_liked_comment")]
    public bool HasLikedComment { get; set; }

    [JsonPropertyName("child_comment_count")]
    public int ChildCommentCount { get; set; }

    [JsonPropertyName("has_more_tail_child_comments")]
    public bool HasMoreTailChildComments { get; set; }

    [JsonPropertyName("has_more_head_child_comments")]
    public bool HasMoreHeadChildComments { get; set; }

    [JsonPropertyName("preview_child_comments")]
    public List<InstaCommentShortResponse> PreviewChildComments { get; set; }

    [JsonPropertyName("other_preview_users")]
    public List<InstaUserShortResponse> OtherPreviewUsers { get; set; }
}