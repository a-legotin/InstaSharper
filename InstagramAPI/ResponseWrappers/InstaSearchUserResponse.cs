using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class InstaSearchUserResponse
    {
        [JsonProperty("has_more")]
        public bool MoreAvailable { get; set; }
        [JsonProperty("num_results")]
        public bool ResultCount { get; set; }
        [JsonProperty("users")]
        public List<InstaUserResponse> Users { get; set; }
    }
}
