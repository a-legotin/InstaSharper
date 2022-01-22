using System.Collections.Generic;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaTopLive
{
    public int RankedPosition { get; set; }

    public List<InstaUserShortFriendshipFull> BroadcastOwners { get; set; } = new();
}