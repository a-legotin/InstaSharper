using System.Collections.Generic;

namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryUploadOptions
{
    public List<InstaStoryLocationUpload> Locations { get; set; } = new();

    public List<InstaStoryHashtagUpload> Hashtags { get; set; } = new();

    public List<InstaStoryPollUpload> Polls { get; set; } = new();

    public InstaStorySliderUpload Slider { get; set; }

    public InstaStoryCountdownUpload Countdown { get; set; }

    internal InstaMediaStoryUpload MediaStory { get; set; }

    public List<InstaStoryMentionUpload> Mentions { get; set; } = new();

    public List<InstaStoryQuestionUpload> Questions { get; set; } = new();
}