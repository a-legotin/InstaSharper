using System;

namespace InstaSharper.Abstractions.API.UriProviders;

public interface IUserUriProvider
{
    Uri Login { get; }

    Uri Logout { get; }

    Uri SearchUsers(string query);
}