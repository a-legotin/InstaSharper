using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryFriendshipStatusShortResponse
{
    [JsonPropertyName("following")]
    public bool Following { get; set; }

    [JsonPropertyName("muting")]
    public bool? Muting { get; set; }

    [JsonPropertyName("outgoing_request")]
    public bool? OutgoingRequest { get; set; }

    [JsonPropertyName("is_muting_reel")]
    public bool? IsMutingReel { get; set; }
}