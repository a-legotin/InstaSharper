using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Product;

internal class InstaMerchantResponse
{
    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("profile_pic_url")]
    public string ProfilePicture { get; set; }
}