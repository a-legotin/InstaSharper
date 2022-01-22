using System;
using System.Collections.Specialized;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Utils;

namespace InstaSharper.API.UriProviders;

internal class FeedUriProvider : IFeedUriProvider
{
    private static readonly string UserMediaFeed = "feed/user/{0}/";
    private static readonly string TimelineFeed = "feed/timeline/";
    private static readonly string StoryFeed = "feed/reels_tray/";

    public Uri GetUserMediaFeedUri(long userPk,
                                   string nextId = "")
    {
        var queryParams = new NameValueCollection
        {
            { "exclude_comment", "true" },
            { "only_fetch_first_carousel_media", "false" }
        };

        if (!string.IsNullOrEmpty(nextId)) queryParams.Add("max_id", nextId);

        if (!Uri.TryCreate($"{Constants.BASE_URI_APIv1}{string.Format(UserMediaFeed, userPk)}",
                UriKind.RelativeOrAbsolute, out var instaUri))
            throw new Exception("Cant create URI for user media retrieval");
        return new UriBuilder(instaUri)
        {
            Query = queryParams.ToQueryString()
        }.Uri;
    }

    public Uri GetTimelineFeedUri(string nextId = "")
    {
        var queryParams = new NameValueCollection();

        if (!string.IsNullOrEmpty(nextId)) queryParams.Add("max_id", nextId);

        if (!Uri.TryCreate($"{Constants.BASE_URI_APIv1}{TimelineFeed}",
                UriKind.RelativeOrAbsolute, out var instaUri))
            throw new Exception("Cant create URI for timeline feed");
        return new UriBuilder(instaUri)
        {
            Query = queryParams.ToQueryString()
        }.Uri;
    }

    public Uri GetStoryFeedUri()
    {
        if (!Uri.TryCreate($"{Constants.BASE_URI_APIv1}{StoryFeed}",
                UriKind.RelativeOrAbsolute, out var instaUri))
            throw new Exception("Cant create URI for story feed");
        return new UriBuilder(instaUri).Uri;
    }
}