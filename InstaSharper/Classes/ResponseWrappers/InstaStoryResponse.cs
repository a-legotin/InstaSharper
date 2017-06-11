using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaStoryResponse
    {
        [JsonProperty("can_reply")]
        public bool CanReply { get; set; }

        [JsonProperty("expiring_at")]
        public long ExpiringAt { get; set; }

        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }

        [JsonProperty("source_token")]
        public string SourceToken { get; set; }

        [JsonProperty("seen")]
        public double Seen { get; set; } //Should be a DateTime

        [JsonProperty("latest_reel_media")]
        public string LatestReelMedia { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ranked_position")]
        public int RankedPosition { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }

        [JsonProperty("seen_ranked_position")]
        public int SeenRankedPosition { get; set; }

        [JsonProperty("items")]
        public List<InstaStoryItemResponse> Items { get; set; }

        [JsonProperty("prefetch_count")]
        public int PrefetchCount { get; set; }

        [JsonProperty("social_context")]
        public string SocialContext { get; set; }
    }
}