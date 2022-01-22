using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Product;

internal class InstaProductTagsContainerResponse
{
    [JsonPropertyName("in")]
    public List<InstaProductContainerResponse> In { get; set; }
}