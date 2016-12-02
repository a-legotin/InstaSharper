namespace InstaSharper.Classes.Models
{
    public class Image
    {
        public Image(string url, string width, string height)
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