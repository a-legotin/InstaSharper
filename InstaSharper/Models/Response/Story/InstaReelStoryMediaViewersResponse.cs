using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Story;

internal class InstaReelStoryMediaViewersResponse
{
    [JsonPropertyName("next_max_id")]
    public string NextMaxId { get; set; }

    [JsonPropertyName("screenshotter_user_ids")]
    public object[] ScreenshotterUserIds { get; set; }

    [JsonPropertyName("total_screenshot_count")]
    public double? TotalScreenshotCount { get; set; }

    [JsonPropertyName("total_viewer_count")]
    public double? TotalViewerCount { get; set; }

    [JsonPropertyName("updated_media")]
    public InstaStoryItemResponse UpdatedMedia { get; set; }

    [JsonPropertyName("user_count")]
    public double? UserCount { get; set; }

    [JsonPropertyName("users")]
    public List<InstaUserShortResponse> Users { get; set; }
}