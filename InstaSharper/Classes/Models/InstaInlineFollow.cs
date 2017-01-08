namespace InstaSharper.Classes.Models
{
    public class InstaInlineFollow
    {
        public bool IsOutgoingRequest { get; set; }
        public bool IsFollowing { get; set; }
        public InstaUser User { get; set; }
    }
}