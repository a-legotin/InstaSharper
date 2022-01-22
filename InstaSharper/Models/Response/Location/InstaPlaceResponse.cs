using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Location;

internal class InstaPlaceResponse
{
    [JsonPropertyName("location")]
    public InstaPlaceShortResponse Location { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("subtitle")]
    public string Subtitle { get; set; }
}