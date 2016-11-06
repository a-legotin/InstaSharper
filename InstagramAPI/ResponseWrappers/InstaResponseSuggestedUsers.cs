using System.Collections.Generic;
using InstagramApi.ResponseWrappers.Common;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers.Web
{
    public class InstaResponseSuggestedUsers
    {
        [JsonProperty("nodes")]
        public IList<InstaUserResponse> Users { get; set; }
    }
}