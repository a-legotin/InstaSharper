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
                Width = int.Parse(SourceObject.Width),
                MediaType = SourceObject.MediaType,
                InstaIdentifier = SourceObject.InstaIdentifier,
                Pk = SourceObject.Pk
            };
            if (SourceObject?.Images?.Candidates != null)
                foreach (var image in SourceObject.Images.Candidates)
                    carouselItem.Images.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
            if (SourceObject?.Videos != null)
                foreach (var video in SourceObject.Videos)
                    carouselItem.Videos.Add(new InstaVideo(video.Url, int.Parse(video.Width), int.Parse(video.Height),
                        video.Type));

            return carouselItem;
        }
    }
}