using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Media;

public class InstaProduct
{
    public string Name { get; set; }

    public string Price { get; set; }

    public string CurrentPrice { get; set; }

    public string FullPrice { get; set; }

    public long ProductId { get; set; }

    public bool HasViewerSaved { get; set; }

    public List<InstaImage> MainImage { get; set; } = new();

    public List<InstaImage> ThumbnailImage { get; set; } = new();

    public List<InstaImage> ProductImages { get; set; } = new();

    public string ReviewStatus { get; set; }

    public string ExternalUrl { get; set; }

    public string CheckoutStyle { get; set; }

    public InstaMerchant Merchant { get; set; }

    public string ProductAppealReviewStatus { get; set; }

    public string FullPriceStripped { get; set; }

    public string CurrentPriceStripped { get; set; }
}