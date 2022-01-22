namespace InstaSharper.Abstractions.Models.Feed;

public class InstaFeed : InstaBaseFeed
{
    public int MediaItemsCount => Medias.Count;
    public bool MoreAvailable { get; set; }
}