using System;
using InstaSharper.API;

namespace InstaSharper.Helpers
{
    public class UriCreator
    {
        private static readonly Uri BaseInstagramUri = new Uri(InstaApiConstants.INSTAGRAM_URL);

        public static Uri GetMediaUri(string mediaId)
        {
            Uri instaUri;
            return Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_MEDIA, mediaId), out instaUri) ? instaUri : null;
        }

        public static Uri GetUserUri(string username)
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SEARCH_USERS, out instaUri)) throw new Exception("Cant create search user URI");
            var userUriBuilder = new UriBuilder(instaUri) { Query = $"q={username}" };
            return userUriBuilder.Uri;
        }

        public static Uri GetUserFeedUri()
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.TIMELINEFEED, out instaUri)) throw new Exception("Cant create timeline feed URI");
            return instaUri;
        }

        public static Uri GetUserMediaListUri(string userPk)
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.USEREFEED + userPk + "/", out instaUri)) throw new Exception("Cant create URI for user media retrieval");
            return instaUri;
        }

        public static Uri GetLoginUri()
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOGIN, out instaUri)) throw new Exception("Cant create URI for user login");
            return instaUri;
        }

        public static Uri GetTimelineWithMaxIdUri(string nextId)
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.TIMELINEFEED, out instaUri)) throw new Exception("Cant create search URI for timeline");
            var uriBuilder = new UriBuilder(instaUri) { Query = $"max_id={nextId}" };
            return uriBuilder.Uri;
        }

        public static Uri GetMediaListWithMaxIdUri(string userPk, string nextId)
        {
            Uri instaUri;
            if (!Uri.TryCreate(new Uri(InstaApiConstants.INSTAGRAM_URL), InstaApiConstants.USEREFEED + userPk + "/", out instaUri)) throw new Exception("Cant create URI for media list");
            var uriBuilder = new UriBuilder(instaUri) { Query = $"max_id={nextId}" };
            return uriBuilder.Uri;
        }

        public static Uri GetCurrentUserUri()
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.CURRENTUSER, out instaUri)) throw new Exception("Cant create URI for current user info");
            return instaUri;
        }

        internal static Uri GetUserFollowersUri(string userPk, string rankToken, string maxId = "")
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_FOLLOWERS, userPk, rankToken), out instaUri)) throw new Exception("Cant create URI for user followers");
            if (string.IsNullOrEmpty(maxId)) return instaUri;
            var uriBuilder = new UriBuilder(instaUri) { Query = $"max_id={maxId}" };
            return uriBuilder.Uri;
        }

        public static Uri GetTagFeedUri(string tag)
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_TAG_FEED, tag), out instaUri)) throw new Exception("Cant create URI for discover tag feed");
            return instaUri;
        }
        public static Uri GetLogoutUri()
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOGOUT, out instaUri)) throw new Exception("Cant create URI for user logout");
            return instaUri;
        }
    }
}