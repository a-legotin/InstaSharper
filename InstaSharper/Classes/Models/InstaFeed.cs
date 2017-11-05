using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaFeed
    {
        public int MediaItemsCount => Medias.Count;
        public int StoriesItemsCount => Stories.Count;

        public InstaMediaList Medias { get; set; } = new InstaMediaList();
        public List<InstaStory> Stories { get; set; } = new List<InstaStory>();
    }
}