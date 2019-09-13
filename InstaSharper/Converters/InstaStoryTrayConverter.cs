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
                Id = SourceObject.Id,
                IsPortrait = SourceObject.IsPortrait,
                TopLive = ConvertersFabric.Instance.GetTopLiveConverter(SourceObject.TopLive).Convert()
            };

            if (SourceObject.Tray != null)
                foreach (var item in SourceObject.Tray)
                {
                    var story = ConvertersFabric.Instance.GetStoryConverter(item).Convert();
                    storyTray.Tray.Add(story);
                }

            return storyTray;
        }
    }
}