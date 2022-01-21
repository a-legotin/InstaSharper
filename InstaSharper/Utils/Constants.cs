namespace InstaSharper.Utils
{
    internal static class Constants
    {
        public static readonly string BASE_URI = "https://i.instagram.com/";
        public static readonly string BASE_URI_APIv1 = $"{BASE_URI}api/v1/";
        public static readonly string SIGNED_BODY = "signed_body";
        public static readonly string CSRFTOKEN = "csrftoken";
        public static readonly int TIMEZONE_OFFSET = 16200;
        
        internal static class Headers
        {
            public static readonly string IG_SET_USE_AUTH_HEADER = "ig-set-use-auth-header-for-sso";
            public static readonly string IG_SET_AUTHORIZATION= "ig-set-authorization";
            public static readonly string WWW_CLAIM = "X-Ig-Www-Claim";
            public static readonly string SET_WWW_CLAIM = "X-Ig-Set-Www-Claim";
            public static readonly string DS_USER_ID = "Ig-U-Ds-User-Id";
            public static readonly string INTENDED_USER_ID = "Ig-Intended-User-Id";
            public static readonly string PUB_KEY_HEADER = "ig-set-password-encryption-pub-key";
            public static readonly string KEY_ID_HEADER = "ig-set-password-encryption-key-id";
            public static readonly string IG_U_SHBID = "Ig-U-Shbid";
            public static readonly string IG_SET_U_SHBID = "Ig-Set-Ig-U-Shbid";
            public static readonly string IG_U_SHBTS = "Ig-U-Shbts";
            public static readonly string IG_SET_U_SHBTS = "Ig-Set-Ig-U-Shbts";
            public static readonly string IG_U_RUR = "Ig-U-Rur";
            public static readonly string IG_SET_U_RUR = "Ig-Set-Ig-U-Rur";
            public static readonly string HEADER_X_FB_TRIP_ID = "X-FB-TRIP-ID";
        }
    }
}