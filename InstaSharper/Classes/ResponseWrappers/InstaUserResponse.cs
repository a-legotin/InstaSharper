using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaUserResponse : BadStatusResponse
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("profile_pic_url")]
        public string ProfilePicture { get; set; }

        [JsonProperty("profile_pic_id")]
        public string ProfilePictureId { get; set; }


        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("pk")]
        public string Pk { get; set; }

        [JsonProperty("follower_count")]
        public int FollowerCount { get; set; }

        [JsonProperty("unseen_count")]
        public int UnseenCount { get; set; }

        [JsonProperty("mutual_followers_count")]
        public string MutualFollowersCount { get; set; }

        [JsonProperty("friendship_status")]
        public InstaFriendshipStatusResponse Friendship { get; set; }

        [JsonProperty("has_anonymous_profile_picture")]
        public bool HasAnonymousProfilePicture { get; set; }
    }
}