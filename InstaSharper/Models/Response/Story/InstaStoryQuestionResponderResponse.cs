using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryQuestionResponderResponse
{
    [JsonPropertyName("response")]
    public string Response { get; set; }

    [JsonPropertyName("has_shared_response")]
    public bool? HasSharedResponse { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortResponse User { get; set; }

    [JsonPropertyName("ts")]
    public long? Ts { get; set; }
}