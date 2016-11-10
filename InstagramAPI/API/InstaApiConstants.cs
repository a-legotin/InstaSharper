namespace InstagramApi.API
{
    public static class InstaApiConstants
    {
        public static string GET_ALL_POSTFIX = "/?__a=1";
        public static string MAX_MEDIA_ID_POSTFIX = "/media/?max_id=";
        public static string HEADER_MAX_ID = "max_id";
        public static string MEDIA = "/media/";
        public static string P_SUFFIX = "p/";
        public static string CSRFTOKEN = "csrftoken";

        public static string HEADER_XCSRFToken = "X-CSRFToken";
        public static string HEADER_XInstagramAJAX = "X-Instagram-AJAX";
        public static string HEADER_XRequestedWith = "X-Requested-With";
        public static string HEADER_XMLHttpRequest = "XMLHttpRequest";
        public static string INSTAGRAM_URL = "https://i.instagram.com";
        public static string API_SUFFIX = "/api";
        public static string GET_USER = API_SUFFIX + "/v1/accounts/login/";
        public static string SEARCH_USERS = API_SUFFIX + "/v1/users/search";
        public static string ACCOUNTS_LOGIN = API_SUFFIX + "/v1/accounts/login/";
        public static string TIMELINEFEED = API_SUFFIX + "/v1/feed/timeline/";
        public static string USEREFEED = API_SUFFIX + "/v1/feed/user/";
        public static string GET_MEDIA = API_SUFFIX + "/v1/media/{0}/info/";

        public static string HEADER_USER_AGENT = "User-Agent";

        public static string USER_AGENT =
            "Instagram 8.0.0 Android (23/6.0.1; 640dpi; 1440x2560; samsung; SM-G935F; hero2lte; samsungexynos8890; en_NZ)";
        public static string HEADER_QUERY = "q";
        public static string HEADER_RANK_TOKEN = "rank_token";
        public static string HEADER_COUNT = "count";

        public static string IG_SIGNATURE_KEY = "9b3b9e55988c954e51477da115c58ae82dcae7ac01c735b4443a3c5923cb593a";
        public static string HEADER_IG_SIGNATURE = "signed_body";
        public static string IG_SIGNATURE_KEY_VERSION = "4";
        public static string HEADER_IG_SIGNATURE_KEY_VERSION = "ig_sig_key_version";
        public static string IG_CAPABILITIES = "3Q==";
        public static string HEADER_IG_CAPABILITIES = "X-IG-Capabilities";
        public static string IG_CONNECTION_TYPE = "WIFI";
        public static string HEADER_IG_CONNECTION_TYPE = "X-IG-Connection-Type";
        public static string ACCEPT_LANGUAGE = "en-NZ";
        public static string HEADER_ACCEPT_LANGUAGE = "Accept-Language";
        public static string ACCEPT_ENCODING = "gzip, deflate, sdch";
        public static string HEADER_ACCEPT_ENCODING = "gzip, deflate, sdch";
        public static string TIMEZONE = "Pacific/Auckland";
        public static string HEADER_PHONE_ID = "phone_id";
        public static string HEADER_TIMEZONE = "timezone_offset";
        public static string HEADER_XGOOGLE_AD_IDE = "X-Google-AD-ID";

        public static int TIMEZONE_OFFSET = 43200;
    }
}