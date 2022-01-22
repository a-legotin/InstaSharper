using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Location;

internal class InstaLocationSearchResponse
{
    [JsonPropertyName("venues")]
    public List<InstaLocationShortResponse> Locations { get; set; }

    [JsonPropertyName("request_id")]
    public string RequestId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}