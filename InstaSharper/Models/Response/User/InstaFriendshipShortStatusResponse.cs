using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaFriendshipShortStatusResponse
{
    [JsonPropertyName("following")]
    public bool Following { get; set; }

    [JsonPropertyName("is_private")]
    public bool IsPrivate { get; set; }

    [JsonPropertyName("incoming_request")]
    public bool IncomingRequest { get; set; }

    [JsonPropertyName("outgoing_request")]
    public bool OutgoingRequest { get; set; }

    [JsonPropertyName("is_bestie")]
    public bool IsBestie { get; set; }
}