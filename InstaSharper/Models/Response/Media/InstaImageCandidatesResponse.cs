using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Media;

internal class InstaImageCandidatesResponse
{
    [JsonPropertyName("candidates")]
    public List<ImageResponse> Candidates { get; set; }
}