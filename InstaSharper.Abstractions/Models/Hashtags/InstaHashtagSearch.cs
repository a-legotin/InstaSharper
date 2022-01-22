using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Hashtags;

public class InstaHashtagSearch : List<InstaHashtag>
{
    public bool MoreAvailable { get; set; }

    public string RankToken { get; set; }
}