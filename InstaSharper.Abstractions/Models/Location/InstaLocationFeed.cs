using InstaSharper.Abstractions.Models.Feed;
using InstaSharper.Abstractions.Models.Media;
using InstaSharper.Abstractions.Models.Story;

namespace InstaSharper.Abstractions.Models.Location;

public class InstaLocationFeed : InstaBaseFeed
{
    public InstaMediaList RankedMedias { get; set; } = new();
    public InstaStory Story { get; set; }
    public InstaLocation Location { get; set; }

    public long MediaCount { get; set; }
}