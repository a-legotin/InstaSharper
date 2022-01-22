namespace InstaSharper.Abstractions.Models.Story;

public class InstaReelShare
{
    public string Text { get; set; }

    public string Type { get; set; }

    public long ReelOwnerId { get; set; }

    public bool IsReelPersisted { get; set; }

    public string ReelType { get; set; }

    public InstaStoryItem Media { get; set; }
}