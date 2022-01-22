using System;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryVoterItem
{
    public InstaUserShortFriendship User { get; set; }

    public double Vote { get; set; }

    public DateTime Time { get; set; }
}