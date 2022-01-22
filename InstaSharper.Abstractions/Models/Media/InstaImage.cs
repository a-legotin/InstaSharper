namespace InstaSharper.Abstractions.Models.Media;

public class InstaImage
{
    public InstaImage(string uri,
                      int width,
                      int height)
    {
        Uri = uri;
        Width = width;
        Height = height;
    }

    public InstaImage()
    {
    }

    public string Uri { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }
}