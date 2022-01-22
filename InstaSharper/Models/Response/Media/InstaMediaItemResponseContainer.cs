using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Media;

internal class InstaMediaItemResponseContainer
{
    [JsonPropertyName("media_or_ad")]
    public InstaMediaItemResponse MediaOrAd { get; set; }
}