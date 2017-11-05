namespace InstaSharper.Classes.Models
{
    public class InstaLocation
    {
        public long FacebookPlacesId { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string ExternalSource { get; set; }

        public double Lng { get; set; }

        public long Pk { get; set; }

        public double Lat { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}