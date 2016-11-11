using System.Collections.Generic;

namespace InstagramAPI.Classes
{
    public class InstaMediaList : List<InstaMedia>
    {
        public int Pages { get; set; }
    }
}