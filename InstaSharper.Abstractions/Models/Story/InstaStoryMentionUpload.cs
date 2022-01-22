namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryMentionUpload
{
    public double X { get; set; } = 0.5;
    public double Y { get; set; } = 0.5;
    public double Z { get; set; } = 0;

    public double Width { get; set; } = 0.7972222;
    public double Height { get; set; } = 0.21962096;
    public double Rotation { get; set; } = 0.0;

    public string Username { get; set; }

    internal long Pk { get; set; } = -1;
}