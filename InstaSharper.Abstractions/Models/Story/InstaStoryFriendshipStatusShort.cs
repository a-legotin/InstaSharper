namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryFriendshipStatusShort
{
    public bool Following { get; set; }

    public bool Muting { get; set; }

    public bool OutgoingRequest { get; set; }

    public bool IsMutingReel { get; set; }
}