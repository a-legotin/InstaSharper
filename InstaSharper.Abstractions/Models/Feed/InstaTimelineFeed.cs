namespace InstaSharper.Abstractions.Models.Feed;

public class InstaTimelineFeed : InstaBaseFeed
{
    public int MediaItemsCount => Medias.Count;
    public bool MoreAvailable { get; set; }
}