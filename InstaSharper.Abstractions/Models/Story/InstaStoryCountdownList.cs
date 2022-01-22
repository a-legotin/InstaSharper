using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryCountdownList
{
    public List<InstaStoryCountdownStickerItem> Items { get; set; } = new();

    public bool MoreAvailable { get; set; }

    public string MaxId { get; set; }
}