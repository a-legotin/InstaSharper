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
        public const string ACCOUNTS_LOGOUT = API_SUFFIX + "/v1/accounts/logout/";
        public const string EXPLORE = API_SUFFIX + "/v1/discover/explore/";
        public const string TIMELINEFEED = API_SUFFIX + "/v1/feed/timeline";
        public const string USEREFEED = API_SUFFIX + "/v1/feed/user/";
        public const string GET_USER_TAGS = API_SUFFIX + "/v1/usertags/{0}/feed/";
        public const string GET_MEDIA = API_SUFFIX + "/v1/media/{0}/info/";
        public const string GET_USER_FOLLOWERS = API_SUFFIX + "/v1/friendships/{0}/followers/?rank_token={1}";
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


        public const string HEADER_USER_AGENT = "User-Agent";

        public const string USER_AGENT =
            "Instagram 10.3.2 Android (23/6.0.1; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_NZ)";

        public const string HEADER_QUERY = "q";
        public const string HEADER_RANK_TOKEN = "rank_token";
        public const string HEADER_COUNT = "count";

        public const string IG_SIGNATURE_KEY = "5ad7d6f013666cc93c88fc8af940348bd067b68f0dce3c85122a923f4f74b251";
        public const string HEADER_IG_SIGNATURE = "signed_body";
        public const string IG_SIGNATURE_KEY_VERSION = "4";
        public const string HEADER_IG_SIGNATURE_KEY_VERSION = "ig_sig_key_version";
        public const string IG_CAPABILITIES = "3ToAAA==";
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

        public const int TIMEZONE_OFFSET = 43200;
    }
}