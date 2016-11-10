using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers.BaseResponse
{
    public class BaseStatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
