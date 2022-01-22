using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaHashtagSearchResponse : BaseStatusResponse
{
    [JsonPropertyName("has_more")]
    public bool? MoreAvailable { get; set; }

    [JsonPropertyName("rank_token")]
    public string RankToken { get; set; }
}