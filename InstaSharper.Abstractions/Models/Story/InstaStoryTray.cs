using System.Collections.Generic;
using InstaSharper.Abstractions.Models.Broadcast;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryTray
{
    public long Id { get; set; }

    public InstaTopLive TopLive { get; set; } = new();

    public bool IsPortrait { get; set; }

    public List<InstaStory> Tray { get; set; } = new();
}