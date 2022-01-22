using System.Collections.Generic;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcastNotifyFriends
{
    public string Text { get; set; }

    public List<InstaUserShortFriendshipFull> Friends { get; set; } = new();

    public int OnlineFriendsCount { get; set; }
}