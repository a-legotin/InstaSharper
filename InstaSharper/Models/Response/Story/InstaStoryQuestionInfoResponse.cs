using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryQuestionInfoResponse
{
    [JsonPropertyName("question_id")]
    public long QuestionId { get; set; }

    [JsonPropertyName("question")]
    public string Question { get; set; }

    [JsonPropertyName("question_type")]
    public string QuestionType { get; set; }

    [JsonPropertyName("background_color")]
    public string BackgroundColor { get; set; }

    [JsonPropertyName("text_color")]
    public string TextColor { get; set; }

    [JsonPropertyName("responders")]
    public List<InstaStoryQuestionResponderResponse> Responders { get; set; }

    [JsonPropertyName("max_id")]
    public string MaxId { get; set; }

    [JsonPropertyName("more_available")]
    public bool? MoreAvailable { get; set; }

    [JsonPropertyName("question_response_count")]
    public int? QuestionResponseCount { get; set; }

    [JsonPropertyName("latest_question_response_time")]
    public long? LatestQuestionResponseTime { get; set; }
}