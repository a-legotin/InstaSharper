using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryPollStickerItem
{
    public string Id { get; set; }

    public long PollId { get; set; }

    public string Question { get; set; }

    public List<InstaStoryTalliesItem> Tallies { get; set; } = new();

    public bool ViewerCanVote { get; set; }

    public bool IsSharedResult { get; set; }

    public bool Finished { get; set; }

    public long ViewerVote { get; set; } = 0;
}