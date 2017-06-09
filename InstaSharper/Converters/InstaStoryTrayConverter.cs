using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaStoryTrayConverter : IObjectConverter<InstaStoryTray, InstaStoryTrayResponse>
    {
        public InstaStoryTrayResponse SourceObject { get; set; }

        public InstaStoryTray Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var storyTray = new InstaStoryTray
            {
                Status = SourceObject.Status,
                StickerVersion = SourceObject.StickerVersion,
                StoryRankingToken = SourceObject.StoryRankingToken
            };

            if (SourceObject.Tray.Count > 0)
                foreach (var story in SourceObject.Tray)
                    storyTray.Tray.Add(ConvertersFabric.GetStoryConverter(story).Convert());

            return storyTray;
        }
    }
}