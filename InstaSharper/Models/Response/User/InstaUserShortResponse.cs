using InstaSharper.Models.Response.Base;
using Newtonsoft.Json;

namespace InstaSharper.Models.Response.User
{
    internal class InstaUserShortResponse : BaseStatusResponse
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("profile_pic_url")]
        public string ProfilePicture { get; set; }

        [JsonProperty("profile_pic_id")]
        public string ProfilePictureId { get; set; } = "unknown";

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("pk")]
        public long Pk { get; set; }

        [JsonProperty("has_anonymous_profile_picture")]
        public bool? HasAnonymousProfilePicture { get; set; }

        [JsonProperty("is_bestie")]
        public bool? IsBestie { get; set; }
    }
}