using System;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcast
{
    public string Id { get; set; }

    public string RtmpPlaybackUrl { get; set; }

    public string DashPlaybackUrl { get; set; }

    public string DashAbrPlaybackUrl { get; set; }

    public string BroadcastStatus { get; set; }

    public long ViewerCount { get; set; }

    public bool InternalOnly { get; set; }

    public string CoverFrameUrl { get; set; }

    public InstaUserShortFriendshipFull BroadcastOwner { get; set; }

    public DateTime PublishedTime { get; set; }

    public string MediaId { get; set; }

    public string BroadcastMessage { get; set; }

    public string OrganicTrackingToken { get; set; }

    public string DashManifest { get; set; }
}