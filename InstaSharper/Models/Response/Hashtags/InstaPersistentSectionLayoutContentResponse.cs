using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaPersistentSectionLayoutContentResponse
{
    [JsonPropertyName("related_style")]
    public string RelatedTtyle { get; set; }

    [JsonPropertyName("related")]
    public List<InstaRelatedHashtagResponse> Related { get; set; }
}