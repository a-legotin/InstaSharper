using InstaSharper.Abstractions.Models.Media;

namespace InstaSharper.Abstractions.Models.Hashtags;

public class InstaDirectHashtag
{
    public string Name { get; set; }

    public long MediaCount { get; set; }

    public InstaMedia Media { get; set; }
}