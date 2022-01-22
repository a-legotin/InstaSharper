using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStorySliderVoterInfoItemResponse
{
    [JsonPropertyName("slider_id")]
    public long SliderId { get; set; }

    [JsonPropertyName("voters")]
    public List<InstaStoryVoterItemResponse> Voters { get; set; }

    [JsonPropertyName("max_id")]
    public string MaxId { get; set; }

    [JsonPropertyName("more_available")]
    public bool MoreAvailable { get; set; }

    [JsonPropertyName("latest_slider_vote_time")]
    public long? LatestSliderVoteTime { get; set; }
}