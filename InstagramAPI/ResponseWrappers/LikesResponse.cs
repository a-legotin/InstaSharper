using InstagramApi.ResponseWrappers.Common;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers.Web
{
    public class LikesResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public InstaUserResponse[] Users { get; set; }
    }
}