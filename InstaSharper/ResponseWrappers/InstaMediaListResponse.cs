using System.Collections.Generic;
using InstaSharper.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class InstaMediaListResponse : BaseLoadableResponse
    {
        [JsonProperty("items")]
        public List<InstaMediaItemResponse> Medias { get; set; } = new List<InstaMediaItemResponse>();

        public List<InstaStoryResponse> Stories { get; set; } = new List<InstaStoryResponse>();
    }
}