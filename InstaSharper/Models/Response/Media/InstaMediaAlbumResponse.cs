using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Media;

internal class InstaMediaAlbumResponse
{
    [JsonPropertyName("media")]
    public InstaMediaItemResponse Media { get; set; }

    [JsonPropertyName("client_sidecar_id")]
    public string ClientSidecarId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}