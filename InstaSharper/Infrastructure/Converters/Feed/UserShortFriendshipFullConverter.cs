using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Infrastructure.Converters.Feed;

internal class UserShortFriendshipFullConverter : IObjectConverter<InstaUserShortFriendshipFull,
    InstaUserShortFriendshipFullResponse>
{
    public InstaUserShortFriendshipFull Convert(InstaUserShortFriendshipFullResponse source)
    {
        var userFriendship = new InstaUserShortFriendshipFull();
        userFriendship.FriendshipStatus = new InstaFriendshipFullStatus
        {
            Blocking = source.FriendshipStatus.Blocking,
            Following = source.FriendshipStatus.Following,
            Muting = source.FriendshipStatus.Muting,
            FollowedBy = source.FriendshipStatus.FollowedBy,
            IncomingRequest = source.FriendshipStatus.IncomingRequest,
            IsBestie = source.FriendshipStatus.IsBestie,
            IsPrivate = source.FriendshipStatus.IsPrivate,
            OutgoingRequest = source.FriendshipStatus.OutgoingRequest
        };
        userFriendship.Pk = source.Pk;
        userFriendship.FullName = source.FullName;
        userFriendship.IsPrivate = source.IsPrivate;
        userFriendship.IsVerified = source.IsVerified;
        userFriendship.ProfilePicture = source.ProfilePicture;
        userFriendship.UserName = source.UserName;
        userFriendship.HasAnonymousProfilePicture = source.HasAnonymousProfilePicture;
        return userFriendship;
    }
}