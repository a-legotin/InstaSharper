namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryHashtagUpload
{
    public double X { get; set; } = 0.5;
    public double Y { get; set; } = 0.5;
    public double Z { get; set; } = 0;

    public double Width { get; set; } = 0.3148148;
    public double Height { get; set; } = 0.110367894;
    public double Rotation { get; set; } = 0.0;

    public string TagName { get; set; }

    public bool IsSticker { get; set; } = false;
}