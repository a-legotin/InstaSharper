using System;
using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryQuestionInfo
{
    public long QuestionId { get; set; }

    public string Question { get; set; }

    public string QuestionType { get; set; }

    public string BackgroundColor { get; set; }

    public string TextColor { get; set; }

    public List<InstaStoryQuestionResponder> Responders { get; set; } = new();

    public string MaxId { get; set; }

    public bool MoreAvailable { get; set; }

    public int QuestionResponseCount { get; set; }

    public DateTime LatestQuestionResponseTime { get; set; }
}