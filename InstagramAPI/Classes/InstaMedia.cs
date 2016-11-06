using System;

namespace InstagramApi.Classes.Web
{
    public class InstaMedia
    {
        public bool CaptionIsEdited { get; set; }
        public string Code { get; set; }

        public Dimensions Dimensions { get; set; }

        public InstaUser Owner { get; set; }

        public bool IsAdvertisement { get; set; }
        public bool IsVideo { get; set; }

        public string ImageSourceLink { get; set; }
        public InstaLocation Location { get; set; }
        public string InstaIdentifier { get; set; }

        public DateTime Date { get; set; }
    }
}