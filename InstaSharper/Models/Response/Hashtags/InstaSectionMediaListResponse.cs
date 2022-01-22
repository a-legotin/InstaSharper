using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaSectionMediaListResponse
{
    [JsonPropertyName("sections")]
    public List<InstaSectionMediaResponse> Sections { get; set; } = new();

    [JsonPropertyName("persistent_sections")]
    public List<InstaPersistentSectionResponse> PersistentSections { get; set; } = new();

    [JsonPropertyName("more_available")]
    public bool MoreAvailable { get; set; }

    [JsonPropertyName("next_max_id")]
    public string NextMaxId { get; set; }

    [JsonPropertyName("next_page")]
    public int? NextPage { get; set; }

    [JsonPropertyName("next_media_ids")]
    public List<long> NextMediaIds { get; set; }

    [JsonPropertyName("auto_load_more_enabled")]
    public bool? AutoLoadMoreEnabled { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}