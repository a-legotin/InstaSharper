using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User;

internal class InstaUserTagResponse
{
    [JsonPropertyName("position")]
    public double[] Position { get; set; }

    [JsonPropertyName("time_in_video")]
    public string TimeInVideo { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortResponse User { get; set; }
}