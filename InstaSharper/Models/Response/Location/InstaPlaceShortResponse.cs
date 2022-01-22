using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Location;

public class InstaPlaceShortResponse
{
    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }

    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("external_source")]
    public string ExternalSource { get; set; }

    [JsonPropertyName("facebook_places_id")]
    public long FacebookPlacesId { get; set; }
}