namespace InstaSharper.Classes.Models
{
    public class InstaExploreFeed
    {
        public InstaMediaList Medias { get; set; } = new InstaMediaList();
        public InstaStoryTray StoryTray { get; set; } = new InstaStoryTray();
        public InstaChannel Channel { get; set; } = new InstaChannel();
    }
}