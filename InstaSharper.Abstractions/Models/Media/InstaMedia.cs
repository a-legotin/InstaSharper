using System;
using System.Collections.Generic;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.Media;

public class InstaMedia
{
    public long TakenAtUnix { get; set; }
    public DateTime TakenAt { get; set; }
    public long Pk { get; set; }

    public string InstaIdentifier { get; set; }

    public DateTime DeviceTimeStamp { get; set; }
    public InstaMediaType MediaType { get; set; }

    public string Code { get; set; }

    public string ClientCacheKey { get; set; }
    public string FilterType { get; set; }

    public List<InstaImage> Images { get; set; } = new();
    public List<InstaVideo> Videos { get; set; } = new();

    public int Width { get; set; }
    public int Height { get; set; }

    public InstaUser User { get; set; }

    public string TrackingToken { get; set; }

    public int LikesCount { get; set; }

    public string NextMaxId { get; set; }

    public InstaCaption Caption { get; set; }

    public long CommentsCount { get; set; }

    public bool IsCommentsDisabled { get; set; }

    public bool PhotoOfYou { get; set; }

    public bool HasLiked { get; set; }

    public IList<InstaUserTag> UserTags { get; set; } = new List<InstaUserTag>();

    public InstaUserShortList Likers { get; set; } = new();
    public InstaCarousel Carousel { get; set; }

    public int ViewCount { get; set; }

    public bool HasAudio { get; set; }

    public bool IsMultiPost => Carousel != null;
    public IList<InstaComment> PreviewComments { get; set; } = new List<InstaComment>();
    public InstaLocation Location { get; set; }


    public bool CommentLikesEnabled { get; set; }

    public bool CommentThreadingEnabled { get; set; }

    public bool HasMoreComments { get; set; }

    public int MaxNumVisiblePreviewComments { get; set; }

    public bool CanViewMorePreviewComments { get; set; }

    public bool CanViewerReshare { get; set; }

    public bool CaptionIsEdited { get; set; }

    public bool CanViewerSave { get; set; }

    public bool HasViewerSaved { get; set; }

    public string Title { get; set; }

    public string ProductType { get; set; }

    public bool NearlyCompleteCopyrightMatch { get; set; }

    public int NumberOfQualities { get; set; }

    public double VideoDuration { get; set; }

    public List<InstaProductTag> ProductTags { get; set; } = new();

    public bool DirectReplyToAuthorEnabled { get; set; }

    public override string ToString()
    {
        return $"[{Code}] {Caption}";
    }
}