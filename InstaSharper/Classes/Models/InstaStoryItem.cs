using System;
using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaStoryItem
    {
        public DateTime TakenAt { get; set; }

        public long Pk { get; set; }

        public string Id { get; set; }

        public InstaMediaType MediaType { get; set; }

        public string Code { get; set; }

        public string ClientCacheKey { get; set; }

        public int FilterType { get; set; }

        public List<MediaImage> Images { get; set; } = new List<MediaImage>();

        public int OriginalWidth { get; set; }

        public int OriginalHeight { get; set; }

        public double CaptionPosition { get; set; }

        public InstaUser User { get; set; }

        public string TrackingToken { get; set; }

        public int LikeCount { get; set; }

        public InstaUserList Likers { get; set; } = new InstaUserList();

        public bool HasLiked { get; set; }

        public bool HasMoreComments { get; set; }

        public int MaxNumVisiblePreviewComments { get; set; }

        //public InstaComment PreviewComments { get; set; }  --- ---  //I'll check what is.

        public int CommentCount { get; set; }

        public bool CommentsDisabled { get; set; }

        public InstaCaption Caption { get; set; }

        public List<InstaUserTag> UserTags { get; set; } = new List<InstaUserTag>();

        public InstaCarousel CarouselMedia { get; set; } = new InstaCarousel();

        public bool CaptionIsEdited { get; set; } //Visible only if the story is an image.

        public bool PhotoOfYou { get; set; }

        public bool CanViewerSave { get; set; }

        public DateTime ExpiringAt { get; set; }

        public bool IsReelMedia { get; set; }

        //public List<InstaReel> ReelMentions { get; set; }  --- ---  //I'll do a test via Fiddler

        //public List<InstaLocation> StoryLocation { get; set; }

        //public List<string> StoryHashtags { get; set; } //I'll do a test via Fiddler

        #region Video

        public List<MediaVideo> VideoVersions { get; set; } = new List<MediaVideo>(); //Visible only if the story is a video.

        public bool HasAudio { get; set; } //Visible only if the story is a video.

        public double VideoDuration { get; set; } //Visible only if the story is a video.

        #endregion
    }
}