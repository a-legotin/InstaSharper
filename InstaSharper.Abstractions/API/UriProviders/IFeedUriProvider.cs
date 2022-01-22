using System;

namespace InstaSharper.Abstractions.API.UriProviders;

public interface IFeedUriProvider
{
    Uri GetUserMediaFeedUri(long userPk, string nextId = "");
}