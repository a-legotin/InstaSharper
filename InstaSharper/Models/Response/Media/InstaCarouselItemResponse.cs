using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Media;

internal class InstaCarouselItemResponse
{
    [JsonPropertyName("id")]
    public string InstaIdentifier { get; set; }

    [JsonPropertyName("media_type")]
    public int MediaType { get; set; }

    [JsonPropertyName("image_versions2")]
    public InstaImageCandidatesResponse Images { get; set; }

    [JsonPropertyName("video_versions")]
    public List<InstaVideoResponse> Videos { get; set; }

    [JsonPropertyName("original_width")]
    public int Width { get; set; }

    [JsonPropertyName("original_height")]
    public int Height { get; set; }

    [JsonPropertyName("pk")]
    public long Pk { get; set; }

    [JsonPropertyName("carousel_parent_id")]
    public string CarouselParentId { get; set; }

    [JsonPropertyName("usertags")]
    public InstaUserTagListResponse UserTagList { get; set; }
}