using System;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;

namespace InstaSharper.Converters
{
    internal class InstaCarouselItemConverter : IObjectConverter<InstaCarouselItem, InstaCarouselItemResponse>
    {
        public InstaCarouselItemResponse SourceObject { get; set; }

        public InstaCarouselItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var carouselItem = new InstaCarouselItem
            {
                CarouselParentId = SourceObject.CarouselParentId,
                Height = int.Parse(SourceObject.Height),
                Width = int.Parse(SourceObject.Width)
            };
            foreach (var image in SourceObject.Images.Candidates)
                carouselItem.Images.Add(new MediaImage(image.Url, image.Width, image.Height));
            return carouselItem;
        }
    }
}