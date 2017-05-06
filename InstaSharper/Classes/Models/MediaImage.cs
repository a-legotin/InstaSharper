namespace InstaSharper.Classes.Models
{
    public class MediaImage
    {
        public MediaImage(string uri, int width, int height)
        {
            URI = uri;
            Width = width;
            Height = height;
        }

        public MediaImage()
        {
        }

        public string URI { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}