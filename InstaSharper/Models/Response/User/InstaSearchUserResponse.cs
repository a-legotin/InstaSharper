using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaSearchUserResponse
{
    [JsonPropertyName("has_more")]
    public bool MoreAvailable { get; set; }

    [JsonPropertyName("num_results")]
    public bool ResultCount { get; set; }

    [JsonPropertyName("users")]
    public List<InstaUserResponse> Users { get; set; }
}