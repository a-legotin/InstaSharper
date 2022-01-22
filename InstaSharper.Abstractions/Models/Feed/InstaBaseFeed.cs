using InstaSharper.Abstractions.Models.Media;

namespace InstaSharper.Abstractions.Models.Feed;

public class InstaBaseFeed
{
    public InstaMediaList Medias { get; set; } = new();
    public string NextMaxId { get; set; }
}