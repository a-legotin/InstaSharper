namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryPollItem
{
    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    public float Width { get; set; }

    public float Height { get; set; }

    public float Rotation { get; set; }

    public int IsPinned { get; set; }

    public int IsHidden { get; set; }

    public InstaStoryPollStickerItem PollSticker { get; set; }
}