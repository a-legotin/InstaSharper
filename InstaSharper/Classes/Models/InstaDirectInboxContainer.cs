﻿using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaDirectInboxContainer
    {
        public int PendingRequestsCount { get; set; }

        public int SeqId { get; set; }

        public InstaDirectInboxSubscription Subscription { get; set; } = new InstaDirectInboxSubscription();

        public InstaDirectInbox Inbox { get; set; } = new InstaDirectInbox();

        public List<InstaUserShort> PendingUsers { get; set; } = new List<InstaUserShort>();
    }
}