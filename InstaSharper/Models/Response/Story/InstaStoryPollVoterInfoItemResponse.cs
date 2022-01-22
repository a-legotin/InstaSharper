using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryPollVoterInfoItemResponse
{
    [JsonPropertyName("poll_id")]
    public long PollId { get; set; }

    [JsonPropertyName("voters")]
    public List<InstaStoryVoterItemResponse> Voters { get; set; }

    [JsonPropertyName("max_id")]
    public string MaxId { get; set; }

    [JsonPropertyName("more_available")]
    public bool MoreAvailable { get; set; }

    [JsonPropertyName("latest_poll_vote_time")]
    public long? LatestPollVoteTime { get; set; }
}