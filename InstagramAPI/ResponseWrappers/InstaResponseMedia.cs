using InstagramApi.Classes;
using InstagramApi.ResponseWrappers.Common;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers.Web
{
    public class InstaResponseMedia
    {
        [JsonProperty("caption_is_edited")]
        public bool CaptionIsEdited { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }

        [JsonProperty("owner")]
        public InstaUserResponse Owner { get; set; }

        [JsonProperty("is_ad")]
        public bool IsAdvertisement { get; set; }

        [JsonProperty("is_video")]
        public bool IsVideo { get; set; }

        [JsonProperty("display_src")]
        public string ImageSourceLink { get; set; }

        [JsonProperty("id")]
        public string InstaIdentifier { get; set; }

        [JsonProperty("location")]
        public InstaLocation Location { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}