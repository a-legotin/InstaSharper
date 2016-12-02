using System;

namespace InstaSharper.Classes.Models
{
    public class InstaCaption
    {
        public long UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public DateTime CreatedAt { get; set; }

        public InstaUser User { get; set; }

        public string Text { get; set; }

        public string MediaId { get; set; }

        public string Pk { get; set; }
    }
}