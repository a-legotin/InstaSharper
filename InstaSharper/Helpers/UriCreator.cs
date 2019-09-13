using System;
using System.Collections.Generic;
using System.Threading;
using InstaSharper.API;
using InstaSharper.Classes.Models;

namespace InstaSharper.Helpers
{
    internal static class UriCreator
    {
        private static readonly Uri BaseInstagramUri = new Uri(InstaApiConstants.INSTAGRAM_URL);

        public static Uri GetMediaUri(string mediaId)
        {
            return Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_MEDIA, mediaId),
                out var instaUri)
                ? instaUri
                : null;
        }

        public static Uri GetSearchTagUri(string tag, int count, IEnumerable<long> excludeList, string rankToken)
        {
            excludeList = excludeList ?? new List<long>();
            var excludeListStr = $"[{string.Join(",", excludeList)}]";
            if (!Uri.TryCreate(BaseInstagramUri,
                string.Format(InstaApiConstants.SEARCH_TAGS, tag, count),
                out var instaUri))
                throw new Exception("Cant create search tag URI");
            return instaUri
                .AddQueryParameter("exclude_list", excludeListStr)
                .AddQueryParameter("rank_token", rankToken);
        }

        public static Uri GetTagInfoUri(string tag)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_TAG_INFO, tag), out var instaUri))
                throw new Exception("Cant create tag info URI");
            return instaUri;
        }

        public static Uri GetUserUri(string username)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SEARCH_USERS, out var instaUri))
                throw new Exception("Cant create search user URI");
            var userUriBuilder = new UriBuilder(instaUri) {Query = $"q={username}"};
            return userUriBuilder.Uri;
        }

        public static Uri GetUserInfoByIdUri(long pk)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_INFO_BY_ID, pk),
                out var instaUri))
                throw new Exception("Cant create user info by identifier URI");
            return instaUri;
        }

        public static Uri GetUserInfoByUsernameUri(string username)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_INFO_BY_USERNAME, username),
                out var instaUri))
                throw new Exception("Cant create user info by username URI");
            return instaUri;
        }

        public static Uri GetUserFeedUri(string maxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.TIMELINEFEED, out var instaUri))
                throw new Exception("Cant create timeline feed URI");
            return !string.IsNullOrEmpty(maxId)
                ? new UriBuilder(instaUri) {Query = $"max_id={maxId}"}.Uri
                : instaUri;
        }

        public static Uri GetUserMediaListUri(long userPk, string nextId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.USEREFEED + userPk, out var instaUri))
                throw new Exception("Cant create URI for user media retrieval");
            return !string.IsNullOrEmpty(nextId)
                ? new UriBuilder(instaUri) {Query = $"max_id={nextId}"}.Uri
                : instaUri;
        }

        public static Uri GetCreateAccountUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_CREATE, out var instaUri))
                throw new Exception("Cant create URI for user creation");
            return instaUri;
        }

        public static Uri GetLoginUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOGIN, out var instaUri))
                throw new Exception("Cant create URI for user login");
            return instaUri;
        }
        
        public static Uri GetResetChallengeUri(string token)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.RESET_CHALLENGE, token), out var instaUri))
                throw new Exception("Cant create URI for challenge reset");
            return instaUri;
        }
        
        public static Uri GetVerifyMethod(string token)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.VERIFY_METHOD, token), out var instaUri))
                throw new Exception("Cant create URI for challenge verify method");
            return instaUri;
        }
        
        public static Uri GetFbSearchPlace(int count, string rankToken, string searchQuery)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.FB_SEARCH_PLACE, count, searchQuery, rankToken), out var instaUri))
                throw new Exception("Cant create URI for fb search place");
            return instaUri;
        }

        public static Uri GetTwoFactorLoginUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_2FA_LOGIN, out var instaUri))
                throw new Exception("Cant create URI for user 2FA login");
            return instaUri;
        }

        public static Uri GetTimelineWithMaxIdUri(string nextId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.TIMELINEFEED, out var instaUri))
                throw new Exception("Cant create search URI for timeline");
            var uriBuilder = new UriBuilder(instaUri) {Query = $"max_id={nextId}"};
            return uriBuilder.Uri;
        }

        public static Uri GetCurrentUserUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.CURRENTUSER, out var instaUri))
                throw new Exception("Cant create URI for current user info");
            return instaUri;
        }

        public static Uri GetUserFollowersUri(long userPk, string rankToken, string searchQuery, string maxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_FOLLOWERS, userPk, rankToken),
                out var instaUri))
                throw new Exception("Cant create URI for user followers");
            return instaUri
                .AddQueryParameterIfNotEmpty("max_id", maxId)
                .AddQueryParameterIfNotEmpty("query", searchQuery);
        }

        public static Uri GetUserFollowingUri(long userPk, string rankToken, string searchQuery, string maxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_FOLLOWING, userPk, rankToken),
                out var instaUri))
                throw new Exception("Cant create URI for user following");
            return instaUri
                .AddQueryParameterIfNotEmpty("max_id", maxId)
                .AddQueryParameterIfNotEmpty("query", searchQuery);
        }

        public static Uri GetTagFeedUri(string tag, string maxId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_TAG_FEED, tag), out var instaUri))
                throw new Exception("Cant create URI for discover tag feed");
            return !string.IsNullOrEmpty(maxId)
                ? new UriBuilder(instaUri) {Query = $"max_id={maxId}"}.Uri
                : instaUri;
        }

        public static Uri GetLogoutUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOGOUT, out var instaUri))
                throw new Exception("Cant create URI for user logout");
            return instaUri;
        }

        public static Uri GetExploreUri(string maxId = null)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.EXPLORE, out var instaUri))
                throw new Exception("Cant create URI for explore posts");
            var query = string.Empty;
            if (!string.IsNullOrEmpty(maxId)) query += $"max_id={maxId}";
            var uriBuilder = new UriBuilder(instaUri) {Query = query};
            return uriBuilder.Uri;
        }

        public static Uri GetDirectSendTextMessageUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_TEXT_BROADCAST, out var instaUri))
                throw new Exception("Cant create URI for sending message");
            return instaUri;
        }
        
        public static Uri GetDirectSendLinkMessageUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_LINK_BROADCAST, out var instaUri))
                throw new Exception("Cant create URI for sending link as a message");
            return instaUri;
        }
        
        public static Uri GetShareMediaUri(InstaMediaType mediaType)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_MEDIA_SHARE_BROADCAST, out var instaUri))
                throw new Exception("Cant create URI for sending media share");
            var query = mediaType == InstaMediaType.Image ? "media_type=photo" : "media_type=video";
            var uriBuilder = new UriBuilder(instaUri) {Query = query};
            return uriBuilder.Uri;
        }
        
        public static Uri GetApproveThreadUri(string threadId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_DIRECT_APPROVE_THREAD, threadId),
                out var instaUri))
                throw new Exception("Cant create URI for user following");
            return instaUri;
        }
        
        public static Uri GetDeclineAllPendingThreadsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_DECLINE_ALL, out var instaUri))
                throw new Exception("Cant create URI for get inbox");
            return instaUri;
        }

        public static Uri GetDirectInboxUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_INBOX, out var instaUri))
                throw new Exception("Cant create URI for get inbox");
            return instaUri;
        }

        public static Uri GetDirectInboxThreadUri(string threadId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_DIRECT_THREAD, threadId),
                    out var instaUri)) throw new Exception("Cant create URI for get inbox thread by id");
            return instaUri;
        }

        public static Uri GetUserTagsUri(long userPk, string rankToken, string maxId = null)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_TAGS, userPk),
                out var instaUri))
                throw new Exception("Cant create URI for get user tags");
            var query = $"rank_token={rankToken}&ranked_content=true";
            if (!string.IsNullOrEmpty(maxId)) query += $"max_id={maxId}";
            var uriBuilder = new UriBuilder(instaUri) {Query = query};
            return uriBuilder.Uri;
        }

        public static Uri GetRecentRecipientsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_RECENT_RECIPIENTS, out var instaUri))
                throw new Exception("Cant create URI (get recent recipients)");
            return instaUri;
        }

        public static Uri GetRankedRecipientsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_RANKED_RECIPIENTS, out var instaUri))
                throw new Exception("Cant create URI (get ranked recipients)");
            return instaUri;
        }

        public static Uri GetRecentActivityUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_RECENT_ACTIVITY, out var instaUri))
                throw new Exception("Cant create URI (get recent activity)");
            var query = $"activity_module=all";
            var uriBuilder = new UriBuilder(instaUri) {Query = query};
            return uriBuilder.Uri;
        }

        public static Uri GetFollowingRecentActivityUri(string maxId = null)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_FOLLOWING_RECENT_ACTIVITY, out var instaUri))
                throw new Exception("Cant create URI (get following recent activity");
            var query = string.Empty;
            if (!string.IsNullOrEmpty(maxId)) query += $"max_id={maxId}";
            var uriBuilder = new UriBuilder(instaUri) {Query = query};
            return uriBuilder.Uri;
        }

        public static Uri GetUnLikeMediaUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.UNLIKE_MEDIA, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for unlike media");
            return instaUri;
        }

        public static Uri GetLikeMediaUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.LIKE_MEDIA, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for like media");
            return instaUri;
        }

        public static Uri GetMediaCommentsUri(string mediaId, string nextId = "")
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_COMMENTS, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media comments");
            return !string.IsNullOrEmpty(nextId)
                ? new UriBuilder(instaUri) {Query = $"max_id={nextId}"}.Uri
                : instaUri;
        }

        public static Uri GetMediaLikersUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_LIKERS, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }

        public static Uri GetFollowUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.FOLLOW_USER, userId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }

        public static Uri GetUnFollowUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.UNFOLLOW_USER, userId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }


        public static Uri GetBlockUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.BLOCK_USER, userId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }

        public static Uri GetUnBlockUserUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.UNBLOCK_USER, userId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media likers");
            return instaUri;
        }


        public static Uri GetUriSetAccountPrivate()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SET_ACCOUNT_PRIVATE, out var instaUri))
                throw new Exception("Cant create URI for set account private");
            return instaUri;
        }

        public static Uri GetUriSetAccountPublic()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SET_ACCOUNT_PUBLIC, out var instaUri))
                throw new Exception("Cant create URI for set account public");
            return instaUri;
        }

        public static Uri GetPostCommetUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.POST_COMMENT, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for posting comment");
            return instaUri;
        }

        public static Uri GetAllowMediaCommetsUri(string mediaId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.ALLOW_MEDIA_COMMENTS, mediaId),
                    out var instaUri))
                throw new Exception("Cant create URI to allow comments on media");
            return instaUri;
        }

        public static Uri GetDisableMediaCommetsUri(string mediaId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DISABLE_MEDIA_COMMENTS, mediaId),
                    out var instaUri))
                throw new Exception("Cant create URI to disable comments on media");
            return instaUri;
        }

        public static Uri GetDeleteCommetUri(string mediaId, string commentId)
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DELETE_COMMENT, mediaId, commentId),
                    out var instaUri))
                throw new Exception("Cant create URI for delete comment");
            return instaUri;
        }

        public static Uri GetUploadVideoUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.UPLOAD_VIDEO, out var instaUri))
                throw new Exception("Cant create URI for upload video");
            return instaUri;
        }

        public static Uri GetUploadPhotoUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.UPLOAD_PHOTO, out var instaUri))
                throw new Exception("Cant create URI for upload photo");
            return instaUri;
        }

        public static Uri GetMediaConfigureUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.MEDIA_CONFIGURE, out var instaUri))
                throw new Exception("Cant create URI for configuring media");
            return instaUri;
        }

        public static Uri GetMediaAlbumConfigureUri()
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, InstaApiConstants.MEDIA_ALBUM_CONFIGURE, out var instaUri))
                throw new Exception("Cant create URI for configuring media album");
            return instaUri;
        }

        public static Uri GetStoryFeedUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_STORY_TRAY, out var instaUri))
                throw new Exception("Can't create URI for getting story tray");
            return instaUri;
        }

        public static Uri GetUserStoryUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_STORY, userId),
                out var instaUri))
                throw new Exception("Can't create URI for getting user's story");
            return instaUri;
        }

        public static Uri GetStoryConfigureUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.STORY_CONFIGURE, out var instaUri))
                throw new Exception("Can't create URI for configuring story media");
            return instaUri;
        }

        public static Uri GetChangePasswordUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.CHANGE_PASSWORD, out var instaUri))
                throw new Exception("Can't create URI for changing password");
            return instaUri;
        }

        public static Uri GetDeleteMediaUri(string mediaId, InstaMediaType mediaType)
        {
            if (!Uri.TryCreate(BaseInstagramUri,
                string.Format(InstaApiConstants.DELETE_MEDIA, mediaId, (int) mediaType), out var instaUri))
                throw new Exception("Can't create URI for deleting media");
            return instaUri;
        }

        public static Uri GetEditMediaUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.EDIT_MEDIA, mediaId),
                out var instaUri))
                throw new Exception("Can't create URI for editing media");
            return instaUri;
        }

        public static Uri GetUserLikeFeedUri(string maxId = null)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.LIKE_FEED, out var instaUri))
                throw new Exception("Can't create URI for getting like feed");
            var query = string.Empty;
            if (!string.IsNullOrEmpty(maxId)) query += $"max_id={maxId}";
            var uriBuilder = new UriBuilder(instaUri) {Query = query};
            return uriBuilder.Uri;
        }

        public static Uri GetUserFriendshipUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Concat(InstaApiConstants.FRIENDSHIPSTATUS, userId, "/"),
                out var instaUri))
                throw new Exception("Can't create URI for getting friendship status");
            return instaUri;
        }

        public static Uri GetUserReelFeedUri(long userId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.USER_REEL_FEED, userId),
                out var instaUri))
                throw new Exception("Can't create URI for getting user reel feed");
            return instaUri;
        }

        public static Uri GetCollectionUri(long collectionId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_COLLECTION, collectionId),
                out var instaUri))
                throw new Exception("Can't create URI for getting collection");
            return instaUri;
        }

        public static Uri GetEditCollectionUri(long collectionId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.EDIT_COLLECTION, collectionId),
                out var instaUri))
                throw new Exception("Can't create URI for editing collection");
            return instaUri;
        }

        public static Uri GetCollectionsUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_LIST_COLLECTIONS,
                out var instaUri))
                throw new Exception("Can't create URI for getting collections");
            return instaUri;
        }

        public static Uri GetCreateCollectionUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.CREATE_COLLECTION,
                out var instaUri))
                throw new Exception("Can't create URI for creating collection");
            return instaUri;
        }

        public static Uri GetDeleteCollectionUri(long collectionId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.DELETE_COLLECTION, collectionId),
                out var instaUri))
                throw new Exception("Can't create URI for deleting collection");
            return instaUri;
        }

        public static Uri GetMediaIdFromUrlUri(Uri uri)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_MEDIAID, uri.AbsoluteUri),
                out var instaUri))
                throw new Exception("Can't create URI for getting media id");
            return instaUri;
        }

        public static Uri GetShareLinkFromMediaId(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_SHARE_LINK, mediaId),
                out var instaUri))
                throw new Exception("Can't create URI for getting share link");
            return instaUri;
        }
    }
}