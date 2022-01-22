using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Media;

namespace InstaSharper.Models.Response.Product;

internal class InstaProductImageResponse
{
    [JsonPropertyName("image_versions2")]
    public InstaImageCandidatesResponse Images { get; set; }
}