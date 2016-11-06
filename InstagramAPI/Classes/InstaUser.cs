namespace InstagramApi.Classes
{
    public class InstaUser
    {
        public string UserName { get; set; }

        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public long InstaIdentifier { get; set; }

        public string ExternalUrl { get; set; }
        public string IsVerified { get; set; }

        public int FollowedByCount { get; set; }

        public static InstaUser Empty => new InstaUser {FullName = string.Empty, UserName = string.Empty};
    }
}