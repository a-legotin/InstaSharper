using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Comment;

internal class InstaInlineFollowResponse
{
    [JsonPropertyName("outgoing_request")]
    public bool IsOutgoingRequest { get; set; }

    [JsonPropertyName("following")]
    public bool IsFollowing { get; set; }

    [JsonPropertyName("user_info")]
    public InstaUserShortResponse UserInfo { get; set; }
}