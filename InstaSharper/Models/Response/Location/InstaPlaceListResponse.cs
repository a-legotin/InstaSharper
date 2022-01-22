using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Location;

internal class InstaPlaceListResponse
{
    [JsonPropertyName("items")]
    public List<InstaPlaceResponse> Items { get; set; }

    [JsonPropertyName("has_more")]
    public bool? HasMore { get; set; }

    [JsonPropertyName("rank_token")]
    public string RankToken { get; set; }

    [JsonPropertyName("status")]
    internal string Status { get; set; }

    [JsonPropertyName("message")]
    internal string Message { get; set; }
}