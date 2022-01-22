using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Location;

public class InstaLocationShortResponse
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("external_id")]
    public string ExternalId { get; set; }

    [JsonPropertyName("external_id_source")]
    public string ExternalIdSource { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}