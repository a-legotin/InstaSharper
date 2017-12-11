namespace InstaSharper.API
{
    internal static class InstaApiConstants
    {
        public const string MAX_MEDIA_ID_POSTFIX = "/media/?max_id=";
        public const string HEADER_MAX_ID = "max_id";
        public const string MEDIA = "/media/";
        public const string P_SUFFIX = "p/";
        public const string CSRFTOKEN = "csrftoken";

        public const string HEADER_XCSRF_TOKEN = "X-CSRFToken";
        public const string HEADER_X_INSTAGRAM_AJAX = "X-Instagram-AJAX";
        public const string HEADER_X_REQUESTED_WITH = "X-Requested-With";
        public const string HEADER_XML_HTTP_REQUEST = "XMLHttpRequest";

        public const string USER_AGENT = "";

        public const string HEADER_QUERY = "q";
        public const string HEADER_RANK_TOKEN = "rank_token";
        public const string HEADER_COUNT = "count";

        public const string IG_SIGNATURE_KEY = "b4946d296abf005163e72346a6d33dd083cadde638e6ad9c5eb92e381b35784a"
            ; //4749bda4fc1f49372dae3d79db339ce4959cfbbe

        public const string HEADER_IG_SIGNATURE = "signed_body";
        public const string IG_SIGNATURE_KEY_VERSION = "4"; //5
        public const string HEADER_IG_SIGNATURE_KEY_VERSION = "ig_sig_key_version";
        public const string IG_CAPABILITIES = "3boBAA==";
        public const string HEADER_IG_CAPABILITIES = "X-IG-Capabilities";
        public const string IG_CONNECTION_TYPE = "WIFI";
        public const string HEADER_IG_CONNECTION_TYPE = "X-IG-Connection-Type";
        public const string ACCEPT_LANGUAGE = "en-US";
        public const string HEADER_ACCEPT_LANGUAGE = "Accept-Language";
        public const string ACCEPT_ENCODING = "gzip, deflate, sdch";
        public const string HEADER_ACCEPT_ENCODING = "gzip, deflate, sdch";
        public const string TIMEZONE = "Pacific/Auckland";
        public const string HEADER_PHONE_ID = "phone_id";
        public const string HEADER_TIMEZONE = "timezone_offset";
        public const string HEADER_XGOOGLE_AD_IDE = "X-Google-AD-ID";
        public const string COMMENT_BREADCRUMB_KEY = "iN4$aGr0m";
        public const int TIMEZONE_OFFSET = 43200;

        public const string INSTAGRAM_URL = "https://i.instagram.com";
        public const string API = "/api";
        public const string API_SUFFIX = API + API_VERSION;
        public const string API_VERSION = "/v1";

        public const string CURRENTUSER = API_SUFFIX + "/accounts/current_user?edit=true";
        public const string SEARCH_USERS = API_SUFFIX + "/users/search";
        public const string ACCOUNTS_LOGIN = API_SUFFIX + "/accounts/login/";
        public const string ACCOUNTS_2FA_LOGIN = API_SUFFIX + "/accounts/two_factor_login/";
        public const string CHANGE_PASSWORD = API_SUFFIX + "/accounts/change_password/";
        public const string ACCOUNTS_LOGOUT = API_SUFFIX + "/accounts/logout/";
        public const string EXPLORE = API_SUFFIX + "/discover/explore/";
        public const string TIMELINEFEED = API_SUFFIX + "/feed/timeline";
        public const string USEREFEED = API_SUFFIX + "/feed/user/";
        public const string GET_USER_TAGS = API_SUFFIX + "/usertags/{0}/feed/";
        public const string GET_MEDIA = API_SUFFIX + "/media/{0}/info/";
        public const string GET_USER_FOLLOWERS = API_SUFFIX + "/friendships/{0}/followers/?rank_token={1}";
        public const string GET_USER_FOLLOWING = API_SUFFIX + "/friendships/{0}/following/?rank_token={1}";
        public const string GET_TAG_FEED = API_SUFFIX + "/feed/tag/{0}";
        public const string GET_RANKED_RECIPIENTS = API_SUFFIX + "/direct_v2/ranked_recipients";

        public const string GET_LIST_COLLECTIONS = API_SUFFIX + "/collections/list/";
        public const string GET_COLLECTION = API_SUFFIX + "/feed/collection/{0}/";
        public const string CREATE_COLLECTION = API_SUFFIX + "/collections/create/";
        public const string EDIT_COLLECTION = API_SUFFIX + "/collections/{0}/edit/";
        public const string DELETE_COLLECTION = API_SUFFIX + "/collections/{0}/delete/";
        public const string COLLECTION_CREATE_MODULE = API_SUFFIX + "collection_create";
        public const string FEED_SAVED_ADD_TO_COLLECTION_MODULE = "feed_saved_add_to_collection";

        public const string GET_MEDIAID = API_SUFFIX + "/oembed/?url={0}";
        public const string GET_SHARE_LINK = API_SUFFIX + "/media/{0}/permalink/";

        public const string GET_RECENT_RECIPIENTS = API_SUFFIX + "/direct_share/recent_recipients/";
        public const string GET_DIRECT_THREAD = API_SUFFIX + "/direct_v2/threads/{0}";
        public const string GET_DIRECT_INBOX = API_SUFFIX + "/direct_v2/inbox/";
        public const string GET_DIRECT_TEXT_BROADCAST = API_SUFFIX + "/direct_v2/threads/broadcast/text/";
        public const string GET_RECENT_ACTIVITY = API_SUFFIX + "/news/inbox/";
        public const string GET_FOLLOWING_RECENT_ACTIVITY = API_SUFFIX + "/news/";
        public const string LIKE_MEDIA = API_SUFFIX + "/media/{0}/like/";
        public const string UNLIKE_MEDIA = API_SUFFIX + "/media/{0}/unlike/";
        public const string MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/comments/";
        public const string MEDIA_LIKERS = API_SUFFIX + "/media/{0}/likers/";
        public const string FOLLOW_USER = API_SUFFIX + "/friendships/create/{0}/";
        public const string UNFOLLOW_USER = API_SUFFIX + "/friendships/destroy/{0}/";
        public const string BLOCK_USER = API_SUFFIX + "/friendships/block/{0}/";
        public const string UNBLOCK_USER = API_SUFFIX + "/friendships/unblock/{0}/";
        public const string SET_ACCOUNT_PRIVATE = API_SUFFIX + "/accounts/set_private/";
        public const string SET_ACCOUNT_PUBLIC = API_SUFFIX + "/accounts/set_public/";
        public const string POST_COMMENT = API_SUFFIX + "/media/{0}/comment/";
        public const string ALLOW_MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/enable_comments/";
        public const string DISABLE_MEDIA_COMMENTS = API_SUFFIX + "/media/{0}/disable_comments/";
        public const string DELETE_COMMENT = API_SUFFIX + "/media/{0}/comment/{1}/delete/";
        public const string UPLOAD_PHOTO = API_SUFFIX + "/upload/photo/";
        public const string MEDIA_CONFIGURE = API_SUFFIX + "/media/configure/";
        public const string MEDIA_ALBUM_CONFIGURE = API_SUFFIX + "/media/configure_sidecar/";
        public const string DELETE_MEDIA = API_SUFFIX + "/media/{0}/delete/?media_type={1}";
        public const string EDIT_MEDIA = API_SUFFIX + "/media/{0}/edit_media/";
        public const string GET_STORY_TRAY = API_SUFFIX + "/feed/reels_tray/";
        public const string GET_USER_STORY = API_SUFFIX + "/feed/user/{0}/reel_media/";
        public const string STORY_CONFIGURE = API_SUFFIX + "/media/configure_to_reel/";
        public const string LOCATION_SEARCH = API_SUFFIX + "/location_search/";
        public const string FRIENDSHIPSTATUS = API_SUFFIX + "/friendships/show/";
        public const string HEADER_USER_AGENT = "User-Agent";
        public const string LIKE_FEED = API_SUFFIX + "/feed/liked/";
        public const string USER_REEL_FEED = API_SUFFIX + "/feed/user/{0}/reel_media/";
    }
}