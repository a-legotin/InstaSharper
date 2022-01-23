using System;
using System.Collections.Specialized;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Utils;

namespace InstaSharper.API.UriProviders;

internal class MediaUriProvider : IMediaUriProvider
{
    private static readonly string MediaInfosFeed = "media/infos/";

    public Uri GetMediaInfosUri(string uuid,
                                params string[] mediaIds)
    {
        var queryParams = new NameValueCollection
        {
            { "_uuid", uuid },
            { "media_ids", string.Join(",", mediaIds) },
            { "ranked_content", "true" },
            { "include_inactive_reel", "true" }
        };

        if (!Uri.TryCreate($"{Constants.BASE_URI_APIv1}{MediaInfosFeed}",
                UriKind.RelativeOrAbsolute, out var instaUri))
            throw new Exception("Cant create URI for user media list retrieval");
        return new UriBuilder(instaUri)
        {
            Query = queryParams.ToQueryString()
        }.Uri;
    }
}