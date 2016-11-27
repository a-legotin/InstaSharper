using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaTagFeedResponse
    {
        [JsonProperty("ranked_items")]
        public List<InstaMediaItemResponse> Items { get; set; }
    }
}
