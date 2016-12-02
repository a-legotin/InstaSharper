namespace InstaSharper.API
{
    public static class InstaApiConstants
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
        public const string GET_USER = API_SUFFIX + "/v1/accounts/login/";
        public const string SEARCH_USERS = API_SUFFIX + "/v1/users/search";
        public const string ACCOUNTS_LOGIN = API_SUFFIX + "/v1/accounts/login/";
        public const string TIMELINEFEED = API_SUFFIX + "/v1/feed/timeline/";
        public const string USEREFEED = API_SUFFIX + "/v1/feed/user/";
        public const string GET_MEDIA = API_SUFFIX + "/v1/media/{0}/info/";
        public const string GET_USER_FOLLOWERS = API_SUFFIX + "/v1/friendships/{0}/followers/?rank_token={1}";
        public const string GET_TAG_FEED = API_SUFFIX + "/v1/feed/tag/{0}/";
        public const string HEADER_USER_AGENT = "User-Agent";
        public const string USER_AGENT = "Instagram 9.7.0 Android (23/6.0.1; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_NZ)";

        public const string HEADER_QUERY = "q";
        public const string HEADER_RANK_TOKEN = "rank_token";
        public const string HEADER_COUNT = "count";

        public const string IG_SIGNATURE_KEY = "2f6dcdf76deb0d3fd008886d032162a79b88052b5f50538c1ee93c4fe7d02e60";
        public const string HEADER_IG_SIGNATURE = "signed_body";
        public const string IG_SIGNATURE_KEY_VERSION = "4";
        public const string HEADER_IG_SIGNATURE_KEY_VERSION = "ig_sig_key_version";
        public const string IG_CAPABILITIES = "3Ro=";
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