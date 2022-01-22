namespace InstaSharper.Abstractions.Models.User;

public class InstaFriendshipShortStatus
{
    public long Pk { get; set; }

    public bool Following { get; set; }

    public bool IsPrivate { get; set; }

    public bool IncomingRequest { get; set; }

    public bool OutgoingRequest { get; set; }

    public bool IsBestie { get; set; }
}