using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Product;

internal class InstaProductResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; }

    [JsonPropertyName("current_price")]
    public string CurrentPrice { get; set; }

    [JsonPropertyName("full_price")]
    public string FullPrice { get; set; }

    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [JsonPropertyName("has_viewer_saved")]
    public bool HasViewerSaved { get; set; }

    [JsonPropertyName("main_image")]
    public InstaProductImageResponse MainImage { get; set; }

    [JsonPropertyName("thumbnail_image")]
    public InstaProductImageResponse ThumbnailImage { get; set; }

    [JsonPropertyName("review_status")]
    public string ReviewStatus { get; set; }

    [JsonPropertyName("external_url")]
    public string ExternalUrl { get; set; }

    [JsonPropertyName("checkout_style")]
    public string CheckoutStyle { get; set; }

    [JsonPropertyName("merchant")]
    public InstaMerchantResponse Merchant { get; set; }

    [JsonPropertyName("product_images")]
    public List<InstaProductImageResponse> ProductImages { get; set; }

    [JsonPropertyName("product_appeal_review_status")]
    public string ProductAppealReviewStatus { get; set; }

    [JsonPropertyName("full_price_stripped")]
    public string FullPriceStripped { get; set; }

    [JsonPropertyName("current_price_stripped")]
    public string CurrentPriceStripped { get; set; }
}