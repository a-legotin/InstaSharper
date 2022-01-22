using System;
using System.Collections.Generic;
using System.ComponentModel;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Media;

public class InstaComment : INotifyPropertyChanged
{
    public int Type { get; set; }

    public int BitFlags { get; set; }

    public long UserId { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public int LikesCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public InstaContentType ContentType { get; set; }
    public InstaUserShort User { get; set; }
    public long Pk { get; set; }
    public string Text { get; set; }

    public bool DidReportAsSpam { get; set; }

    public bool HasLikedComment { get; set; }

    public int ChildCommentCount { get; set; }

    public bool HasMoreTailChildComments { get; set; }

    public bool HasMoreHeadChildComments { get; set; }

    public List<InstaUserShort> OtherPreviewUsers { get; set; } = new();

    public event PropertyChangedEventHandler PropertyChanged;

    private void Update(string PName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PName));
    }

    public bool Equals(InstaComment comment)
    {
        return Pk == comment?.Pk;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as InstaComment);
    }

    public override int GetHashCode()
    {
        return Pk.GetHashCode();
    }
}