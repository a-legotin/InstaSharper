using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Product;

internal class InstaProductContainerResponse
{
    [JsonPropertyName("product")]
    public InstaProductResponse Product { get; set; }

    [JsonPropertyName("position")]
    public double[] Position { get; set; }
}