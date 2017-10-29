using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaUserConverter : IObjectConverter<InstaUser, InstaUserResponse>
    {
        public InstaUserResponse SourceObject { get; set; }

        public InstaUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var shortConverter = ConvertersFabric.GetUserShortConverter(SourceObject);
            var user = new InstaUser(shortConverter.Convert())
            {
                HasAnonymousProfilePicture = SourceObject.HasAnonymousProfilePicture,
                FollowersCount = SourceObject.FollowersCount,
                FollowersCountByLine = SourceObject.FollowersCountByLine,
                SearchSocialContext = SourceObject.SearchSocialContext,
                SocialContext = SourceObject.SocialContext,
                MutualFollowers = (int) System.Convert.ToDouble(SourceObject.MulualFollowersCount)
            };
            if (SourceObject.FriendshipStatus != null)
            {
                var freindShipStatusConverter =
                    ConvertersFabric.GetFriendShipStatusConverter(SourceObject.FriendshipStatus);
                user.FriendshipStatus = freindShipStatusConverter.Convert();
            }
            return user;
        }
    }
}