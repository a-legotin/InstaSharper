using System;
using System.Collections.Specialized;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Utils;

namespace InstaSharper.API.UriProviders;

internal class UserFollowersUriProvider : IUserFollowersUriProvider
{
    private const string FRIENDSHIPS_APPROVE = "friendships/approve/{0}/";
    private const string FRIENDSHIPS_UNFOLLOW_USER = "friendships/destroy/{0}/";
    private const string FRIENDSHIPS_USER_FOLLOWERS = "friendships/{0}/followers/";
    private const string FRIENDSHIPS_USER_FOLLOWING = "friendships/{0}/following/";

    private const string FRIENDSHIPS_USER_FOLLOWERS_MUTUALFIRST =
        "friendships/{0}/followers/?rank_token={1}&rank_mutual={2}";

    public Uri GetUserFollowersUri(long userPk,
                                   string rankToken,
                                   string nextMaxId)
    {
        var queryParams = new NameValueCollection
        {
            { "search_surface", "follow_list_page" },
            { "query", "" },
            { "enable_groups", "true" }
        };

        if (!string.IsNullOrEmpty(rankToken))
            queryParams.Add("rank_token", rankToken);

        if (!string.IsNullOrEmpty(nextMaxId))
            queryParams.Add("max_id", nextMaxId);

        if (!Uri.TryCreate(new Uri(Constants.BASE_URI_APIv1), string.Format(FRIENDSHIPS_USER_FOLLOWERS, userPk),
                out var result))
            throw new Exception("Cant create followers URI");

        return new UriBuilder(result)
        {
            Query = queryParams.ToQueryString()
        }.Uri;
    }

    public Uri GetUserFollowingUri(long userPk,
                                   string rankToken,
                                   string nextMaxId)
    {
        var queryParams = new NameValueCollection
        {
            { "search_surface", "follow_list_page" },
            { "query", "" },
            { "enable_groups", "true" },
            { "follow_list_page", "true" }
        };

        if (!string.IsNullOrEmpty(rankToken))
            queryParams.Add("rank_token", rankToken);

        if (!string.IsNullOrEmpty(nextMaxId))
            queryParams.Add("max_id", nextMaxId);

        if (!Uri.TryCreate(new Uri(Constants.BASE_URI_APIv1), string.Format(FRIENDSHIPS_USER_FOLLOWING, userPk),
                out var result))
            throw new Exception("Cant create followers URI");

        return new UriBuilder(result)
        {
            Query = queryParams.ToQueryString()
        }.Uri;
    }
}