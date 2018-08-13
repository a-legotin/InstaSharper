using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaLocationConverter : IObjectConverter<InstaLocation, InstaLocationResponse>
    {
        public InstaLocationResponse SourceObject { get; set; }

        public InstaLocation Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var location = new InstaLocation
            {
                Height   = SourceObject.Height,
                Width    = SourceObject.Width,
                X        = SourceObject.X,
                Y        = SourceObject.Y,
                Z        = SourceObject.Z,
                Rotation = SourceObject.Rotation,
                IsHidden = SourceObject.IsHidden,
                IsPinned = SourceObject.IsPinned,
                Location = ConvertersFabric.Instance.GetLocationShortConverter(SourceObject.Location).Convert()
            };
            return location;
        }
    }
}