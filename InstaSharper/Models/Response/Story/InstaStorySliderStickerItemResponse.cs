using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStorySliderStickerItemResponse
{
    [JsonPropertyName("text_color")]
    public string TextColor { get; set; }

    [JsonPropertyName("slider_id")]
    public long SliderId { get; set; }

    [JsonPropertyName("question")]
    public string Question { get; set; }

    [JsonPropertyName("emoji")]
    public string Emoji { get; set; }

    [JsonPropertyName("viewer_can_vote")]
    public bool ViewerCanVote { get; set; }

    [JsonPropertyName("slider_vote_count")]
    public long? SliderVoteCount { get; set; }

    [JsonPropertyName("slider_vote_average")]
    public double? SliderVoteAverage { get; set; }
}