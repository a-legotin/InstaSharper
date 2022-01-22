namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryFriendshipStatus
{
    public bool Following { get; set; }

    public bool FollowedBy { get; set; }

    public bool Blocking { get; set; }

    public bool Muting { get; set; }

    public bool IsPrivate { get; set; }

    public bool IncomingRequest { get; set; }

    public bool OutgoingRequest { get; set; }

    public bool IsBlockingReel { get; set; }

    public bool IsMutingReel { get; set; }

    public bool IsBestie { get; set; }
}