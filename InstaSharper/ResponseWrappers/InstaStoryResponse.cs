using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class InstaStoryResponse
    {
        [JsonProperty("can_reply")]
        public bool CanReply { get; set; }

        [JsonProperty("expiring_at")]
        public string ExpiringAt { get; set; }

        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }

        [JsonProperty("source_token")]
        public string SourceToken { get; set; }

        [JsonProperty("seen")]
        public bool Seen { get; set; }

        [JsonProperty("latest_reel_media")]
        public string LatestReelMedia { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ranked_position")]
        public int RankedPosition { get; set; }

        [JsonProperty("seen_ranked_position")]
        public int SeenRankedPosition { get; set; }
    }
}