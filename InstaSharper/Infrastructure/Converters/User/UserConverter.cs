using System;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.Response.User;

namespace InstaSharper.Infrastructure.Converters.User;

internal class UserConverter : IObjectConverter<InstaUser, InstaUserResponse>
{
    private readonly IObjectConverter<InstaFriendshipShortStatus, InstaFriendshipShortStatusResponse>
        _friendshipStatusConverter;

    private readonly IObjectConverter<InstaUserShort, InstaUserShortResponse> _userConverter;

    public UserConverter(IObjectConverter<InstaUserShort, InstaUserShortResponse> userConverter,
                         IObjectConverter<InstaFriendshipShortStatus, InstaFriendshipShortStatusResponse>
                             friendshipStatusConverter)
    {
        _userConverter = userConverter;
        _friendshipStatusConverter = friendshipStatusConverter;
    }

    public InstaUser Convert(InstaUserResponse source)
    {
        if (source == null)
            throw new ArgumentException("InstaUserResponse is null");
        var user = new InstaUser(_userConverter.Convert(source));
        if (source.FriendshipStatus != null)
            user.FriendshipStatus = _friendshipStatusConverter.Convert(source.FriendshipStatus);
        user.HasAnonymousProfilePicture = source.HasAnonymousProfilePicture;
        user.FollowersCount = source.FollowersCount;
        user.FollowersCountByLine = source.FollowersCountByLine;
        user.SearchSocialContext = source.SearchSocialContext;
        user.SocialContext = source.SocialContext;
        user.LatestReelMedia = source.LatestReelMedia.GetValueOrDefault();
        return user;
    }
}