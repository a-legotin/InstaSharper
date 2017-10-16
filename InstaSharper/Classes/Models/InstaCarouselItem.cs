using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaCarouselItem
    {
        public string InstaIdentifier { get; set; }

        public InstaMediaType MediaType { get; set; }

        public List<MediaImage> Images { get; set; } = new List<MediaImage>();

        public int Width { get; set; }

        public int Height { get; set; }

        public string Pk { get; set; }

        public string CarouselParentId { get; set; }
    }
}