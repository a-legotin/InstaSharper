using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

public class InstaStoryPollStickerItemResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("poll_id")]
    public long PollId { get; set; }

    [JsonPropertyName("question")]
    public string Question { get; set; }

    [JsonPropertyName("tallies")]
    public List<InstaStoryTalliesItemResponse> Tallies { get; set; }

    [JsonPropertyName("viewer_can_vote")]
    public bool ViewerCanVote { get; set; }

    [JsonPropertyName("is_shared_result")]
    public bool IsSharedResult { get; set; }

    [JsonPropertyName("finished")]
    public bool Finished { get; set; }

    [JsonPropertyName("viewer_vote")]
    public long? ViewerVote { get; set; }
}