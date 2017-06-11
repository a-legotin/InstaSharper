using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaStoryTray
    {
        public List<InstaStory> Tray { get; set; } = new List<InstaStory>();

        public string StoryRankingToken { get; set; }

        //public List<InstaBroadcast> Broadcasts { get; set; } = new List<InstaBroadcast>(); //No info at this time... I'll check later with Fiddler

        public int StickerVersion { get; set; }

        public string Status { get; set; }
    }
}