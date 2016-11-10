using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaFeedPageInfoResponse
    {
        [JsonProperty("has_next_page")]
        public bool HasNextPage { get; set; }

        [JsonProperty("has_previous_page")]
        public bool HasPrevPage { get; set; }

        [JsonProperty("start_cursor")]
        public string StartCursor { get; set; }

        [JsonProperty("end_cursor")]
        public string EndCursor { get; set; }
    }
}