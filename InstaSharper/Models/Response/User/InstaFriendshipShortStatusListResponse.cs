using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaFriendshipShortStatusListResponse : List<InstaFriendshipShortStatusResponse>
{
    [JsonPropertyName("status")]
    public string Status { get; set; }
}