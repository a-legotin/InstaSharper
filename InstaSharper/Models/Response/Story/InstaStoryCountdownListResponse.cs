using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryCountdownListResponse
{
    [JsonPropertyName("countdowns")]
    public List<InstaStoryCountdownStickerItemResponse> Items { get; set; }

    [JsonPropertyName("more_available")]
    public bool? MoreAvailable { get; set; }

    [JsonPropertyName("max_id")]
    public string MaxId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}