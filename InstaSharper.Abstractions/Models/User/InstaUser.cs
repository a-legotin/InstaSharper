using System;
using System.Collections;
using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.User
{
    [Serializable]
    public class InstaUser : InstaUserShort
    {
        public InstaUser(InstaUserShort instaUserShort)
        {
            Pk = instaUserShort.Pk;
            UserName = instaUserShort.UserName;
            FullName = instaUserShort.FullName;
            IsPrivate = instaUserShort.IsPrivate;
            ProfilePicture = instaUserShort.ProfilePicture;
            ProfilePicUrl = instaUserShort.ProfilePicUrl;
            ProfilePictureId = instaUserShort.ProfilePictureId;
            IsVerified = instaUserShort.IsVerified;
            HasAnonymousProfilePicture = instaUserShort.HasAnonymousProfilePicture;
        }

        public int FollowersCount { get; set; }

        public string FollowersCountByLine { get; set; }

        public string SocialContext { get; set; }

        public string SearchSocialContext { get; set; }

        public int MutualFollowers { get; set; }

        public int UnseenCount { get; set; }

        public InstaFriendshipShortStatus FriendshipStatus { get; set; }

        public long LatestReelMedia { get; set; }
    }
}