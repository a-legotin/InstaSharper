using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramAPI.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaCurrentUserResponse : BaseStatusResponse
    {
        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }
    }
}
