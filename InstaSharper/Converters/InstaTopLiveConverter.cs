using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaTopLiveConverter : IObjectConverter<InstaTopLive, InstaTopLiveResponse>
    {
        public InstaTopLiveResponse SourceObject { get; set; }

        public InstaTopLive Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var storyTray = new InstaTopLive {RankedPosition = SourceObject.RankedPosition};
            foreach (var owner in SourceObject.BroadcastOwners)
            {
                var userOwner = ConvertersFabric.Instance.GetUserShortConverter(owner).Convert();
                storyTray.BroadcastOwners.Add(userOwner);
            }

            return storyTray;
        }
    }
}