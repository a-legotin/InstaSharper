﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaRecentActivityStoryItemResponse
    {
        [JsonProperty("profile_id")]
        public long ProfileId { get; set; }

        [JsonProperty("profile_image")]
        public string ProfileImage { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("inline_follow")]
        public InstaInlineFollowResponse InlineFollow { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("links")]
        public List<InstaLinkResponse> Links { get; set; }
    }
}