﻿using System;
using InstaSharper.API;
using InstaSharper.Classes.Models;

namespace InstaSharper.Helpers
{
    internal class UriCreator
    {
        private static readonly Uri BaseInstagramUri = new Uri(InstaApiConstants.INSTAGRAM_URL);

        public static Uri GetMediaUri(string mediaId)
        {
            return Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_MEDIA, mediaId),
                out var instaUri)
                ? instaUri
                : null;
        }

        public static Uri GetUserUri(string username)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.SEARCH_USERS, out var instaUri))
                throw new Exception("Cant create search user URI");
            var userUriBuilder = new UriBuilder(instaUri) {Query = $"q={username}"};
            return userUriBuilder.Uri;
        }

        public static Uri GetUserFeedUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.TIMELINEFEED, out var instaUri))
                throw new Exception("Cant create timeline feed URI");
            return instaUri;
        }

        public static Uri GetUserMediaListUri(string userPk)
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.USEREFEED + userPk, out var instaUri))
                throw new Exception("Cant create URI for user media retrieval");
            return instaUri;
        }

        public static Uri GetLoginUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.ACCOUNTS_LOGIN, out var instaUri))
                throw new Exception("Cant create URI for user login");
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

        public static Uri GetMediaListWithMaxIdUri(string userPk, string nextId)
        {
            if (
                !Uri.TryCreate(new Uri(InstaApiConstants.INSTAGRAM_URL), InstaApiConstants.USEREFEED + userPk,
                    out var instaUri)) throw new Exception("Cant create URI for media list");
            var uriBuilder = new UriBuilder(instaUri) {Query = $"max_id={nextId}"};
            return uriBuilder.Uri;
        }

        public static Uri GetCurrentUserUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.CURRENTUSER, out var instaUri))
                throw new Exception("Cant create URI for current user info");
            return instaUri;
        }

        internal static Uri GetUserFollowersUri(string userPk, string rankToken, string maxId = "")
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_FOLLOWERS, userPk, rankToken),
                    out var instaUri)) throw new Exception("Cant create URI for user followers");
            if (string.IsNullOrEmpty(maxId)) return instaUri;
            var uriBuilder = new UriBuilder(instaUri) {Query = $"max_id={maxId}"};
            return uriBuilder.Uri;
        }

        internal static Uri GetUserFollowingUri(string userPk, string rankToken, string maxId = "")
        {
            if (
                !Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_USER_FOLLOWING, userPk, rankToken),
                    out var instaUri)) throw new Exception("Cant create URI for user following");
            if (string.IsNullOrEmpty(maxId)) return instaUri;
            var uriBuilder = new UriBuilder(instaUri) {Query = $"max_id={maxId}"};
            return uriBuilder.Uri;
        }

        public static Uri GetTagFeedUri(string tag)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.GET_TAG_FEED, tag), out var instaUri))
                throw new Exception("Cant create URI for discover tag feed");
            return instaUri;
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

        public static Uri GetDirectSendMessageUri()
        {
            if (!Uri.TryCreate(BaseInstagramUri, InstaApiConstants.GET_DIRECT_TEXT_BROADCAST, out var instaUri))
                throw new Exception("Cant create URI for sending message");
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

        public static Uri GetUserTagsUri(string userPk, string rankToken, string maxId = null)
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

        public static Uri GetMediaCommentsUri(string mediaId)
        {
            if (!Uri.TryCreate(BaseInstagramUri, string.Format(InstaApiConstants.MEDIA_COMMENTS, mediaId),
                out var instaUri))
                throw new Exception("Cant create URI for getting media comments");
            return instaUri;
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