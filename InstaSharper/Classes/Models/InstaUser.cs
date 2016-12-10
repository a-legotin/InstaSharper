namespace InstaSharper.Classes.Models
{
    public class InstaUser
    {
        public string UserName { get; set; }
        public bool HasAnonymousProfilePicture { get; set; }

        public InstaFriendshipStatus FriendshipStatus { get; set; }

        public int UnseenCount { get; set; }
        public string ProfilePicture { get; set; }

        public string ProfilePictureId { get; set; }

        public string FullName { get; set; }

        public long InstaIdentifier { get; set; }

        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }

        public int FollowedByCount { get; set; }
        public int FollowerCount { get; set; }

        public string Pk { get; set; }

        public string MutualFollowersCount { get; set; }
        public static InstaUser Empty => new InstaUser {FullName = string.Empty, UserName = string.Empty};
    }
}