
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.User
{
    internal class InstaUserResponse : InstaUserShortResponse
    {
        [JsonPropertyName("friendship_status")]
        public InstaFriendshipShortStatusResponse FriendshipStatus { get; set; }

        [JsonPropertyName("follower_count")]
        public int FollowersCount { get; set; }

        [JsonPropertyName("byline")]
        public string FollowersCountByLine { get; set; }

        [JsonPropertyName("social_context")]
        public string SocialContext { get; set; }

        [JsonPropertyName("search_social_context")]
        public string SearchSocialContext { get; set; }

        [JsonPropertyName("mutual_followers_count")]
        public string MulualFollowersCount { get; set; }

        [JsonPropertyName("unseen_count")]
        public int UnseenCount { get; set; }

        [JsonPropertyName("latest_reel_media")]
        public long? LatestReelMedia { get; set; }
    }
}