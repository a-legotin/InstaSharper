using System.Collections.Generic;

namespace InstagramAPI.Classes.Models
{
    public class InstaMediaList : List<InstaMedia>
    {
        public int Pages { get; set; }
    }
}