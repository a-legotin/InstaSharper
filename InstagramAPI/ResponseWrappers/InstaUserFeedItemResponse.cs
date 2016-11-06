using InstagramApi.Classes;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class InstaUserFeedItemResponse
    {
        [JsonProperty("type")]
        public int Type { get; set; } = 0;

        [JsonProperty("taken_at")]
        public string TakenAtUnixLike { get; set; }

        [JsonProperty("pk")]
        public string PK { get; set; }

        [JsonProperty("id")]
        public string InstaIdentifier { get; set; }

        [JsonProperty("device_timestamp")]
        public string DeviceTimeStapUnixLike { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("media_type")]
        public InstaMediaType MediaType { get; set; }

        [JsonProperty("original_width")]
        public int Width { get; set; }

        [JsonProperty("original_height")]
        public string Height { get; set; }

        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }

        [JsonProperty("organic_tracking_token")]
        public string TrakingToken { get; set; }

        [JsonProperty("like_count")]
        public int LikesCount { get; set; }

        [JsonProperty("next_max_id")]
        public string NextMaxId { get; set; }

        [JsonProperty("caption")]
        public InstaCaptionResponse Caption { get; set; }

        [JsonProperty("comment_count")]
        public string CommentsCount { get; set; }
    }
}