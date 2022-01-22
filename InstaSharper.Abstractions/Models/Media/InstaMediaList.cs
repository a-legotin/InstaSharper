using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Media;

public class InstaMediaList : List<InstaMedia>
{
    public string NextMaxId { get; set; }
    public bool MoreAvailable { get; set; }
}