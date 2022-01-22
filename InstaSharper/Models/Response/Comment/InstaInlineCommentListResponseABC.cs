using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Models.Response.Comment;

internal class InstaInlineCommentListResponseABC
{
    [JsonPropertyName("comment_likes_enabled")]
    public bool CommentLikesEnabled { get; set; }

    [JsonPropertyName("comments")]
    public List<InstaCommentResponse> Comments { get; set; }

    [JsonPropertyName("comment_count")]
    public int CommentCount { get; set; }

    [JsonPropertyName("caption")]
    public InstaCaptionResponse Caption { get; set; }

    [JsonPropertyName("caption_is_edited")]
    public bool CaptionIsEdited { get; set; }

    [JsonPropertyName("has_more_comments")]
    public bool HasMoreComments { get; set; }

    [JsonPropertyName("has_more_headload_comments")]
    public bool HasMoreHeadloadComments { get; set; }

    [JsonPropertyName("threading_enabled")]
    public bool ThreadingEnabled { get; set; }

    [JsonPropertyName("media_header_display")]
    public string MediaHeaderDisplay { get; set; }

    [JsonPropertyName("initiate_at_top")]
    public bool InitiateAtTop { get; set; }

    [JsonPropertyName("insert_new_comment_to_top")]
    public bool InsertNewCommentToTop { get; set; }

    [JsonPropertyName("preview_comments")]
    public List<InstaCommentResponse> PreviewComments { get; set; }

    [JsonPropertyName("can_view_more_preview_comments")]
    public bool CanViewMorePreviewComments { get; set; }

    [JsonPropertyName("next_min_id")]
    public string NextMinId { get; set; }

    [JsonPropertyName("status")]
    internal string Status { get; set; }

    [JsonPropertyName("next_max_id")]
    public string NextMaxId { get; set; }
}