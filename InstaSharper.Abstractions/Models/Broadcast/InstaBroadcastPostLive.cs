using System.Collections.Generic;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcastPostLive
{
    public string Pk { get; set; }

    public InstaUserShortFriendshipFull User { get; set; }

    public List<InstaBroadcastInfo> Broadcasts { get; set; } = new();

    public int PeakViewerCount { get; set; }
}