using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryFriendshipStatusContainerResponse
{
    [JsonPropertyName("friendship_status")]
    public InstaStoryFriendshipStatusResponse FriendshipStatus { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}