using System;

namespace InstaSharper.Abstractions.API.UriProviders;

public interface IUserFollowersUriProvider
{
    Uri GetUserFollowersUri(long userPk, string rankToken, string nextMaxId);
}