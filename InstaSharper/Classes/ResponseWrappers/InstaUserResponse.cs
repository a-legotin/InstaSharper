using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaUserResponse : InstaUserShortResponse
    {
        [JsonProperty("friendship_status")] public InstaFriendshipStatusResponse FriendshipStatus { get; set; }

        [JsonProperty("has_anonymous_profile_picture")] public bool HasAnonymousProfilePicture { get; set; }

        [JsonProperty("follower_count")] public int FollowersCount { get; set; }

        [JsonProperty("byline")] public string FollowersCountByLine { get; set; }

        [JsonProperty("social_context")] public string SocialContext { get; set; }

        [JsonProperty("search_social_context")] public string SearchSocialContext { get; set; }

        [JsonProperty("mutual_followers_count")] public string MulualFollowersCount { get; set; }

        [JsonProperty("unseen_count")] public int UnseenCount { get; set; }

        [JsonProperty("can_boost_post")] public bool CanBoostPost { get; set; }

        [JsonProperty("show_insights_terms")] public bool ShowInsightsTerms { get; set; }

        [JsonProperty("is_business")] public bool IsBusiness { get; set; }

        [JsonProperty("nametag")] public InstaNametag Nametag { get; set; }

        [JsonProperty("has_placed_orders")] public bool HasPlacedOrders { get; set; }

        [JsonProperty("can_see_organic_insights")] public bool CanSeeOrganicInsights { get; set; }

        [JsonProperty("allowed_commenter_type")] public string AllowedCommEnterType { get; set; }

        [JsonProperty("reel_auto_archive")] public string ReelAutoArchive { get; set; }

        [JsonProperty("allow_contacts_sync")] public bool AllowContactsSync { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }

        [JsonProperty("country_code")] public int CountryCode { get; set; }

        [JsonProperty("national_number")] public long NationalNumber { get; set; }
    }
}