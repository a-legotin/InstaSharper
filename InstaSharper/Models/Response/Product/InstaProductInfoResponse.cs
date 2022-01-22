using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Models.Response.Product;

internal class InstaProductInfoResponse : BaseStatusResponse
{
    [JsonPropertyName("product_item")]
    public InstaProductResponse Product { get; set; }

    [JsonPropertyName("other_product_items")]
    public List<InstaProductResponse> OtherProductItems { get; set; }

    [JsonPropertyName("user")]
    public InstaUserShortResponse User { get; set; }

    [JsonPropertyName("more_from_business")]
    public InstaProductMediaListResponse MoreFromBusiness { get; set; }
}