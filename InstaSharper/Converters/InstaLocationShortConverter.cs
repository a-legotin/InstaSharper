using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaLocationShortConverter : IObjectConverter<InstaLocationShort, InstaLocationShortResponse>
    {
        public InstaLocationShortResponse SourceObject { get; set; }

        public InstaLocationShort Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var location = new InstaLocationShort
            {
                Name           = SourceObject.Name,
                Address        = SourceObject.Address,
                ExternalSource = SourceObject.ExternalSource,
                ExternalId     = SourceObject.ExternalId,
                Lat            = SourceObject.Lat,
                Lng            = SourceObject.Lng,
                City           = SourceObject.City,
                Pk             = SourceObject.Pk,
                ShortName      = SourceObject.ShortName
            };
            return location;
        }
    }
}