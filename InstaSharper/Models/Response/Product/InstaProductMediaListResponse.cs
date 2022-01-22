using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Models.Response.Product;

internal class InstaProductMediaListResponse : BaseLoadableResponse
{
    [JsonPropertyName("items")]
    public List<InstaMediaItemResponse> Medias { get; set; } = new();
}