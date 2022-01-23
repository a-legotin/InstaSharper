using System;

namespace InstaSharper.Abstractions.API.UriProviders;

public interface IMediaUriProvider
{
    Uri GetMediaInfosUri(string uuid,
                         params string[] mediaIds);
}