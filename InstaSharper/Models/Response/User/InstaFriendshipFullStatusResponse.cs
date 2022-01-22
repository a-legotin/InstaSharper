using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaFriendshipFullStatusResponse
{
    [JsonPropertyName("following")]
    public bool? Following { get; set; }

    [JsonPropertyName("followed_by")]
    public bool? FollowedBy { get; set; }

    [JsonPropertyName("blocking")]
    public bool? Blocking { get; set; }

    [JsonPropertyName("muting")]
    public bool? Muting { get; set; }

    [JsonPropertyName("is_private")]
    public bool? IsPrivate { get; set; }

    [JsonPropertyName("incoming_request")]
    public bool? IncomingRequest { get; set; }

    [JsonPropertyName("outgoing_request")]
    public bool? OutgoingRequest { get; set; }

    [JsonPropertyName("is_bestie")]
    public bool? IsBestie { get; set; }
}