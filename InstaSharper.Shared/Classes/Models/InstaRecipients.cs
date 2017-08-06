using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;

namespace InstaSharper.Classes.Models
{
    public class InstaRecipients : BaseStatusResponse
    {
        public InstaUserList Users { get; set; } = new InstaUserList();
        public List<InstaDirectInboxThread> Threads { get; set; } = new List<InstaDirectInboxThread>();
    }
}