using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaFriendshipFullStatusContainerResponse
{
    [JsonPropertyName("friendship_status")]
    public InstaFriendshipFullStatusResponse FriendshipStatus { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}