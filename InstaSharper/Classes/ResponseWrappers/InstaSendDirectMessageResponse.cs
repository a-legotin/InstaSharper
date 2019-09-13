using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaSendDirectMessageResponse : BaseStatusResponse
    {
        public List<InstaDirectInboxThreadResponse> Threads { get; set; } = new List<InstaDirectInboxThreadResponse>();
    }
}