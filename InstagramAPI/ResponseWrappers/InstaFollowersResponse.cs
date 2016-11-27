using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramAPI.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaFollowersResponse : BaseStatusResponse
    {
        [JsonProperty("users")]
        public List<InstaUserResponse> Items { get; set; }

        [JsonProperty("big_list")]
        public bool IsBigList { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        public bool IsOK()
        {
            return !string.IsNullOrEmpty(Status) && Status.ToLower() == "ok";
        }
    }
}
