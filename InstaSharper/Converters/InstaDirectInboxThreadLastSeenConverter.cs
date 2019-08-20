using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    public class InstaDirectInboxThreadLastSeenConverter : IObjectConverter<InstaDirectInboxThreadLastSeen, InstaDirectInboxThreadLastSeenResponse>
    {
        public InstaDirectInboxThreadLastSeenResponse SourceObject { get; set; }

        public InstaDirectInboxThreadLastSeen Convert()
        {
            if (SourceObject == null)
                throw new NullReferenceException("Source Object is null");

            return new InstaDirectInboxThreadLastSeen()
            {
                UserId = SourceObject.UserId,
                ItemId = SourceObject.ItemId,
                TimeStamp = SourceObject.TimeStamp
            };
        }
    }
}
