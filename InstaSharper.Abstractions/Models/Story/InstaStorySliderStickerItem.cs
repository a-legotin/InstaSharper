﻿namespace InstaSharper.Abstractions.Models.Story;

public class InstaStorySliderStickerItem
{
    public string TextColor { get; set; }

    public long SliderId { get; set; }

    public string Question { get; set; }

    public string Emoji { get; set; }

    public bool ViewerCanVote { get; set; }

    public long SliderVoteCount { get; set; } = 0;

    public double SliderVoteAverage { get; set; } = 0;
}