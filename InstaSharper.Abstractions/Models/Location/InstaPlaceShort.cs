namespace InstaSharper.Abstractions.Models.Location;

public class InstaPlaceShort
{
    public long Pk { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string ShortName { get; set; }

    public double Lng { get; set; }

    public double Lat { get; set; }

    public string ExternalSource { get; set; }

    public long FacebookPlacesId { get; set; }
}