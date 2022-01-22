using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaUserShortFriendshipResponse : InstaUserShortResponse
{
    [JsonPropertyName("friendship_status")]
    public InstaFriendshipShortStatusResponse FriendshipStatus { get; set; }
}