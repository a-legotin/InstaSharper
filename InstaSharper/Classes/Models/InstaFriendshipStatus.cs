namespace InstaSharper.Classes.Models
{
    public class InstaFriendshipStatus
    {
        public bool Foolowing { get; set; }
        public bool IsPrivate { get; set; }

        public bool IncomingRequest { get; set; }

        public bool OutgoingRequest { get; set; }
    }
}