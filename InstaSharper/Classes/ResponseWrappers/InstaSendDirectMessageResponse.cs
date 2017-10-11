using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using System.Collections.Generic;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaSendDirectMessageResponse : BaseStatusResponse
    {
        public List<InstaDirectInboxThreadResponse> Threads { get; set; } = new List<InstaDirectInboxThreadResponse>();
    }    
}
