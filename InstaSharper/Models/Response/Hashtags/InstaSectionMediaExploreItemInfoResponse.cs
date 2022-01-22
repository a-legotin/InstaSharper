using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaSectionMediaExploreItemInfoResponse
{
    [JsonPropertyName("num_columns")]
    public int NumBolumns { get; set; }

    [JsonPropertyName("total_num_columns")]
    public int TotalNumBolumns { get; set; }

    [JsonPropertyName("aspect_ratio")]
    public float AspectRatio { get; set; }

    [JsonPropertyName("autoplay")]
    public bool Autoplay { get; set; }
}