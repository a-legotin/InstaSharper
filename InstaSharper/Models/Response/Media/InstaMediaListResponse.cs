using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.Story;

namespace InstaSharper.Models.Response.Media;

internal class InstaMediaListResponse : BaseLoadableResponse
{
    [JsonPropertyName("items")]
    public List<InstaMediaItemResponse> Medias { get; set; } = new();

    public List<InstaStoryResponse> Stories { get; set; } = new();
}