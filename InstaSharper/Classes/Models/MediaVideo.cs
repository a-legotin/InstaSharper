namespace InstaSharper.Classes.Models
{
    public class MediaVideo
    {
        public MediaVideo(string url, string width, string height, int type)
        {
            Url = url;
            Width = width;
            Height = height;
            Type = type;
        }

        public string Url { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public int Type { get; set; }
    }
}