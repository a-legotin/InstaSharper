using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;

namespace InstaSharper.Models.Response.Comment;

internal class InstaInlineCommentListResponse : BaseStatusResponse
{
    [JsonPropertyName("child_comment_count")]
    public int ChildCommentCount { get; set; }

    [JsonPropertyName("has_more_tail_child_comments")]
    public bool HasMoreTailChildComments { get; set; }

    [JsonPropertyName("has_more_head_child_comments")]
    public bool HasMoreHeadChildComments { get; set; }

    [JsonPropertyName("next_max_child_cursor")]
    public string NextMaxId { get; set; }

    [JsonPropertyName("next_in_child_cursor")]
    public string NextMinId { get; set; }

    [JsonPropertyName("num_tail_child_comments")]
    public int NumTailChildComments { get; set; }

    [JsonPropertyName("parent_comment")]
    public InstaCommentResponse ParentComment { get; set; }

    [JsonPropertyName("child_comments")]
    public List<InstaCommentResponse> ChildComments { get; set; }
}