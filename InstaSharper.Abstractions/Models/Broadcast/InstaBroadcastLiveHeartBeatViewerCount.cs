namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcastLiveHeartBeatViewerCount
{
    public float ViewerCount { get; set; }

    public string BroadcastStatus { get; set; }

    public object[] CobroadcasterIds { get; set; }

    public int OffsetToVideoStart { get; set; }

    public int TotalUniqueViewerCount { get; set; }

    public int IsTopLiveEligible { get; set; }
}