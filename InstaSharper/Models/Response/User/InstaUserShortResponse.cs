using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;

namespace InstaSharper.Models.Response.User
{
    internal class InstaUserShortResponse : BaseStatusResponse
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("profile_pic_url")]
        public string ProfilePicture { get; set; }

        [JsonPropertyName("profile_pic_id")]
        public string ProfilePictureId { get; set; } = "unknown";

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("is_verified")]
        public bool IsVerified { get; set; }

        [JsonPropertyName("is_private")]
        public bool IsPrivate { get; set; }

        [JsonPropertyName("pk")]
        public long Pk { get; set; }

        [JsonPropertyName("has_anonymous_profile_picture")]
        public bool? HasAnonymousProfilePicture { get; set; }

        [JsonPropertyName("is_bestie")]
        public bool? IsBestie { get; set; }
    }

    internal class InstaUserListShortResponse : BaseStatusResponse
    {
        [JsonPropertyName("users")]
        public List<InstaUserShortResponse> Items { get; set; }

        [JsonPropertyName("big_list")]
        public bool IsBigList { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("next_max_id")]
        public string NextMaxId { get; set; }
    }
}