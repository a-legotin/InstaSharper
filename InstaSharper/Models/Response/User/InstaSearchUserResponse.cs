using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Models.Response.User
{
    internal class InstaSearchUserResponse
    {
        [JsonProperty("has_more")]
        public bool MoreAvailable { get; set; }

        [JsonProperty("num_results")]
        public bool ResultCount { get; set; }

        [JsonProperty("users")]
        public List<InstaUserResponse> Users { get; set; }
    }
}