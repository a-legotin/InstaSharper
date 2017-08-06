using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaCommentList
    {
        public int CommentsCount { get; set; }

        public bool LikesEnabled { get; set; }

        public bool CaptionIsEdited { get; set; }

        public bool MoreHeadLoadAvailable { get; set; }

        public InstaCaption Caption { get; set; }

        public bool MoreComentsAvailable { get; set; }

        public List<InstaComment> Comments { get; set; } = new List<InstaComment>();
        public int Pages { get; set; } = 0;
    }
}