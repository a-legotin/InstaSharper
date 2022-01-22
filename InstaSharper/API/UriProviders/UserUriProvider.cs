using System;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Utils;

namespace InstaSharper.API.UriProviders;

internal class UserUriProvider : IUserUriProvider
{
    public Uri Login { get; } = new(Constants.BASE_URI_APIv1 + "accounts/login/");
    public Uri Logout { get; } = new(Constants.BASE_URI_APIv1 + "accounts/logout/");

    public Uri SearchUsers(string query)
    {
        if (!Uri.TryCreate(new Uri(Constants.BASE_URI_APIv1), "users/search/", out var result))
            throw new Exception("Cant create search user URI");
        return new UriBuilder(result)
        {
            Query = "q=" + query
        }.Uri;
    }
}