using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryVoterItemResponse
{
    [JsonPropertyName("user")]
    public InstaUserShortFriendshipResponse User { get; set; }

    [JsonPropertyName("vote")]
    public double? Vote { get; set; }

    [JsonPropertyName("ts")]
    public long Ts { get; set; }
}