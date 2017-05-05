using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaUsersConverter : IObjectConverter<InstaUser, InstaUserResponse>
    {
        public InstaUserResponse SourceObject { get; set; }

        public InstaUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var user = new InstaUser
            {
                InstaIdentifier = SourceObject.Id
            };
            if (!string.IsNullOrEmpty(SourceObject.FullName)) user.FullName = SourceObject.FullName;
            if (!string.IsNullOrEmpty(SourceObject.ProfilePicture)) user.ProfilePicture = SourceObject.ProfilePicture;
            if (!string.IsNullOrEmpty(SourceObject.UserName)) user.UserName = SourceObject.UserName;
            if (!string.IsNullOrEmpty(SourceObject.Pk)) user.Pk = SourceObject.Pk;
            if (SourceObject.Friendship != null)
                user.FriendshipStatus = ConvertersFabric.GetFriendShipStatusConverter(SourceObject.Friendship)
                    .Convert();
            user.HasAnonymousProfilePicture = SourceObject.HasAnonymousProfilePicture;
            user.ProfilePictureId = SourceObject.ProfilePictureId;
            user.IsVerified = SourceObject.IsVerified;
            user.IsPrivate = SourceObject.IsPrivate;
            user.UnseenCount = SourceObject.UnseenCount;
            user.MutualFollowersCount = SourceObject.MutualFollowersCount;
            return user;
        }
    }
}