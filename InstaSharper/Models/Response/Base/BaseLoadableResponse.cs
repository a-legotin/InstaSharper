using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Base;

internal class BaseLoadableResponse : BaseStatusResponse
{
    [JsonPropertyName("more_available")]
    public bool MoreAvailable { get; set; }

    [JsonPropertyName("num_results")]
    public int ResultsCount { get; set; }

    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }

    [JsonPropertyName("auto_load_more_enabled")]
    public bool AutoLoadMoreEnabled { get; set; }

    [JsonPropertyName("next_max_id")]
    public string NextMaxId { get; set; }

    [JsonPropertyName("rank_token")]
    public string RankToken { get; set; } = "unknown";
}