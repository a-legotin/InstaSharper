namespace InstaSharper.Classes.Models
{
    public class InstaBaseFeed
    {
        public InstaMediaList Medias { get; set; } = new InstaMediaList();
        public string NextId { get; set; }
    }
}