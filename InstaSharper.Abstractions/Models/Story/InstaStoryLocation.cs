using InstaSharper.Abstractions.Models.Location;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryLocation
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Rotation { get; set; }
    public double IsPinned { get; set; }
    public double IsHidden { get; set; }
    public InstaPlaceShort Location { get; set; }
}