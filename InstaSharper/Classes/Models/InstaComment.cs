using System;

namespace InstaSharper.Classes.Models
{
    public class InstaComment
    {
        public int Type { get; set; }

        public int BitFlags { get; set; }

        public long UserId { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public int LikesCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public InstaContentType ContentType { get; set; }
        public InstaUser User { get; set; }
        public string Pk { get; set; }
        public string Text { get; set; }
    }
}