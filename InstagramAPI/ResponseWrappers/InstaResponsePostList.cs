using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.ResponseWrappers;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaResponsePostList : BaseLoadableResponse
    {
        [JsonProperty("items")]
        public List<InstaUserFeedItemResponse> Items { get; set; }
    }
}
