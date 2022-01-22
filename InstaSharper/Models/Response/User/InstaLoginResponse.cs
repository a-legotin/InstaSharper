using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;

namespace InstaSharper.Models.Response.User;

internal class InstaLoginResponse : BaseStatusResponse
{
    [JsonPropertyName("logged_in_user")]
    public InstaUserShortResponse User { get; set; }
}