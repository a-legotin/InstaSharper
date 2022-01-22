using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Comment;

internal class InstaBlockedCommentersResponse : BaseStatusResponse
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("blocked_commenters")]
    public List<InstaUserShortResponse> BlockedCommenters { get; set; }
}