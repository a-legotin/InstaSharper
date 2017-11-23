using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaPermalinkResponse : BaseStatusResponse
    {
        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }
}
