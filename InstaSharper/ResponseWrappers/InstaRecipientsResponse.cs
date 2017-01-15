using System.Collections.Generic;
using InstaSharper.ResponseWrappers.BaseResponse;

namespace InstaSharper.ResponseWrappers
{
    internal class InstaRecipientsResponse : BaseStatusResponse
    {
        public List<InstaDirectInboxThreadResponse> Threads { get; set; } = new List<InstaDirectInboxThreadResponse>();

        public List<InstaUserResponse> Users { get; set; } = new List<InstaUserResponse>();
    }
}