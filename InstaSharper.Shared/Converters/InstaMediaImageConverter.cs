using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaMediaImageConverter : IObjectConverter<MediaImage, ImageResponse>
    {
        public ImageResponse SourceObject { get; set; }

        public MediaImage Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var image = new MediaImage(SourceObject.Url, int.Parse(SourceObject.Width), int.Parse(SourceObject.Height));
            return image;
        }
    }
}