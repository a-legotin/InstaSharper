using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class BadStatusResponse : BaseStatusResponse
    {
        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("error_type")] public string ErrorType { get; set; } = "unknown";

        [JsonProperty("checkpoint_url")] public string CheckPointUrl { get; set; }
        
        [JsonProperty("spam")] public bool Spam { get; set; }
        
        [JsonProperty("feedback_title")] public string FeedbackTitle { get; set; }
        
        [JsonProperty("feedback_message")] public string FeedbackMessage { get; set; }
        
        [JsonProperty("feedback_appeal_label")] public string FeedbackAppealLabel { get; set; }
        
        [JsonProperty("feedback_action")] public string FeedbackAction { get; set; }
        
        [JsonProperty("feedback_ignore_label")] public string FeedbackIgnoreLabel { get; set; }
    }
}