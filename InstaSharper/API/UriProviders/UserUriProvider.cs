using System;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Utils;

namespace InstaSharper.API.UriProviders
{
    internal class UserUriProvider : IUserUriProvider
    {
        public Uri Login { get; } = new Uri(Constants.BASE_URI_APIv1 + "accounts/login/");
        public Uri Logout { get; } = new Uri(Constants.BASE_URI_APIv1 + "accounts/logout/");
    }
}