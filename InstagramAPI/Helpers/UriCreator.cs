using System;
using InstagramAPI.API;

namespace InstagramAPI.Helpers
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
            var userUriBuilder = new UriBuilder(instaUri) {Query = $"q={username}"};
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

        public static Uri GetLogintUri()
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOGIN, out instaUri)) throw new Exception("Cant create URI for user login");
            return instaUri;
        }

        public static Uri GetTimelineWithMaxIdUri(string nextId)
        {
            Uri instaUri;
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.TIMELINEFEED, out instaUri)) throw new Exception("Cant create search URI for timeline");
            var uriBuilder = new UriBuilder(instaUri) {Query = $"max_id={nextId}"};
            return uriBuilder.Uri;
        }

        public static Uri GetMediaListWithMaxIdUri(string userPk, string nextId)
        {
            Uri instaUri;
            if (!Uri.TryCreate(new Uri(InstaApiConstants.INSTAGRAM_URL), InstaApiConstants.USEREFEED + userPk + "/", out instaUri)) throw new Exception("Cant create URI for media list");
            var uriBuilder = new UriBuilder(instaUri) {Query = $"max_id={nextId}"};
            return uriBuilder.Uri;
        }
    }
}