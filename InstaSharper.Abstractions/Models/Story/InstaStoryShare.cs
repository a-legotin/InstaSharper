using InstaSharper.Abstractions.Models.Media;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryShare
{
    public InstaMedia Media { get; set; }
    public string ReelType { get; set; }
    public bool IsReelPersisted { get; set; }
    public string Text { get; set; }
    public bool IsLinked { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
}