using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaRecipientThreads
    {
        public List<InstaRankedRecipientThread> Items = new List<InstaRankedRecipientThread>();
        public long ExpiresIn { get; set; }

        public bool Filtered { get; set; }

        public string RankToken { get; set; }

        public string RequestId { get; set; }
    }
}