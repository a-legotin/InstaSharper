namespace InstaSharper.Classes.Models
{
    public class MediaImage
    {
        public MediaImage(string url, string width, string height)
        {
            Url = url;
            Width = width;
            Height = height;
        }

        public string Url { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }
    }
}