using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Location;

internal class InstaLocationResponse : InstaLocationShortResponse
{
    [JsonPropertyName("x")]
    public float X { get; set; }

    [JsonPropertyName("y")]
    public float Y { get; set; }

    [JsonPropertyName("z")]
    public int Z { get; set; }

    [JsonPropertyName("width")]
    public float Width { get; set; }

    [JsonPropertyName("height")]
    public float Height { get; set; }

    [JsonPropertyName("rotation")]
    public float Rotation { get; set; }

    [JsonPropertyName("facebook_places_id")]
    public long FacebookPlacesId { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; }
}