using System;
using System.Collections.Generic;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaReelFeed
{
    public long HasBestiesMedia { get; set; }

    public long PrefetchCount { get; set; }

    public bool? CanReshare { get; set; }

    public bool CanReply { get; set; }

    public DateTime ExpiringAt { get; set; }

    public List<InstaStoryItem> Items { get; set; } = new();

    public string Id { get; set; }

    public long LatestReelMedia { get; set; }

    public long Seen { get; set; }

    public InstaUserShortFriendshipFull User { get; set; }
}