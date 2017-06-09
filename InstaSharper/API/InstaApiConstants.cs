namespace InstaSharper.API
{
    internal static class InstaApiConstants
    {
        public const string CURRENTUSER = API_SUFFIX + "/v1/accounts/current_user?edit=true";
        public const string MAX_MEDIA_ID_POSTFIX = "/media/?max_id=";
        public const string HEADER_MAX_ID = "max_id";
        public const string MEDIA = "/media/";
        public const string P_SUFFIX = "p/";
        public const string CSRFTOKEN = "csrftoken";

        public const string HEADER_XCSRF_TOKEN = "X-CSRFToken";
        public const string HEADER_X_INSTAGRAM_AJAX = "X-Instagram-AJAX";
        public const string HEADER_X_REQUESTED_WITH = "X-Requested-With";
        public const string HEADER_XML_HTTP_REQUEST = "XMLHttpRequest";
        public const string INSTAGRAM_URL = "https://i.instagram.com";
        public const string API_SUFFIX = "/api";
        public const string SEARCH_USERS = API_SUFFIX + "/v1/users/search";
        public const string ACCOUNTS_LOGIN = API_SUFFIX + "/v1/accounts/login/";
        public const string CHANGE_PASSWORD = API_SUFFIX + "/v1/accounts/change_password/";
        public const string ACCOUNTS_LOGOUT = API_SUFFIX + "/v1/accounts/logout/";
        public const string EXPLORE = API_SUFFIX + "/v1/discover/explore/";
        public const string TIMELINEFEED = API_SUFFIX + "/v1/feed/timeline";
        public const string USEREFEED = API_SUFFIX + "/v1/feed/user/";
        public const string GET_USER_TAGS = API_SUFFIX + "/v1/usertags/{0}/feed/";
        public const string GET_MEDIA = API_SUFFIX + "/v1/media/{0}/info/";
        public const string GET_USER_FOLLOWERS = API_SUFFIX + "/v1/friendships/{0}/followers/?rank_token={1}";
        public const string GET_USER_FOLLOWING = API_SUFFIX + "/v1/friendships/{0}/following/?rank_token={1}";
        public const string GET_TAG_FEED = API_SUFFIX + "/v1/feed/tag/{0}";
        public const string GET_RANKED_RECIPIENTS = API_SUFFIX + "/v1/direct_v2/ranked_recipients";
        public const string GET_RECENT_RECIPIENTS = API_SUFFIX + "/v1/direct_share/recent_recipients/";
        public const string GET_DIRECT_THREAD = API_SUFFIX + "/v1/direct_v2/threads/{0}";
        public const string GET_DIRECT_INBOX = API_SUFFIX + "/v1/direct_v2/inbox/";
        public const string GET_DIRECT_TEXT_BROADCAST = API_SUFFIX + "/v1/direct_v2/threads/broadcast/text/";
        public const string GET_RECENT_ACTIVITY = API_SUFFIX + "/v1/news/inbox/";
        public const string GET_FOLLOWING_RECENT_ACTIVITY = API_SUFFIX + "/v1/news/";
        public const string LIKE_MEDIA = API_SUFFIX + "/v1/media/{0}/like/";
        public const string UNLIKE_MEDIA = API_SUFFIX + "/v1/media/{0}/unlike/";
        public const string MEDIA_COMMENTS = API_SUFFIX + "/v1/media/{0}/comments/";
        public const string MEDIA_LIKERS = API_SUFFIX + "/v1/media/{0}/likers/";
        public const string FOLLOW_USER = API_SUFFIX + "/v1/friendships/create/{0}/";
        public const string UNFOLLOW_USER = API_SUFFIX + "/v1/friendships/destroy/{0}/";
        public const string SET_ACCOUNT_PRIVATE = API_SUFFIX + "/v1/accounts/set_private/";
        public const string SET_ACCOUNT_PUBLIC = API_SUFFIX + "/v1/accounts/set_public/";
        public const string POST_COMMENT = API_SUFFIX + "/v1/media/{0}/comment/";
        public const string ALLOW_MEDIA_COMMENTS = API_SUFFIX + "/v1/media/{0}/enable_comments/";
        public const string DISABLE_MEDIA_COMMENTS = API_SUFFIX + "/v1/media/{0}/disable_comments/";
        public const string DELETE_COMMENT = API_SUFFIX + "/v1/media/{0}/comment/{1}/delete/";
        public const string UPLOAD_PHOTO = API_SUFFIX + "/v1/upload/photo/";
        public const string MEDIA_CONFIGURE = API_SUFFIX + "/v1/media/configure/";
        public const string DELETE_MEDIA = API_SUFFIX + "/v1/media/{0}/delete/?media_type={1}";
        public const string EDIT_MEDIA = API_SUFFIX + "/v1/media/{0}/edit_media/";
        public const string GET_STORY_TRAY = API_SUFFIX + "/v1/feed/reels_tray/";
        public const string GET_USER_STORY = API_SUFFIX + "/v1/feed/user/{0}/reel_media/";
        public const string STORY_CONFIGURE = API_SUFFIX + "/v1/media/configure_to_reel/";
        public const string LOCATION_SEARCH = API_SUFFIX + "/v1/location_search/";
        public static string LIKE_FEED = API_SUFFIX + "/v1/feed/liked/";
        public const string HEADER_USER_AGENT = "User-Agent";

        public const string USER_AGENT =
                "Instagram 10.15.0 Android (23/6.0.1; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_NZ)"
            ;

        public const string HEADER_QUERY = "q";
        public const string HEADER_RANK_TOKEN = "rank_token";
        public const string HEADER_COUNT = "count";
        public const string IG_SIGNATURE_KEY = "b03e0daaf2ab17cda2a569cace938d639d1288a1197f9ecf97efd0a4ec0874d7";
        public const string HEADER_IG_SIGNATURE = "signed_body";
        public const string IG_SIGNATURE_KEY_VERSION = "4";
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
    }
}