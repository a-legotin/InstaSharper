using System;
using System.Collections.Generic;
using InstaSharper.Abstractions.Models.Story;

namespace InstaSharper.Abstractions.Models.Hashtags;

public class InstaHashtagStory
{
    public string Id { get; set; }

    public int LatestReelMedia { get; set; }

    public DateTime ExpiringAt { get; set; }

    public bool CanReply { get; set; }

    public bool CanReshare { get; set; }

    public string ReelType { get; set; }

    public InstaHashtagOwner Owner { get; set; }

    public List<InstaStoryItem> Items { get; set; } = new();

    public int PrefetchCount { get; set; }

    public long UniqueIntegerReelId { get; set; }

    public bool Muted { get; set; }
}