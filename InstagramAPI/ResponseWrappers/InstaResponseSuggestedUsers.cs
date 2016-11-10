using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaResponseSuggestedUsers
    {
        [JsonProperty("nodes")]
        public IList<InstaUserResponse> Users { get; set; }
    }
}