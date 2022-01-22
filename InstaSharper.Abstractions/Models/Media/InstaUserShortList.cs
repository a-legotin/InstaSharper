using System.Collections.Generic;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Media;

public class InstaUserShortList : List<InstaUserShort>
{
    public string NextMaxId { get; set; }
}