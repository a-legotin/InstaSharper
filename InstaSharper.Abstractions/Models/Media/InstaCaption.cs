using System;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Media;

public class InstaCaption
{
    public long UserId { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public DateTime CreatedAt { get; set; }

    public InstaUserShort User { get; set; }

    public string Text { get; set; }

    public long MediaId { get; set; }

    public long Pk { get; set; }
    
    public override string ToString()
    {
        return Text;
    }
}