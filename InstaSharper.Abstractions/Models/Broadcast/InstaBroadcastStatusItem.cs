namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcastStatusItem
{
    public long Id { get; set; }

    public string BroadcastStatus { get; set; }

    public float ViewerCount { get; set; }

    public bool HasReducedVisibility { get; set; }

    public string CoverFrameUrl { get; set; }
}