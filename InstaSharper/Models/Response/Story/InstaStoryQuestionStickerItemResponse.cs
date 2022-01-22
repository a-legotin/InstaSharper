using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryQuestionStickerItemResponse
{
    [JsonPropertyName("background_color")]
    public string BackgroundColor { get; set; }

    [JsonPropertyName("profile_pic_url")]
    public string ProfilePicUrl { get; set; }

    [JsonPropertyName("question_id")]
    public long QuestionId { get; set; }

    [JsonPropertyName("question")]
    public string Question { get; set; }

    [JsonPropertyName("question_type")]
    public string QuestionType { get; set; }

    [JsonPropertyName("text_color")]
    public string TextColor { get; set; }

    [JsonPropertyName("viewer_can_interact")]
    public bool ViewerCanInteract { get; set; }
}