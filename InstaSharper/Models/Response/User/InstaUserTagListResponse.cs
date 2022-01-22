using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaUserTagListResponse
{
    [JsonPropertyName("in")]
    public List<InstaUserTagResponse> In { get; set; } = new();
}