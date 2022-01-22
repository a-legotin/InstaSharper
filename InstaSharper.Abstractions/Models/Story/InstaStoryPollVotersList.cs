using System;
using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryPollVotersList
{
    public long PollId { get; set; }

    public List<InstaStoryVoterItem> Voters { get; set; } = new();

    public string MaxId { get; set; }

    public bool MoreAvailable { get; set; }

    public DateTime LatestPollVoteTime { get; set; }
}