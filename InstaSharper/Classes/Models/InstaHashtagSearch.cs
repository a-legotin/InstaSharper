using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaHashtagSearch : List<InstaHashtag>
    {
        public bool MoreAvailable { get; set; }

        public string RankToken { get; set; }
    }
}
