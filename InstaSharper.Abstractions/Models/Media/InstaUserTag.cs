using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Media;

public class InstaUserTag
{
    public InstaPosition Position { get; set; }

    public string TimeInVideo { get; set; }

    public InstaUserShort User { get; set; }
}