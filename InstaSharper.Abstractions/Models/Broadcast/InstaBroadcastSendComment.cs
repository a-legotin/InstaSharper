using System;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcastSendComment
{
    public string ContentType { get; set; }

    public InstaUserShortFriendshipFull User { get; set; }

    public long Pk { get; set; }

    public string Text { get; set; }

    public int Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public long MediaId { get; set; }
}