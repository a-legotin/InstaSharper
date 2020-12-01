using Newtonsoft.Json;

namespace InstaSharper.Models.Response.User
{
    internal class InstaUserResponse : InstaUserShortResponse
    {
        [JsonProperty("friendship_status")]
        public InstaFriendshipShortStatusResponse FriendshipStatus { get; set; }

        [JsonProperty("follower_count")]
        public int FollowersCount { get; set; }

        [JsonProperty("byline")]
        public string FollowersCountByLine { get; set; }

        [JsonProperty("social_context")]
        public string SocialContext { get; set; }

        [JsonProperty("search_social_context")]
        public string SearchSocialContext { get; set; }

        [JsonProperty("mutual_followers_count")]
        public string MulualFollowersCount { get; set; }

        [JsonProperty("unseen_count")]
        public int UnseenCount { get; set; }

        [JsonProperty("latest_reel_media")]
        public long? LatestReelMedia { get; set; }
    }
}