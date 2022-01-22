using System.Collections.Generic;
using InstaSharper.Abstractions.Models.Media;

namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcastCommentList
{
    public bool CommentLikesEnabled { get; set; }

    public List<InstaBroadcastComment> Comments { get; set; } = new();

    public int CommentCount { get; set; }

    public InstaCaption Caption { get; set; }

    public bool CaptionIsEdited { get; set; }

    public bool HasMoreComments { get; set; }

    public bool HasMoreHeadloadComments { get; set; }

    public string MediaHeaderDisplay { get; set; }

    public int LiveSecondsPerComment { get; set; }

    public string IsFirstFetch { get; set; }

    public InstaBroadcastComment PinnedComment { get; set; }

    public object SystemComments { get; set; }

    public int CommentMuted { get; set; }
}