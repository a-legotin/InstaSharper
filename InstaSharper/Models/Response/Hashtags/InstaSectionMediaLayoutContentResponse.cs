using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaSectionMediaLayoutContentResponse
{
    [JsonPropertyName("medias")]
    public List<InstaMediaAlbumResponse> Medias { get; set; }
}