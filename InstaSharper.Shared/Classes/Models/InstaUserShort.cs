namespace InstaSharper.Classes.Models
{
    public class InstaUserShort
    {
        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }
        public string Pk { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfilePictureId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public static InstaUserShort Empty => new InstaUserShort {FullName = string.Empty, UserName = string.Empty};
    }
}