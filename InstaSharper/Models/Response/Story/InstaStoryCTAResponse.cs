using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryCTAResponse
{
    [JsonPropertyName("linkType")]
    public int LinkType { get; set; }

    [JsonPropertyName("webUri")]
    public string WebUri { get; set; }

    [JsonPropertyName("androidClass")]
    public string AndroidClass { get; set; }

    [JsonPropertyName("package")]
    public string Package { get; set; }

    [JsonPropertyName("deeplinkUri")]
    public string DeeplinkUri { get; set; }

    [JsonPropertyName("callToActionTitle")]
    public string CallToActionTitle { get; set; }

    [JsonPropertyName("redirectUri")]
    public object RedirectUri { get; set; }

    [JsonPropertyName("leadGenFormId")]
    public string LeadGenFormId { get; set; }

    [JsonPropertyName("igUserId")]
    public string IgUserId { get; set; }
}