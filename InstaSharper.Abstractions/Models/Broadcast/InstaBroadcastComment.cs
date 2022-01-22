namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcastComment : InstaBroadcastSendComment
{
    public long UserId { get; set; }

    public int BitFlags { get; set; }

    public bool DidReportAsSpam { get; set; }

    public string InlineComposerDisplayCondition { get; set; }
}