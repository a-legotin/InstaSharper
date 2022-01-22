using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaUserShortFriendshipFullResponse : InstaUserShortResponse
{
    [JsonPropertyName("friendship_status")]
    public InstaFriendshipFullStatusResponse FriendshipStatus { get; set; }
}