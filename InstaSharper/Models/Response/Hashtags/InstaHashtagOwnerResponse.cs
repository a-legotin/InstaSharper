using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Hashtags;

internal class InstaHashtagOwnerResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("pk")]
    public string Pk { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("profile_pic_url")]
    public string ProfilePicUrl { get; set; }

    [JsonPropertyName("profile_pic_username")]
    public string ProfilePicUsername { get; set; }
}