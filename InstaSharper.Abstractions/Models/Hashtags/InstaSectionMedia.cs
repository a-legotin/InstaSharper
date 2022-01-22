using System.Collections.Generic;
using InstaSharper.Abstractions.Models.Media;

namespace InstaSharper.Abstractions.Models.Hashtags;

public class InstaSectionMedia
{
    public List<InstaMedia> Medias { get; set; } = new();

    public List<InstaRelatedHashtag> RelatedHashtags { get; set; } = new();

    public bool MoreAvailable { get; set; }

    public string NextMaxId { get; set; }

    public int NextPage { get; set; }

    public List<long> NextMediaIds { get; set; } = new();

    public bool AutoLoadMoreEnabled { get; set; }
}