using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaRecipientsResponse : BaseStatusResponse
    {
        public List<InstaDirectInboxThreadResponse> Threads { get; set; } = new List<InstaDirectInboxThreadResponse>();

        public List<InstaUserResponse> Users { get; set; } = new List<InstaUserResponse>();
    }
}