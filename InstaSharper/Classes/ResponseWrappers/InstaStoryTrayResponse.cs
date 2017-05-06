using InstaSharper.Classes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes.ResponseWrappers
{
    class InstaStoryTrayResponse
    {
        [JsonProperty("tray")]
        public List<InstaStoryResponse> Tray { get; set; }

        [JsonProperty("story_ranking_token")]
        public string StoryRankingToken { get; set; }

        //public List<InstaBroadcast> Broadcasts { get; set; } = new List<InstaBroadcast>(); //No info at this time... I'll check later with Fiddler

        [JsonProperty("sticker_version")]
        public int StickerVersion { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
