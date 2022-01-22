using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

public class InstaHashtagResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("media_count")]
    public long MediaCount { get; set; }

    [JsonPropertyName("profile_pic_url")]
    public string ProfilePicUrl { get; set; }
}